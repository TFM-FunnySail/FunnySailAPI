using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class FixTechnicalServiceTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalServiceBoat_TechnicalServices_TechnicalServiceId",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalServices",
                table: "TechnicalServices");

            migrationBuilder.RenameTable(
                name: "TechnicalServices",
                newName: "TechnicalService");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalService",
                table: "TechnicalService",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalServiceBoat_TechnicalService_TechnicalServiceId",
                table: "TechnicalServiceBoat",
                column: "TechnicalServiceId",
                principalTable: "TechnicalService",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalServiceBoat_TechnicalService_TechnicalServiceId",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TechnicalService",
                table: "TechnicalService");

            migrationBuilder.RenameTable(
                name: "TechnicalService",
                newName: "TechnicalServices");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TechnicalServices",
                table: "TechnicalServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalServiceBoat_TechnicalServices_TechnicalServiceId",
                table: "TechnicalServiceBoat",
                column: "TechnicalServiceId",
                principalTable: "TechnicalServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
