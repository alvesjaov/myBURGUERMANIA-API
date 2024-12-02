using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBURGUERMANIA_API.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderHistoryToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remover a criação do índice duplicado
            // migrationBuilder.CreateIndex(
            //     name: "IX_Orders_UserId",
            //     table: "Orders",
            //     column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                table: "Orders");

            // Remover a exclusão do índice duplicado
            // migrationBuilder.DropIndex(
            //     name: "IX_Orders_UserId",
            //     table: "Orders");
        }
    }
}
