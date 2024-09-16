using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Colors_ColorId",
                table: "Variants");

            migrationBuilder.DropForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_Variants_ColorId",
                table: "Variants");

            migrationBuilder.DropIndex(
                name: "IX_Variants_SizeId",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Variants");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Variants");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Variants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Variants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Variants_ColorId",
                table: "Variants",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_SizeId",
                table: "Variants",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Colors_ColorId",
                table: "Variants",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Variants_Sizes_SizeId",
                table: "Variants",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
