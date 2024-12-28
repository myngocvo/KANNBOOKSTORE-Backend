using Microsoft.EntityFrameworkCore;
using QLCHS.DTO;
using QLCHS.Entities;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình VNPAY từ appsettings.json
var vnpaySettings = new VnpaySettings();
builder.Configuration.GetSection("VnpaySettings").Bind(vnpaySettings);

// Thêm dịch vụ VNPAY settings vào DI container
builder.Services.AddSingleton(vnpaySettings);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // This handles reference loops
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<QLBANSACHContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QLBANSACH")));

builder.Services.AddCors(p => p.AddPolicy("MyCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline
    app.UseSwagger();
    app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

app.Run();
