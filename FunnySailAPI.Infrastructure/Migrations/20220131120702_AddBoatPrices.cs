using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatPrices",
                columns: table => new
                {
                    BoatId = table.Column<int>(nullable: false),
                    DayBasePrice = table.Column<decimal>(type: "money", nullable: false),
                    HourBasePrice = table.Column<decimal>(type: "money", nullable: false),
                    Supplement = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatPrices", x => x.BoatId);
                    table.ForeignKey(
                        name: "FK_BoatPrices_Boat_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatPrices");
        }
    }
}
