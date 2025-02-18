using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.api.Migrations
{
    /// <inheritdoc />
    public partial class DataModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_RECURRENCY",
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
                    table.PrimaryKey("PK_TB_RECURRENCY", x => x.RCR_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_USERS",
                columns: table => new
                {
                    USR_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    USR_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    USR_EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    USR_PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USR_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    USR_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USERS", x => x.USR_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_WALLET",
                columns: table => new
                {
                    WLT_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WLT_TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WLT_NAME = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    WLT_COLOR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WLT_BALANCE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WLT_LAST_EDIT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WLT_USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WLT_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WLT_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_WALLET", x => x.WLT_ID);
                    table.ForeignKey(
                        name: "FK_TB_WALLET_TB_USERS_WLT_USER_ID",
                        column: x => x.WLT_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "USR_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_TRANSACTION",
                columns: table => new
                {
                    TRN_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TRN_TYPE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TRN_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TRN_DUEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRN_COMPLETED = table.Column<bool>(type: "bit", nullable: false),
                    TRN_FIXED = table.Column<bool>(type: "bit", nullable: false),
                    TRN_RECURRING = table.Column<bool>(type: "bit", nullable: false),
                    TRN_RECURRENCY_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TRN_WALLET_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TRN_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TRN_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TRANSACTION", x => x.TRN_ID);
                    table.ForeignKey(
                        name: "FK_TB_TRANSACTION_TB_RECURRENCY_TRN_RECURRENCY_ID",
                        column: x => x.TRN_RECURRENCY_ID,
                        principalTable: "TB_RECURRENCY",
                        principalColumn: "RCR_ID");
                    table.ForeignKey(
                        name: "FK_TB_TRANSACTION_TB_WALLET_TRN_WALLET_ID",
                        column: x => x.TRN_WALLET_ID,
                        principalTable: "TB_WALLET",
                        principalColumn: "WLT_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TRANSACTION_TRN_RECURRENCY_ID",
                table: "TB_TRANSACTION",
                column: "TRN_RECURRENCY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_TRANSACTION_TRN_WALLET_ID",
                table: "TB_TRANSACTION",
                column: "TRN_WALLET_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TB_WALLET_WLT_USER_ID",
                table: "TB_WALLET",
                column: "WLT_USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TRANSACTION");

            migrationBuilder.DropTable(
                name: "TB_RECURRENCY");

            migrationBuilder.DropTable(
                name: "TB_WALLET");

            migrationBuilder.DropTable(
                name: "TB_USERS");
        }
    }
}
