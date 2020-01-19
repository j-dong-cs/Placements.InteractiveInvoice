using Microsoft.EntityFrameworkCore.Migrations;

namespace Placement.InteractiveInvoice.Migrations
{
    public partial class ChangeBookedAmtToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "BookedAmount",
                table: "LineItems",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "BookedAmount",
                table: "LineItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
