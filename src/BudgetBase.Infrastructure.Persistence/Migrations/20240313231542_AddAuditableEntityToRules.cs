using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditableEntityToRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TransactionRulesGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TransactionRulesGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TransactionRulesGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TransactionRulesGroups",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TransactionRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "TransactionRules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "TransactionRules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "TransactionRules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TransactionRulesGroups");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TransactionRulesGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TransactionRulesGroups");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TransactionRulesGroups");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TransactionRules");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "TransactionRules");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "TransactionRules");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "TransactionRules");
        }
    }
}
