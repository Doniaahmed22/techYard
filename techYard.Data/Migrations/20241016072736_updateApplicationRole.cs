using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class updateApplicationRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArName",
                schema: "dbo",
                table: "Role");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Role",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Role",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "ArName",
                schema: "dbo",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
