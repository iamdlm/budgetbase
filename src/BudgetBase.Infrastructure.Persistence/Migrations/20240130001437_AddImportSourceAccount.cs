using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImportSourceAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SourceAccountId",
                table: "Imports",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imports_SourceAccountId",
                table: "Imports",
                column: "SourceAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imports_Accounts_SourceAccountId",
                table: "Imports",
                column: "SourceAccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imports_Accounts_SourceAccountId",
                table: "Imports");

            migrationBuilder.DropIndex(
                name: "IX_Imports_SourceAccountId",
                table: "Imports");

            migrationBuilder.DropColumn(
                name: "SourceAccountId",
                table: "Imports");
        }
    }
}
