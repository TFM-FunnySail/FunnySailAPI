using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddTechnicalServKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalServiceBoat",
                table: "TechnicalServiceBoat");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TechnicalServiceBoat",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalServiceBoat",
                table: "TechnicalServiceBoat",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServiceBoat_BoatId",
                table: "TechnicalServiceBoat",
                column: "BoatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalServiceBoat",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalServiceBoat_BoatId",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TechnicalServiceBoat");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalServiceBoat",
                table: "TechnicalServiceBoat",
                columns: new[] { "BoatId", "TechnicalServiceId" });
        }
    }
}
