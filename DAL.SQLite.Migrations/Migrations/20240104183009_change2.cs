using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class change2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "GenreBooks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GenreBooks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GenreBooks",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 1, 1 },
                column: "Id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "GenreBooks",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 2, 2 },
                column: "Id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "GenreBooks",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 3, 3 },
                column: "Id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GenreBooks",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 4, 4 },
                column: "Id",
                value: 4);

            migrationBuilder.UpdateData(
                table: "GenreBooks",
                keyColumns: new[] { "BookId", "GenreId" },
                keyValues: new object[] { 5, 5 },
                column: "Id",
                value: 5);
        }
    }
}
