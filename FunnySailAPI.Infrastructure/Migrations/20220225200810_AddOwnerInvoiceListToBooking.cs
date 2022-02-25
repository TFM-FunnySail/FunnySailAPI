using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddOwnerInvoiceListToBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInvoiceLine_OwnerInvoice_OwnerInvoiceId",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerInvoiceLine",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_OwnerInvoiceLine_BookingId",
                table: "OwnerInvoiceLine");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerInvoiceId",
                table: "OwnerInvoiceLine",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "OwnerInvoiceLine",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerInvoiceLine",
                table: "OwnerInvoiceLine",
                columns: new[] { "BookingId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInvoiceLine_OwnerInvoice_OwnerInvoiceId",
                table: "OwnerInvoiceLine",
                column: "OwnerInvoiceId",
                principalTable: "OwnerInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OwnerInvoiceLine_OwnerInvoice_OwnerInvoiceId",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OwnerInvoiceLine",
                table: "OwnerInvoiceLine");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "OwnerInvoiceLine");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerInvoiceId",
                table: "OwnerInvoiceLine",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OwnerInvoiceLine",
                table: "OwnerInvoiceLine",
                columns: new[] { "BookingId", "OwnerInvoiceId" });

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInvoiceLine_BookingId",
                table: "OwnerInvoiceLine",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OwnerInvoiceLine_OwnerInvoice_OwnerInvoiceId",
                table: "OwnerInvoiceLine",
                column: "OwnerInvoiceId",
                principalTable: "OwnerInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
