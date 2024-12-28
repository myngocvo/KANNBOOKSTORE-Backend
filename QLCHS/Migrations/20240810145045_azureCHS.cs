using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLCHS.Migrations
{
    public partial class azureCHS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUTHORS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUTHORS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BANNERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Image = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANNERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CATEGORIES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activated = table.Column<bool>(type: "bit", nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    birthday = table.Column<DateTime>(type: "date", nullable: true),
                    Phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTPs",
                columns: table => new
                {
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    OTPCode = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OTPs__A9D10535167CAD95", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "SUPPLIERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPPLIERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VOUCHERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PercentDiscount = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    MaxDiscount = table.Column<decimal>(type: "money", nullable: true),
                    DateBegin = table.Column<DateTime>(type: "date", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOUCHERS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BOOKS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AuthorId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    SupplierId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true),
                    PricePercent = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PublishYear = table.Column<int>(type: "int", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BOOKS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BOOKS_AUTHORS",
                        column: x => x.AuthorId,
                        principalTable: "AUTHORS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BOOKS_SUPPLIERS",
                        column: x => x.SupplierId,
                        principalTable: "SUPPLIERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BILLS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    VoucherId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BillDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: true),
                    PaymentStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Code_pay = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BILLS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BILLS_USERS",
                        column: x => x.UserId,
                        principalTable: "USERS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BILLS_VOUCHERS",
                        column: x => x.VoucherId,
                        principalTable: "VOUCHERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BOOKDETAILS",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Dimensions = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    Pages = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BOOKDETA__3DE0C207252AF59E", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BOOKDETAILS_BOOKS",
                        column: x => x.BookId,
                        principalTable: "BOOKS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BOOKDETAILS_CATEGORIES",
                        column: x => x.CategoryId,
                        principalTable: "CATEGORIES",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BOOKIMG",
                columns: table => new
                {
                    BookId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Image0 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BOOKIMG__3DE0C2077BE38430", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BOOKIMG_BOOKS",
                        column: x => x.BookId,
                        principalTable: "BOOKS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CARTS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    BookId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CARTS_BOOKS",
                        column: x => x.BookId,
                        principalTable: "BOOKS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CARTS_CUSTOMER",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_REVIEWS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BookId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayCommemt = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_REVIEWS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_REVIEWS_BOOKS",
                        column: x => x.BookId,
                        principalTable: "BOOKS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_REVIEWS_CUSTOMERS",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    BookId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BillId = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BILLS_ORDERS",
                        column: x => x.BillId,
                        principalTable: "BILLS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ORDER_BOOKS",
                        column: x => x.BookId,
                        principalTable: "BOOKS",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ORDERS_CUSTOMERS",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BILLS_UserId",
                table: "BILLS",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BILLS_VoucherId",
                table: "BILLS",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKDETAILS_CategoryId",
                table: "BOOKDETAILS",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKS_AuthorId",
                table: "BOOKS",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BOOKS_SupplierId",
                table: "BOOKS",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_BookId",
                table: "CARTS",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_CustomerId",
                table: "CARTS",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_BillId",
                table: "ORDERS",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_BookId",
                table: "ORDERS",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_CustomerId",
                table: "ORDERS",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEWS_BookId",
                table: "PRODUCT_REVIEWS",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "UQ_CUSTOMER_BOOK_REVIEW",
                table: "PRODUCT_REVIEWS",
                columns: new[] { "CustomerId", "BookId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BANNERS");

            migrationBuilder.DropTable(
                name: "BOOKDETAILS");

            migrationBuilder.DropTable(
                name: "BOOKIMG");

            migrationBuilder.DropTable(
                name: "CARTS");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "OTPs");

            migrationBuilder.DropTable(
                name: "PRODUCT_REVIEWS");

            migrationBuilder.DropTable(
                name: "CATEGORIES");

            migrationBuilder.DropTable(
                name: "BILLS");

            migrationBuilder.DropTable(
                name: "BOOKS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "VOUCHERS");

            migrationBuilder.DropTable(
                name: "AUTHORS");

            migrationBuilder.DropTable(
                name: "SUPPLIERS");
        }
    }
}
