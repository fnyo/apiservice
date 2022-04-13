using Microsoft.EntityFrameworkCore.Migrations;

namespace Fnyo.Learn.Jenkins.Migrations
{
    public partial class v003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "class",
                table: "tms_student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "number",
                table: "tms_student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "sex",
                table: "tms_student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "tel",
                table: "tms_student",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "class",
                table: "tms_student");

            migrationBuilder.DropColumn(
                name: "number",
                table: "tms_student");

            migrationBuilder.DropColumn(
                name: "sex",
                table: "tms_student");

            migrationBuilder.DropColumn(
                name: "tel",
                table: "tms_student");
        }
    }
}
