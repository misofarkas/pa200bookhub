using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeletePurchaseHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Books_BookId",
                table: "PurchaseHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Books_BookId",
                table: "PurchaseHistories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseHistories_Books_BookId",
                table: "PurchaseHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseHistories_Books_BookId",
                table: "PurchaseHistories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
