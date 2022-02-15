using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class ModActivityAddActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Activity",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Activity");
        }
    }
}
