using Microsoft.EntityFrameworkCore.Migrations;

namespace Placements.InteractiveInvoice.Migrations
{
    public partial class altercolname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Lineitems_LineItemID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitem_Lineitems_InvoiceID",
                table: "InvoiceLineitem");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitem_Invoices_LineitemID",
                table: "InvoiceLineitem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceLineitem",
                table: "InvoiceLineitem");

            migrationBuilder.RenameTable(
                name: "InvoiceLineitem",
                newName: "InvoiceLineitems");

            migrationBuilder.RenameColumn(
                name: "LineItemID",
                table: "Comments",
                newName: "LineitemID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_LineItemID",
                table: "Comments",
                newName: "IX_Comments_LineitemID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLineitem_LineitemID",
                table: "InvoiceLineitems",
                newName: "IX_InvoiceLineitems_LineitemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceLineitems",
                table: "InvoiceLineitems",
                columns: new[] { "InvoiceID", "LineitemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Lineitems_LineitemID",
                table: "Comments",
                column: "LineitemID",
                principalTable: "Lineitems",
                principalColumn: "LineitemID",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Lineitems_LineitemID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Lineitems_InvoiceID",
                table: "InvoiceLineitems");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceLineitems_Invoices_LineitemID",
                table: "InvoiceLineitems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceLineitems",
                table: "InvoiceLineitems");

            migrationBuilder.RenameTable(
                name: "InvoiceLineitems",
                newName: "InvoiceLineitem");

            migrationBuilder.RenameColumn(
                name: "LineitemID",
                table: "Comments",
                newName: "LineItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_LineitemID",
                table: "Comments",
                newName: "IX_Comments_LineItemID");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceLineitems_LineitemID",
                table: "InvoiceLineitem",
                newName: "IX_InvoiceLineitem_LineitemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceLineitem",
                table: "InvoiceLineitem",
                columns: new[] { "InvoiceID", "LineitemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Lineitems_LineItemID",
                table: "Comments",
                column: "LineItemID",
                principalTable: "Lineitems",
                principalColumn: "LineitemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitem_Lineitems_InvoiceID",
                table: "InvoiceLineitem",
                column: "InvoiceID",
                principalTable: "Lineitems",
                principalColumn: "LineitemID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceLineitem_Invoices_LineitemID",
                table: "InvoiceLineitem",
                column: "LineitemID",
                principalTable: "Invoices",
                principalColumn: "InvoiceID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
