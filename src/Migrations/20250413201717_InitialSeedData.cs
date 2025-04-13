using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rr_events.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndTimeUtc", "EnhancedExperienceLink", "EnhancedExperienceSoldOut", "EventImageUrl", "IsPrivate", "Location", "StartTimeUtc", "SupportingActsSerialized", "TicketLink", "TicketsSoldOut", "Title", "TourName", "Venue" },
                values: new object[,]
                {
                    { 1, "Opening night of the West Coast tour", new DateTime(2025, 4, 26, 23, 0, 0, 0, DateTimeKind.Utc), null, false, "https://example.com/seattle.png", false, "Seattle, WA", new DateTime(2025, 4, 25, 19, 0, 0, 0, DateTimeKind.Utc), "[\"Local Opener\"]", "https://example.com/seattle-tickets", true, "Rob Rich: Seattle Show", "Dark Roads Tour", "Neptune Theater" },
                    { 2, "Next stop on the tour", new DateTime(2025, 4, 18, 22, 13, 39, 491, DateTimeKind.Utc).AddTicks(8560), "https://example.com/portland-vip", false, "https://example.com/portland.png", false, "Portland, OR", new DateTime(2025, 4, 18, 20, 13, 39, 491, DateTimeKind.Utc).AddTicks(8460), "[\"Special Guest\"]", "https://example.com/portland-tickets", false, "Rob Rich: Portland Show", "Dark Roads Tour", "Crystal Ballroom" }
                });
        }
    }
}
