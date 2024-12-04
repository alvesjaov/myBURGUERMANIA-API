using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBURGUERMANIA_API.Migrations
{
    /// <inheritdoc />
    public partial class AddSelectedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductIds",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "SelectedProductsId",
                table: "Orders",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SelectedProducts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ProductIds = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelectedProducts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SelectedProductsId",
                table: "Orders",
                column: "SelectedProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_SelectedProducts_SelectedProductsId",
                table: "Orders",
                column: "SelectedProductsId",
                principalTable: "SelectedProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_SelectedProducts_SelectedProductsId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "SelectedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Orders_SelectedProductsId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SelectedProductsId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "ProductIds",
                table: "Orders",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "");
        }
    }
}
