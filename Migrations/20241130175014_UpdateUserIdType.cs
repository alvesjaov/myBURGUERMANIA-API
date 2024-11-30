using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace myBURGUERMANIA_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36)
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
