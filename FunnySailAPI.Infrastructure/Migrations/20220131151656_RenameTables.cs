using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boat_BoatType_BoatTypeId",
                table: "Boat");

            migrationBuilder.DropForeignKey(
                name: "FK_Boat_Users_UsersENUserId",
                table: "Boat");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatInfo_Boat_BoatId",
                table: "BoatInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPrices_Boat_BoatId",
                table: "BoatPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitle_Boat_BoatId",
                table: "RequiredBoatTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitle_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitle");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_UserId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredBoatTitle",
                table: "RequiredBoatTitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatType",
                table: "BoatType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boat",
                table: "Boat");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UsersInfo");

            migrationBuilder.RenameTable(
                name: "RequiredBoatTitle",
                newName: "RequiredBoatTitles");

            migrationBuilder.RenameTable(
                name: "BoatType",
                newName: "BoatTypes");

            migrationBuilder.RenameTable(
                name: "BoatResource",
                newName: "BoatResources");

            migrationBuilder.RenameTable(
                name: "Boat",
                newName: "Boats");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredBoatTitle_TitleId",
                table: "RequiredBoatTitles",
                newName: "IX_RequiredBoatTitles_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Boat_UsersENUserId",
                table: "Boats",
                newName: "IX_Boats_UsersENUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Boat_BoatTypeId",
                table: "Boats",
                newName: "IX_Boats_BoatTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredBoatTitles",
                table: "RequiredBoatTitles",
                columns: new[] { "BoatId", "TitleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatTypes",
                table: "BoatTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources",
                columns: new[] { "BoatId", "Uri" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boats",
                table: "Boats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatInfo_Boats_BoatId",
                table: "BoatInfo",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPrices_Boats_BoatId",
                table: "BoatPrices",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatResources_Boats_BoatId",
                table: "BoatResources",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_BoatTypes_BoatTypeId",
                table: "Boats",
                column: "BoatTypeId",
                principalTable: "BoatTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_UsersInfo_UsersENUserId",
                table: "Boats",
                column: "UsersENUserId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitles_Boats_BoatId",
                table: "RequiredBoatTitles",
                column: "BoatId",
                principalTable: "Boats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitles",
                column: "TitleId",
                principalTable: "BoatTitlesEnums",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersInfo_AspNetUsers_UserId",
                table: "UsersInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatInfo_Boats_BoatId",
                table: "BoatInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatPrices_Boats_BoatId",
                table: "BoatPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_BoatResources_Boats_BoatId",
                table: "BoatResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Boats_BoatTypes_BoatTypeId",
                table: "Boats");

            migrationBuilder.DropForeignKey(
                name: "FK_Boats_UsersInfo_UsersENUserId",
                table: "Boats");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitles_Boats_BoatId",
                table: "RequiredBoatTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredBoatTitles_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitles");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersInfo_AspNetUsers_UserId",
                table: "UsersInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersInfo",
                table: "UsersInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequiredBoatTitles",
                table: "RequiredBoatTitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatTypes",
                table: "BoatTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Boats",
                table: "Boats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources");

            migrationBuilder.RenameTable(
                name: "UsersInfo",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "RequiredBoatTitles",
                newName: "RequiredBoatTitle");

            migrationBuilder.RenameTable(
                name: "BoatTypes",
                newName: "BoatType");

            migrationBuilder.RenameTable(
                name: "Boats",
                newName: "Boat");

            migrationBuilder.RenameTable(
                name: "BoatResources",
                newName: "BoatResource");

            migrationBuilder.RenameIndex(
                name: "IX_RequiredBoatTitles_TitleId",
                table: "RequiredBoatTitle",
                newName: "IX_RequiredBoatTitle_TitleId");

            migrationBuilder.RenameIndex(
                name: "IX_Boats_UsersENUserId",
                table: "Boat",
                newName: "IX_Boat_UsersENUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Boats_BoatTypeId",
                table: "Boat",
                newName: "IX_Boat_BoatTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequiredBoatTitle",
                table: "RequiredBoatTitle",
                columns: new[] { "BoatId", "TitleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatType",
                table: "BoatType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Boat",
                table: "Boat",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource",
                columns: new[] { "BoatId", "Uri" });

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_BoatType_BoatTypeId",
                table: "Boat",
                column: "BoatTypeId",
                principalTable: "BoatType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Boat_Users_UsersENUserId",
                table: "Boat",
                column: "UsersENUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatInfo_Boat_BoatId",
                table: "BoatInfo",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatPrices_Boat_BoatId",
                table: "BoatPrices",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoatResource_Boat_BoatId",
                table: "BoatResource",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitle_Boat_BoatId",
                table: "RequiredBoatTitle",
                column: "BoatId",
                principalTable: "Boat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredBoatTitle_BoatTitlesEnums_TitleId",
                table: "RequiredBoatTitle",
                column: "TitleId",
                principalTable: "BoatTitlesEnums",
                principalColumn: "TitleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_UserId",
                table: "Users",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
