using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vegist.Migrations
{
    public partial class category_edit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ProductImages",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CategoryId",
                table: "ProductImages",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Categories_CategoryId",
                table: "ProductImages",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Categories_CategoryId",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_CategoryId",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ProductImages");
        }
    }
}
