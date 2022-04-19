using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgManager.Repository.Migrations
{
    public partial class AddingFunctionToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Function",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Function",
                table: "AspNetUsers");
        }
    }
}
