using Microsoft.EntityFrameworkCore.Migrations;

namespace Placements.InteractiveInvoice.Migrations
{
    public partial class addInvoiceNameCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceName",
                table: "Invoices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceName",
                table: "Invoices");
        }
    }
}
