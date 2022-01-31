using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddRequiredBoatTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequiredBoatTitle",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false),
                    BoatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredBoatTitle", x => new { x.BoatId, x.TitleId });
                    table.ForeignKey(
                        name: "FK_RequiredBoatTitle_Boat_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequiredBoatTitle");
        }
    }
}
