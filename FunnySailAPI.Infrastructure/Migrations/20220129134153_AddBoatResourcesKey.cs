using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddBoatResourcesKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource");

            migrationBuilder.DropIndex(
                name: "IX_BoatResource_BoatId",
                table: "BoatResource");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BoatResource");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "BoatResource",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource",
                columns: new[] { "BoatId", "Uri" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource");

            migrationBuilder.AlterColumn<string>(
                name: "Uri",
                table: "BoatResource",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BoatResource",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResource",
                table: "BoatResource",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BoatResource_BoatId",
                table: "BoatResource",
                column: "BoatId");
        }
    }
}
