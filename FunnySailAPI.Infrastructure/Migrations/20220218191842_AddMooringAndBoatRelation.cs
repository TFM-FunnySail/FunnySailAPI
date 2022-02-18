using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddMooringAndBoatRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoatId",
                table: "Moorings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MooringId",
                table: "Boats",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MooringId",
                table: "Boats");
        }
    }
}
