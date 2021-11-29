using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridge.Migrations
{
    public partial class SetPasswordForProductAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Update Users set Password='adminPassword' where email='productadmin@shopbridge.in' and name='Admin'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
