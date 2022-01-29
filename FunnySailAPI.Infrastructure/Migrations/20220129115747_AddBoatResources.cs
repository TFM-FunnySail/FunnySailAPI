using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatResource",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BoatId = table.Column<int>(nullable: true),
                    Uri = table.Column<string>(nullable: false),
                    Main = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoatResource_Boat_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatResource_BoatId",
                table: "BoatResource",
                column: "BoatId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatResource");
        }
    }
}
