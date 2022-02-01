using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddClientInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientInvoiceId",
                table: "InvoiceLines",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ClientInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    Canceled = table.Column<bool>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    ClientId = table.Column<int>(nullable: false),
                    ClientUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientInvoice_UsersInfo_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "UsersInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLines_ClientInvoiceId",
                table: "InvoiceLines",
                column: "ClientInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientInvoice_ClientUserId",
                table: "ClientInvoice",
                column: "ClientUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLines_ClientInvoice_ClientInvoiceId",
                table: "InvoiceLines",
                column: "ClientInvoiceId",
                principalTable: "ClientInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLines_ClientInvoice_ClientInvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropTable(
                name: "ClientInvoice");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceLines_ClientInvoiceId",
                table: "InvoiceLines");

            migrationBuilder.DropColumn(
                name: "ClientInvoiceId",
                table: "InvoiceLines");
        }
    }
}
