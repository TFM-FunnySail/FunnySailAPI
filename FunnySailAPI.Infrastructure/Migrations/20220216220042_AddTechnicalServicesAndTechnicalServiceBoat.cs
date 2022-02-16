using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddTechnicalServicesAndTechnicalServiceBoat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TechnicalServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalServiceBoat",
                columns: table => new
                {
                    BoatId = table.Column<int>(nullable: false),
                    TechnicalServiceId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ServiceDate = table.Column<DateTime>(nullable: false),
                    Done = table.Column<bool>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalServiceBoat", x => new { x.BoatId, x.TechnicalServiceId });
                    table.ForeignKey(
                        name: "FK_TechnicalServiceBoat_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalServiceBoat_TechnicalServices_TechnicalServiceId",
                        column: x => x.TechnicalServiceId,
                        principalTable: "TechnicalServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServiceBoat_TechnicalServiceId",
                table: "TechnicalServiceBoat",
                column: "TechnicalServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TechnicalServiceBoat");

            migrationBuilder.DropTable(
                name: "TechnicalServices");
        }
    }
}
