using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class FIXAliasToMooring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Moorings");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Moorings",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Moorings");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Moorings",
                type: "int",
                maxLength: 100,
                nullable: false,
                defaultValue: 0);
        }
    }
}
