using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace rr_events.Migrations
{
    /// <inheritdoc />
    public partial class SeedEventsWithOwnedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportingActs",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "SupportingActsSerialized",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Description", "EndTimeUtc", "EnhancedExperienceLink", "EnhancedExperienceSoldOut", "EventImageUrl", "IsPrivate", "Location", "StartTimeUtc", "SupportingActsSerialized", "TicketLink", "TicketsSoldOut", "Title", "TourName", "Venue" },
                values: new object[,]
                {
                    { 1, "Opening night of the West Coast tour", new DateTime(2025, 4, 3, 9, 3, 35, 834, DateTimeKind.Utc).AddTicks(7970), null, false, "https://example.com/seattle.png", false, "Seattle, WA", new DateTime(2025, 4, 3, 7, 3, 35, 834, DateTimeKind.Utc).AddTicks(7440), "[\"Local Opener\"]", "https://example.com/seattle-tickets", true, "Rob Rich: Seattle Show", "Dark Roads Tour", "Neptune Theater" },
                    { 2, "Next stop on the tour", new DateTime(2025, 4, 15, 9, 3, 35, 835, DateTimeKind.Utc).AddTicks(1530), "https://example.com/portland-vip", false, "https://example.com/portland.png", false, "Portland, OR", new DateTime(2025, 4, 15, 7, 3, 35, 835, DateTimeKind.Utc).AddTicks(1530), "[\"Special Guest\"]", "https://example.com/portland-tickets", false, "Rob Rich: Portland Show", "Dark Roads Tour", "Crystal Ballroom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "SupportingActsSerialized",
                table: "Events");

            migrationBuilder.AddColumn<List<string>>(
                name: "SupportingActs",
                table: "Events",
                type: "text[]",
                nullable: true);
        }
    }
}
