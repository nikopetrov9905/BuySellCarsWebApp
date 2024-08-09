using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuySellCarsWebApp.Data.Migrations
{
    public partial class RenameMakeToBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Cars");
        }
    }
}
