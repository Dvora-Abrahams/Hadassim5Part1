using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class AddOrderQuantityNumToOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderQuantityNum",
                table: "Orders", 
                type: "int", 
                nullable: false, 
                defaultValue: 0); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderQuantityNum", 
                table: "Orders"); 
        }
    }
}
