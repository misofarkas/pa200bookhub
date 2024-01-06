using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.SQLite.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class PurchaseHistoryStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "PurchaseHistories",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "PurchaseHistories",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Paid", "TotalPrice" },
                values: new object[] { false, 15.99m });

            migrationBuilder.UpdateData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Paid", "TotalPrice" },
                values: new object[] { true, 25.99m });

            migrationBuilder.UpdateData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Paid", "TotalPrice" },
                values: new object[] { true, 18.99m });

            migrationBuilder.UpdateData(
                table: "PurchaseHistories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Paid", "TotalPrice" },
                values: new object[] { true, 22.99m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "PurchaseHistories");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "PurchaseHistories");
        }
    }
}
