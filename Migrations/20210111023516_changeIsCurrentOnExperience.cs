using Microsoft.EntityFrameworkCore.Migrations;

namespace ResumeMvcCore.Migrations
{
    public partial class changeIsCurrentOnExperience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Experiences",
                newName: "IsCurrent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsCurrent",
                table: "Experiences",
                newName: "Status");
        }
    }
}
