using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_commerceAPI.Migrations
{
    /// <inheritdoc />
    public partial class edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "CartItems",
                newName: "UnitPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "CartItems",
                newName: "Price");
        }
    }
}
