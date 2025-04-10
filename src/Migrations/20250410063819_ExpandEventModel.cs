using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rr_events.Migrations
{
    /// <inheritdoc />
    public partial class ExpandEventModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnhancedExperienceLink",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnhancedExperienceSoldOut",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EventImageUrl",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FanClubPresale_AccessCode",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FanClubPresale_EndUtc",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FanClubPresale_StartUtc",
                table: "Events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "SupportingActs",
                table: "Events",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TicketsSoldOut",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TourName",
                table: "Events",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Venue",
                table: "Events",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnhancedExperienceLink",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EnhancedExperienceSoldOut",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventImageUrl",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FanClubPresale_AccessCode",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FanClubPresale_EndUtc",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FanClubPresale_StartUtc",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SupportingActs",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TicketsSoldOut",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "TourName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Venue",
                table: "Events");
        }
    }
}
