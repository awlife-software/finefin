using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.api.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionCompletionDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TRN_COMPLETION_DATE",
                table: "TB_TRANSACTION",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRN_COMPLETION_DATE",
                table: "TB_TRANSACTION");
        }
    }
}
