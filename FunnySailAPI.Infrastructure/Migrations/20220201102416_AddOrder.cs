using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    TotalPeople = table.Column<int>(nullable: false),
                    Paid = table.Column<bool>(nullable: false),
                    RequestCaptain = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    ClientUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_UsersInfo_ClientUserId",
                        column: x => x.ClientUserId,
                        principalTable: "UsersInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderRates",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<decimal>(type: "money", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "money", nullable: false),
                    TasaCurrency = table.Column<decimal>(type: "decimal(9,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRates", x => new { x.OrderId, x.Currency });
                    table.ForeignKey(
                        name: "FK_OrderRates_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientUserId",
                table: "Orders",
                column: "ClientUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRates");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
