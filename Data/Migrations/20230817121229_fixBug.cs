using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eTickets.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "ShoppingCartItems",
                newName: "movieId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_MovieId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_movieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Movies_movieId",
                table: "ShoppingCartItems",
                column: "movieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Movies_movieId",
                table: "ShoppingCartItems");

            migrationBuilder.RenameColumn(
                name: "movieId",
                table: "ShoppingCartItems",
                newName: "MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCartItems_movieId",
                table: "ShoppingCartItems",
                newName: "IX_ShoppingCartItems_MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Movies_MovieId",
                table: "ShoppingCartItems",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
