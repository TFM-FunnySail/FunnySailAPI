using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class addOwnerEntityToBoat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_UsersInfo_UsersENUserId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_UsersENUserId",
                table: "Boats");

            migrationBuilder.DropColumn(
                name: "UsersENUserId",
                table: "Boats");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Boats",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boats_OwnerId",
                table: "Boats",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_UsersInfo_OwnerId",
                table: "Boats",
                column: "OwnerId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boats_UsersInfo_OwnerId",
                table: "Boats");

            migrationBuilder.DropIndex(
                name: "IX_Boats_OwnerId",
                table: "Boats");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Boats",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsersENUserId",
                table: "Boats",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boats_UsersENUserId",
                table: "Boats",
                column: "UsersENUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boats_UsersInfo_UsersENUserId",
                table: "Boats",
                column: "UsersENUserId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
