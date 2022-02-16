using Microsoft.EntityFrameworkCore.Migrations;

namespace FunnySailAPI.Infrastructure.Migrations
{
    public partial class AddResourcesAndActivityResources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources");

            migrationBuilder.DropColumn(
                name: "Uri",
                table: "BoatResources");

            migrationBuilder.DropColumn(
                name: "Main",
                table: "BoatResources");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "BoatResources");

            migrationBuilder.AddColumn<int>(
                name: "ResourceId",
                table: "BoatResources",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources",
                columns: new[] { "BoatId", "ResourceId" });

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uri = table.Column<string>(nullable: false),
                    Main = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityResources",
                columns: table => new
                {
                    ActivityId = table.Column<int>(nullable: false),
                    ResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityResources", x => new { x.ActivityId, x.ResourceId });
                    table.ForeignKey(
                        name: "FK_ActivityResources_Activity_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityResources_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoatResources_ResourceId",
                table: "BoatResources",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityResources_ResourceId",
                table: "ActivityResources",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoatResources_Resources_ResourceId",
                table: "BoatResources",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoatResources_Resources_ResourceId",
                table: "BoatResources");

            migrationBuilder.DropTable(
                name: "ActivityResources");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources");

            migrationBuilder.DropIndex(
                name: "IX_BoatResources_ResourceId",
                table: "BoatResources");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "BoatResources");

            migrationBuilder.AddColumn<string>(
                name: "Uri",
                table: "BoatResources",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Main",
                table: "BoatResources",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "BoatResources",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoatResources",
                table: "BoatResources",
                columns: new[] { "BoatId", "Uri" });
        }
    }
}
