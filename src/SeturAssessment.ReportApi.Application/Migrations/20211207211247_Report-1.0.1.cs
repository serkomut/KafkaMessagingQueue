using Microsoft.EntityFrameworkCore.Migrations;

namespace SeturAssessment.ReportApi.Application.Migrations
{
    public partial class Report101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Status",
                table: "Reports",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reports");
        }
    }
}
