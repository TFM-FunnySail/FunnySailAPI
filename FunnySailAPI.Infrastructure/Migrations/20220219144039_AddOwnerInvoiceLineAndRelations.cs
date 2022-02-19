using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddOwnerInvoiceLineAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerInvoiceId",
                table: "TechnicalServiceBoat",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OwnerInvoiceLine",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    OwnerInvoiceId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OwnerInvoiceLine", x => new { x.BookingId, x.OwnerInvoiceId });
                    table.ForeignKey(
                        name: "FK_OwnerInvoiceLine_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OwnerInvoiceLine_OwnerInvoice_OwnerInvoiceId",
                        column: x => x.OwnerInvoiceId,
                        principalTable: "OwnerInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServiceBoat_OwnerInvoiceId",
                table: "TechnicalServiceBoat",
                column: "OwnerInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInvoiceLine_BookingId",
                table: "OwnerInvoiceLine",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OwnerInvoiceLine_OwnerInvoiceId",
                table: "OwnerInvoiceLine",
                column: "OwnerInvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalServiceBoat_OwnerInvoice_OwnerInvoiceId",
                table: "TechnicalServiceBoat",
                column: "OwnerInvoiceId",
                principalTable: "OwnerInvoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalServiceBoat_OwnerInvoice_OwnerInvoiceId",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropTable(
                name: "OwnerInvoiceLine");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalServiceBoat_OwnerInvoiceId",
                table: "TechnicalServiceBoat");

            migrationBuilder.DropColumn(
                name: "OwnerInvoiceId",
                table: "TechnicalServiceBoat");
        }
    }
}
