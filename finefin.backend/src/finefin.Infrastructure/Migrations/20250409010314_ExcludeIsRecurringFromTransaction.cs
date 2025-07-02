using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finefin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExcludeIsRecurringFromTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TRN_RECURRING",
                table: "TB_TRANSACTION");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TRN_RECURRING",
                table: "TB_TRANSACTION",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
