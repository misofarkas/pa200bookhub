using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class NewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Agatha Christie" },
                    { 5, "Stephen King" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Password", "Username" },
                values: new object[,]
                {
                    { 2, "annaBsecure456", "AnnaB" },
                    { 3, "mikeWpassword789", "MikeW" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Mystery" },
                    { 5, "Horror" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Simon & Schuster" },
                    { 5, "Scholastic" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "Description", "GenreId", "Price", "PublisherId", "Title" },
                values: new object[,]
                {
                    { 4, 4, "Murder on the Orient Express is a detective novel by English writer Agatha Christie.", 4, 18.99m, 4, "Murder on the Orient Express" },
                    { 5, 5, "The Shining is a horror novel by American author Stephen King.", 5, 22.99m, 5, "The Shining" }
                });

            migrationBuilder.InsertData(
                table: "PurchaseHistories",
                columns: new[] { "Id", "BookId", "CustomerId", "PurchaseDate" },
                values: new object[,]
                {
                    { 3, 4, 2, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, 3, new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CustomerId", "Rating" },
                values: new object[,]
                {
                    { 3, 4, "Intriguing and thrilling!", 2, 4 },
                    { 4, 5, "Scary but a bit too long.", 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Wishlists",
                columns: new[] { "Id", "BookId", "CustomerId" },
                values: new object[,]
                {
                    { 2, 4, 2 },
                    { 3, 5, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Wishlists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wishlists",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
