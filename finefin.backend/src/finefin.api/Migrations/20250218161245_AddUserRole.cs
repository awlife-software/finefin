using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USR_NAME",
                table: "TB_USERS");

            migrationBuilder.AlterColumn<decimal>(
                name: "WLT_BALANCE",
                table: "TB_WALLET",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "USR_EMAIL",
                table: "TB_USERS",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "USR_FIRST_NAME",
                table: "TB_USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "USR_LAST_NAME",
                table: "TB_USERS",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TRN_AMOUNT",
                table: "TB_TRANSACTION",
                type: "decimal(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "TB_ROLES",
                columns: table => new
                {
                    RLE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RLE_NAME = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RLE_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RLE_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_ROLES", x => x.RLE_ID);
                });

            migrationBuilder.CreateTable(
                name: "TB_USER_ROLES",
                columns: table => new
                {
                    URL_USER_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    URL_ROLE_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    URL_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    URL_CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    URL_UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER_ROLES", x => new { x.URL_USER_ID, x.URL_ROLE_ID });
                    table.ForeignKey(
                        name: "FK_TB_USER_ROLES_TB_ROLES_URL_ROLE_ID",
                        column: x => x.URL_ROLE_ID,
                        principalTable: "TB_ROLES",
                        principalColumn: "RLE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TB_USER_ROLES_TB_USERS_URL_USER_ID",
                        column: x => x.URL_USER_ID,
                        principalTable: "TB_USERS",
                        principalColumn: "USR_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_USERS_USR_EMAIL",
                table: "TB_USERS",
                column: "USR_EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_ROLES_RLE_NAME",
                table: "TB_ROLES",
                column: "RLE_NAME",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_ROLES_URL_ROLE_ID",
                table: "TB_USER_ROLES",
                column: "URL_ROLE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USER_ROLES");

            migrationBuilder.DropTable(
                name: "TB_ROLES");

            migrationBuilder.DropIndex(
                name: "IX_TB_USERS_USR_EMAIL",
                table: "TB_USERS");

            migrationBuilder.DropColumn(
                name: "USR_FIRST_NAME",
                table: "TB_USERS");

            migrationBuilder.DropColumn(
                name: "USR_LAST_NAME",
                table: "TB_USERS");

            migrationBuilder.AlterColumn<decimal>(
                name: "WLT_BALANCE",
                table: "TB_WALLET",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<string>(
                name: "USR_EMAIL",
                table: "TB_USERS",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddColumn<string>(
                name: "USR_NAME",
                table: "TB_USERS",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "TRN_AMOUNT",
                table: "TB_TRANSACTION",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldPrecision: 18,
                oldScale: 4);
        }
    }
}
