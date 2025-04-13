using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rr_events.Migrations
{
    /// <inheritdoc />
    public partial class SeedStaticEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTimeUtc", "StartTimeUtc" },
                values: new object[] { new DateTime(2025, 4, 26, 23, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 25, 19, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTimeUtc", "StartTimeUtc" },
                values: new object[] { new DateTime(2025, 4, 18, 22, 13, 39, 491, DateTimeKind.Utc).AddTicks(8560), new DateTime(2025, 4, 18, 20, 13, 39, 491, DateTimeKind.Utc).AddTicks(8460) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndTimeUtc", "StartTimeUtc" },
                values: new object[] { new DateTime(2025, 4, 3, 9, 3, 35, 834, DateTimeKind.Utc).AddTicks(7970), new DateTime(2025, 4, 3, 7, 3, 35, 834, DateTimeKind.Utc).AddTicks(7440) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndTimeUtc", "StartTimeUtc" },
                values: new object[] { new DateTime(2025, 4, 15, 9, 3, 35, 835, DateTimeKind.Utc).AddTicks(1530), new DateTime(2025, 4, 15, 7, 3, 35, 835, DateTimeKind.Utc).AddTicks(1530) });
        }
    }
}
