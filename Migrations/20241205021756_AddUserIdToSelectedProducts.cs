using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBURGUERMANIA_API.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToSelectedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SelectedProducts",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SelectedProducts");
        }
    }
}
