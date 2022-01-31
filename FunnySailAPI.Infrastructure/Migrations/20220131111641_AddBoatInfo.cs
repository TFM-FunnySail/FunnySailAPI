using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatInfo",
                columns: table => new
                {
                    BoatId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    Registration = table.Column<string>(maxLength: 50, nullable: false),
                    MooringPoint = table.Column<string>(maxLength: 50, nullable: false),
                    Length = table.Column<decimal>(type: "decimal(9, 2)", nullable: false),
                    Sleeve = table.Column<decimal>(type: "decimal(9, 2)", nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    MotorPower = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatInfo", x => x.BoatId);
                    table.ForeignKey(
                        name: "FK_BoatInfo_Boat_BoatId",
                        column: x => x.BoatId,
                        principalTable: "Boat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoatInfo");
        }
    }
}
