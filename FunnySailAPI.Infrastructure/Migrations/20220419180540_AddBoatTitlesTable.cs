using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatTitlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.DropTable(
                name: "BoatTitlesEnums");

            migrationBuilder.DropIndex(
                name: "IX_RequiredBoatTitles_TitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.AddColumn<int>(
                name: "BoatTitlesTitleId",
                table: "RequiredBoatTitles",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoatTitles",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatTitles", x => x.TitleId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredBoatTitles_BoatTitlesTitleId",
                table: "RequiredBoatTitles",
                column: "BoatTitlesTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInvoiceLine_OwnerId",
                table: "OwnerInvoiceLine",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInvoiceLine_UsersInfo_OwnerId",
                table: "OwnerInvoiceLine",
                column: "OwnerId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitles_BoatTitlesTitleId",
                table: "RequiredBoatTitles",
                column: "BoatTitlesTitleId",
                principalTable: "BoatTitles",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInvoiceLine_UsersInfo_OwnerId",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitles_BoatTitlesTitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.DropTable(
                name: "BoatTitles");

            migrationBuilder.DropIndex(
                name: "IX_RequiredBoatTitles_BoatTitlesTitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.DropIndex(
                name: "IX_OwnerInvoiceLine_OwnerId",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropColumn(
                name: "BoatTitlesTitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.CreateTable(
                name: "BoatTitlesEnums",
                columns: table => new
                {
                    TitleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatTitlesEnums", x => x.TitleId);
                });

            migrationBuilder.InsertData(
                table: "BoatTitlesEnums",
                columns: new[] { "TitleId", "Description", "Name" },
                values: new object[] { 1, "Titulación de capitanía", "Captaincy" });

            migrationBuilder.InsertData(
                table: "BoatTitlesEnums",
                columns: new[] { "TitleId", "Description", "Name" },
                values: new object[] { 2, "Licencia de navegación", "NavigationLicence" });

            migrationBuilder.InsertData(
                table: "BoatTitlesEnums",
                columns: new[] { "TitleId", "Description", "Name" },
                values: new object[] { 0, "Titulación de patron/a de embarcaciones", "Patronja" });

            migrationBuilder.CreateIndex(
                name: "IX_RequiredBoatTitles_TitleId",
                table: "RequiredBoatTitles",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitles",
                column: "TitleId",
                principalTable: "BoatTitlesEnums",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
