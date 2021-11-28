using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopBridge.Migrations
{
    public partial class InitializeDefaultData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO UserTypes (Type) VALUES ('ProductAdmin') ");
            migrationBuilder.Sql("INSERT INTO UserTypes (Type) VALUES ('Employee') ");
            migrationBuilder.Sql("INSERT INTO UserTypes (Type) VALUES ('EndUser') ");

            migrationBuilder.Sql("INSERT INTO Users (Name,Email,IsActive,UserTypeId) " +
                "VALUES ('Admin','productadmin@shopbridge.in',1,(SELECT Id FROM UserTypes WHERE Type='ProductAdmin'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM UserTypes where Type in ('ProductAdmin','Employee','EndUser')");
        }
    }
}
