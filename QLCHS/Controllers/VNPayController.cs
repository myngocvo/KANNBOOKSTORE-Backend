using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QLCHS.DTO;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace QLCHS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
        private readonly VnpaySettings _vnpaySettings;

        public VNPayController(IOptions<VnpaySettings> vnpaySettings)
        {
            _vnpaySettings = vnpaySettings.Value;
        }

        [HttpPost("create-payment")]
        public IActionResult CreatePayment([FromBody] PaymentRequest paymentRequest)
        {
            if (paymentRequest.Amount <= 0)
            {
                return BadRequest(new { Status = "Invalid amount", Message = "The amount must be greater than zero." });
            }

            if (string.IsNullOrEmpty(paymentRequest.OrderInfo))
            {
                return BadRequest(new { Status = "Invalid order info", Message = "Order information must be provided." });
            }

            string vnp_ReturnUrl = "http://localhost:4200/payment-result"; // Thay đổi URL này thành URL của bạn
            string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
            string vnp_TmnCode = _vnpaySettings.TmnCode;
            string vnp_HashSecret = _vnpaySettings.HashSecret;

            var orderId = !string.IsNullOrEmpty(paymentRequest.OrderId) ? paymentRequest.OrderId : DateTime.Now.Ticks.ToString();
            var createDate = DateTime.Now.ToString("yyyyMMddHHmmss");
            var expireDate = DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"); // Thay đổi thời gian hết hạn theo nhu cầu

            var vnpay = new VnPayLibrary();

            // Thêm tham số theo đúng thứ tự
            vnpay.AddRequestData("vnp_Version", "2.1.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (paymentRequest.Amount * 100).ToString()); // Nhân với 100 và chuyển thành chuỗi
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", orderId);
            vnpay.AddRequestData("vnp_OrderInfo", Uri.EscapeDataString(paymentRequest.OrderInfo)); // Mã hóa URL
            vnpay.AddRequestData("vnp_ReturnUrl", Uri.EscapeDataString(vnp_ReturnUrl)); // Mã hóa URL
            vnpay.AddRequestData("vnp_IpAddr", HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1");
            vnpay.AddRequestData("vnp_CreateDate", createDate);
            vnpay.AddRequestData("vnp_ExpireDate", expireDate);

            // Thêm tham số ngôn ngữ nếu cần
            vnpay.AddRequestData("vnp_Locale", "vn"); // hoặc giá trị từ request nếu có

            // Thêm tham số loại đơn hàng nếu cần
            vnpay.AddRequestData("vnp_OrderType", "other"); // Hoặc giá trị từ request nếu có

            if (!string.IsNullOrEmpty(paymentRequest.BankCode))
            {
                vnpay.AddRequestData("vnp_BankCode", paymentRequest.BankCode);
            }

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

            return Ok(new { paymentUrl });
        }


        [HttpGet("payment-response")]
        public IActionResult PaymentResponse()
        {
            var vnpayData = Request.Query;
            VnPayLibrary vnpay = new VnPayLibrary();

            foreach (var kv in vnpayData)
            {
                vnpay.AddResponseData(kv.Key, kv.Value);
            }

            string vnp_HashSecret = _vnpaySettings.HashSecret;
            bool checkSignature = vnpay.ValidateSignature(vnpayData["vnp_SecureHash"], vnp_HashSecret);

            if (checkSignature)
            {
                // Handle the payment response here
                // For example, update the order status in the database
                return Ok(new { Status = "Success", Message = "Payment is successful." });
            }
            else
            {
                return BadRequest(new { Status = "Error", Message = "Invalid signature." });
            }
        }

        public class VnPayLibrary
        {
            private SortedList<string, string> _requestData = new SortedList<string, string>(new VnPayCompare());
            private SortedList<string, string> _responseData = new SortedList<string, string>(new VnPayCompare());

            public void AddRequestData(string key, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _requestData.Add(key, value);
                }
            }

            public void AddResponseData(string key, string value)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _responseData.Add(key, value);
                }
            }

            public string CreateRequestUrl(string baseUrl, string vnp_HashSecret)
            {
                StringBuilder data = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in _requestData.OrderBy(x => x.Key))
                {
                    if (data.Length > 0)
                    {
                        data.Append('&');
                    }
                    data.Append(kv.Key + "=" + kv.Value);
                }

                string queryString = data.ToString();
                string signData = queryString + vnp_HashSecret;
                string vnp_SecureHash = ConvertToSHA256(signData);

                return $"{baseUrl}?{queryString}&vnp_SecureHash={vnp_SecureHash}";
            }

            private string ConvertToSHA256(string input)
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("x2"));
                    }
                    return builder.ToString();
                }
            }


            public bool ValidateSignature(string inputHash, string secretKey)
            {
                string rspRaw = GetResponseData();
                string myChecksum = ConvertToSHA256(rspRaw + secretKey);
                return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
            }

            private string GetResponseData()
            {
                StringBuilder data = new StringBuilder();
                foreach (KeyValuePair<string, string> kv in _responseData)
                {
                    if (data.Length > 0)
                    {
                        data.Append('&');
                    }
                    data.Append(kv.Key + "=" + kv.Value);
                }
                return data.ToString();
            }

        }

        public class VnPayCompare : IComparer<string>
        {
            public int Compare(string x, string y)
            {
                return string.CompareOrdinal(x, y);
            }
        }


        public class PaymentRequest
        {
            public string OrderId { get; set; }
            public decimal Amount { get; set; }
            public string BankCode { get; set; } // Mã ngân hàng (Nếu có)
            public string OrderInfo { get; set; }
        }
    }
}
