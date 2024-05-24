using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vegist.Migrations
{
    public partial class add_slider_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "SliderId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
