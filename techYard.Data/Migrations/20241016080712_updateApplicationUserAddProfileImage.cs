using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class updateApplicationUserAddProfileImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImagePath",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImagePath",
                schema: "dbo",
                table: "Users");
        }
    }
}
