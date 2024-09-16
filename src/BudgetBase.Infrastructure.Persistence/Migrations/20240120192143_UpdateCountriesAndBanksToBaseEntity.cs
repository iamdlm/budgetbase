using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCountriesAndBanksToBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Banks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Countries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Countries",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Banks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Banks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Banks",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Banks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
