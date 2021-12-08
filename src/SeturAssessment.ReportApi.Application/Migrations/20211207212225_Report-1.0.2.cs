using Microsoft.EntityFrameworkCore.Migrations;

namespace SeturAssessment.ReportApi.Application.Migrations
{
    public partial class Report102 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "Reports",
                type: "text",
                maxLength: 2147483647,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 2147483647);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                table: "Reports",
                type: "text",
                maxLength: 2147483647,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldMaxLength: 2147483647,
                oldNullable: true);
        }
    }
}
