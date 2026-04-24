using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FixItNow.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedRequestId = table.Column<int>(type: "int", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: true),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetWorkerIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RejectedWorkerIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBroadcast = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    WorkerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    WorkerProfileId = table.Column<int>(type: "int", nullable: true),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOrStore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvatarUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerProfiles", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "CreatedAt", "Email", "FullName", "PasswordHash", "Phone", "Role", "WorkerProfileId" },
                values: new object[,]
                {
                    { 101, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Nguy?n Van A", "123456", "0900000001", 1, 101 },
                    { 102, null, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Tr?n Th? B", "123456", "0900000002", 1, 102 },
                    { 103, null, new DateTime(2025, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Ph?m C", "123456", "0900000003", 1, 103 },
                    { 104, null, new DateTime(2025, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Lê Van D", "123456", "0900000004", 1, 104 },
                    { 105, null, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Ph?m Van E", "123456", "0900000005", 1, 105 },
                    { 106, null, new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Khách Hàng M?t", "123456", "0900000006", 0, null },
                    { 107, null, new DateTime(2025, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Khách Hàng Hai", "123456", "0900000007", 0, null },
                    { 108, null, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Khách Hàng Ba", "123456", "0900000008", 0, null },
                    { 109, null, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Khách Hàng B?n", "123456", "0900000009", 0, null },
                    { 110, null, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Khách Hàng Nam", "123456", "0900000010", 0, null }
                });

            migrationBuilder.InsertData(
                table: "WorkerProfiles",
                columns: new[] { "Id", "Address", "AvatarUrl", "Description", "IsActive", "Location", "NameOrStore", "PhoneNumber", "Rating", "Services" },
                values: new object[,]
                {
                    { 101, "123 Lê L?i, Qu?n 1", null, "S?a di?n nhanh chóng, an toàn", true, "H? Chí Minh", "Th? Ði?n Nguy?n Van A", "0900000001", 4.7999999999999998, "S?a di?n,Máy l?nh" },
                    { 102, "456 Tr?n Hung Ð?o, Hoàn Ki?m", null, "Chuyên s?a ?ng nu?c, vòi sen", true, "Hà N?i", "Tr?n Th? B D?ch V? Nu?c", "0900000002", 4.5, "S?a nu?c" },
                    { 103, "789 Nguy?n Van Linh, H?i Châu", null, "Thay l?c t? l?nh, ki?m tra tivi", true, "Ðà N?ng", "C?a hàng S?a Ch?a C", "0900000003", 5.0, "Ði?n gia d?ng,Máy l?nh" },
                    { 104, "101 Lý T? Tr?ng, Ninh Ki?u", null, "Nh?n s?a m?i th? trong nhà", true, "C?n Tho", "Lê Van D - Ða nang", "0900000004", 4.2000000000000002, "S?a di?n,S?a nu?c,Ði?n gia d?ng" },
                    { 105, "202 Ð?ng Kh?i, Biên Hòa", null, "Chuyên s?a c?a g?, t? g?", true, "Ð?ng Nai", "Ph?m Van E M?c", "0900000005", 4.7000000000000002, "Khác" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RepairRequests");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WorkerProfiles");
        }
    }
}
