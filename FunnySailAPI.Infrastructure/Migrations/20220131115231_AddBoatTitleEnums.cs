using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatTitleEnums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatTitlesEnums",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatTitlesEnums", x => x.TitleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredBoatTitle_TitleId",
                table: "RequiredBoatTitle",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitle_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitle",
                column: "TitleId",
                principalTable: "BoatTitlesEnums",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitle_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitle");

            migrationBuilder.DropTable(
                name: "BoatTitlesEnums");

            migrationBuilder.DropIndex(
                name: "IX_RequiredBoatTitle_TitleId",
                table: "RequiredBoatTitle");
        }
    }
}
