using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddAddressDataToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "UsersInfo",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "UsersInfo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "UsersInfo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "UsersInfo",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "UsersInfo",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "City",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "State",
                table: "UsersInfo");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "UsersInfo");
        }
    }
}
