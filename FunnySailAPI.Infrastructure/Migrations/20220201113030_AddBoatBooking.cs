using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatBooking",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    BoatId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatBooking", x => new { x.BookingId, x.BoatId });
                    table.ForeignKey(
                        name: "FK_BoatBooking_Boats_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoatBooking_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatBooking_BoatId",
                table: "BoatBooking",
                column: "BoatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatBooking");
        }
    }
}
