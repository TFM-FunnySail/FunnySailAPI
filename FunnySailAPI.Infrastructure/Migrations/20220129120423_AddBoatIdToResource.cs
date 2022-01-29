using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatIdToResource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource");

            migrationBuilder.AlterColumn<int>(
                name: "BoatId",
                table: "BoatResource",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource");

            migrationBuilder.AlterColumn<int>(
                name: "BoatId",
                table: "BoatResource",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
