using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddRequireCapitan_To_BoatBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boats_MooringId",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "RequestCaptain",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "RequestCaptain",
                table: "BoatBooking",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Boats_MooringId",
                table: "Boats",
                column: "MooringId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Boats_MooringId",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "RequestCaptain",
                table: "BoatBooking");

            migrationBuilder.AddColumn<bool>(
                name: "RequestCaptain",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Boats_MooringId",
                table: "Boats",
                column: "MooringId");
        }
    }
}
