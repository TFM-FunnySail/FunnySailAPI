using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddOwnerID_ToOwnerInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "OwnerInvoice",
                nullable: true);

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
                name: "IX_OwnerInvoice_OwnerId",
                table: "OwnerInvoice",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInvoice_UsersInfo_OwnerId",
                table: "OwnerInvoice",
                column: "OwnerId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInvoice_UsersInfo_OwnerId",
                table: "OwnerInvoice");

            migrationBuilder.DropIndex(
                name: "IX_OwnerInvoice_OwnerId",
                table: "OwnerInvoice");

            migrationBuilder.DeleteData(
                table: "BoatTitlesEnums",
                keyColumn: "TitleId",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "BoatTitlesEnums",
                keyColumn: "TitleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BoatTitlesEnums",
                keyColumn: "TitleId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "OwnerInvoice");
        }
    }
}
