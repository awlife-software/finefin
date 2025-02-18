using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.api.Migrations
{
    /// <inheritdoc />
    public partial class RemovingIdFromUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL_ID",
                table: "TB_USER_ROLES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "URL_ID",
                table: "TB_USER_ROLES",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
