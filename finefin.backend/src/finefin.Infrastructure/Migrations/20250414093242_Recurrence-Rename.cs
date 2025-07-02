using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RecurrenceRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TRANSACTION_TB_RECURRENCY_TRN_RECURRENCY_ID",
                table: "TB_TRANSACTION");

            migrationBuilder.DropTable(
                name: "TB_RECURRENCY");

            migrationBuilder.RenameColumn(
                name: "TRN_RECURRENCY_ID",
                table: "TB_TRANSACTION",
                newName: "TRN_RECURRENCE_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TRANSACTION_TRN_RECURRENCY_ID",
                table: "TB_TRANSACTION",
                newName: "IX_TB_TRANSACTION_TRN_RECURRENCE_ID");

            migrationBuilder.CreateTable(
                name: "TB_RECURRENCE",
                columns: table => new
                {
                    RCR_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RCR_TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RCR_OCCURRENCES = table.Column<int>(type: "int", nullable: false),
                    RCR_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RCR_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RECURRENCE", x => x.RCR_ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TRANSACTION_TB_RECURRENCE_TRN_RECURRENCE_ID",
                table: "TB_TRANSACTION",
                column: "TRN_RECURRENCE_ID",
                principalTable: "TB_RECURRENCE",
                principalColumn: "RCR_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TRANSACTION_TB_RECURRENCE_TRN_RECURRENCE_ID",
                table: "TB_TRANSACTION");

            migrationBuilder.DropTable(
                name: "TB_RECURRENCE");

            migrationBuilder.RenameColumn(
                name: "TRN_RECURRENCE_ID",
                table: "TB_TRANSACTION",
                newName: "TRN_RECURRENCY_ID");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TRANSACTION_TRN_RECURRENCE_ID",
                table: "TB_TRANSACTION",
                newName: "IX_TB_TRANSACTION_TRN_RECURRENCY_ID");

            migrationBuilder.CreateTable(
                name: "TB_RECURRENCY",
                columns: table => new
                {
                    RCR_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RCR_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RCR_OCCURRENCES = table.Column<int>(type: "int", nullable: false),
                    RCR_TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RCR_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_RECURRENCY", x => x.RCR_ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TRANSACTION_TB_RECURRENCY_TRN_RECURRENCY_ID",
                table: "TB_TRANSACTION",
                column: "TRN_RECURRENCY_ID",
                principalTable: "TB_RECURRENCY",
                principalColumn: "RCR_ID");
        }
    }
}
