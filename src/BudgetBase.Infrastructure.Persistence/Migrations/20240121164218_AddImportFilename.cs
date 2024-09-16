using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetBase.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddImportFilename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Filename",
                table: "Imports",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filename",
                table: "Imports");
        }
    }
}
