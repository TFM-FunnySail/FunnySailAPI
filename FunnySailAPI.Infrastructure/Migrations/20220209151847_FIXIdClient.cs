using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class FIXIdClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_UsersInfo_ClientUserId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_UsersInfo_ClientUserId",
                table: "ClientInvoice");

            migrationBuilder.DropIndex(
                name: "IX_ClientInvoice_ClientUserId",
                table: "ClientInvoice");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClientUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "ClientInvoice");

            migrationBuilder.DropColumn(
                name: "ClientUserId",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "ClientInvoice",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "Bookings",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInvoice_ClientId",
                table: "ClientInvoice",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_UsersInfo_ClientId",
                table: "Bookings",
                column: "ClientId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_UsersInfo_ClientId",
                table: "ClientInvoice",
                column: "ClientId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_UsersInfo_ClientId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientInvoice_UsersInfo_ClientId",
                table: "ClientInvoice");

            migrationBuilder.DropIndex(
                name: "IX_ClientInvoice_ClientId",
                table: "ClientInvoice");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "ClientInvoice",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientUserId",
                table: "ClientInvoice",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClientUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInvoice_ClientUserId",
                table: "ClientInvoice",
                column: "ClientUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientUserId",
                table: "Bookings",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_UsersInfo_ClientUserId",
                table: "Bookings",
                column: "ClientUserId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientInvoice_UsersInfo_ClientUserId",
                table: "ClientInvoice",
                column: "ClientUserId",
                principalTable: "UsersInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
