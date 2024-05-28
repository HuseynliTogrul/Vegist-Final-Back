using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vegist.Migrations
{
    public partial class Produc_And_Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_SliderId",
                table: "ProductImages",
                column: "SliderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Sliders_SliderId",
                table: "ProductImages",
                column: "SliderId",
                principalTable: "Sliders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Sliders_SliderId",
                table: "ProductImages");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_SliderId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "SliderId",
                table: "ProductImages");
        }
    }
}
