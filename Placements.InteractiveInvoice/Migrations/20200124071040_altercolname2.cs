using Microsoft.EntityFrameworkCore.Migrations;

namespace Placements.InteractiveInvoice.Migrations
{
    public partial class altercolname2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineItemName",
                table: "Lineitems",
                newName: "LineitemName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LineitemName",
                table: "Lineitems",
                newName: "LineItemName");
        }
    }
}
