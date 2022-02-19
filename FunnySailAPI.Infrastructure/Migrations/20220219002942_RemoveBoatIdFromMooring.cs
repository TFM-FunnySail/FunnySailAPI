using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class RemoveBoatIdFromMooring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moorings_Boats_BoatId",
                table: "Moorings");

            migrationBuilder.DropIndex(
                name: "IX_Moorings_BoatId",
                table: "Moorings");

            migrationBuilder.DropColumn(
                name: "BoatId",
                table: "Moorings");

            migrationBuilder.CreateIndex(
                name: "IX_Boats_MooringId",
                table: "Boats",
                column: "MooringId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_Moorings_MooringId",
                table: "Boats",
                column: "MooringId",
                principalTable: "Moorings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_Moorings_MooringId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_MooringId",
                table: "Boats");

            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Moorings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moorings_BoatId",
                table: "Moorings",
                column: "BoatId",
                unique: true,
                filter: "[BoatId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Moorings_Boats_BoatId",
                table: "Moorings",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
