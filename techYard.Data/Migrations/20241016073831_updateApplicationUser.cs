using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace techYard.Data.Migrations
{
    public partial class updateApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNumber",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BankName",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BeneficiaryName",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Education",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "academicSpecialization",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "yearsExperience",
                schema: "dbo",
                table: "Users");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BeneficiaryName",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "academicSpecialization",
                schema: "dbo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "yearsExperience",
                schema: "dbo",
                table: "Users",
                type: "int",
                nullable: true);

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
    }
}
