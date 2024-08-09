using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuySellCarsWebApp.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CarParts",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Brake Pad", 50.00m },
                    { 2, "Oil Filter", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "Milage", "Model", "Price", "Year" },
                values: new object[,]
                {
                    { 1, "Toyota", 0, "Camry", 0m, 2020 },
                    { 2, "Honda", 0, "Civic", 0m, 2021 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "UserId", "UserId1" },
                values: new object[] { 1, new DateTime(2024, 8, 9, 21, 23, 10, 955, DateTimeKind.Local).AddTicks(882), 1, null });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CarId", "OrderId", "OrderItemType" },
                values: new object[] { 1, 1, 1, "CarOrderItem" });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CarPartId", "OrderId", "OrderItemType" },
                values: new object[] { 2, 1, 1, "CarPartOrderItem" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CarParts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
