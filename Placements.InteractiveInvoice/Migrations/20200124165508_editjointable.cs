using Microsoft.EntityFrameworkCore.Migrations;

namespace Placements.InteractiveInvoice.Migrations
{
    public partial class editjointable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Lineitems_InvoiceID",
                table: "InvoiceLineitems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Invoices_LineitemID",
                table: "InvoiceLineitems");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitems_Invoices_InvoiceID",
                table: "InvoiceLineitems",
                column: "InvoiceID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitems_Lineitems_LineitemID",
                table: "InvoiceLineitems",
                column: "LineitemID",
                principalTable: "Lineitems",
                principalColumn: "LineitemID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Invoices_InvoiceID",
                table: "InvoiceLineitems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Lineitems_LineitemID",
                table: "InvoiceLineitems");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitems_Lineitems_InvoiceID",
                table: "InvoiceLineitems",
                column: "InvoiceID",
                principalTable: "Lineitems",
                principalColumn: "LineitemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitems_Invoices_LineitemID",
                table: "InvoiceLineitems",
                column: "LineitemID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
