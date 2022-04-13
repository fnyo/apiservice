using Microsoft.EntityFrameworkCore.Migrations;

namespace Fnyo.Learn.Jenkins.Migrations
{
    public partial class v010 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "real_name",
                table: "tms_user",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "telephone",
                table: "tms_user",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "real_name",
                table: "tms_user");

            migrationBuilder.DropColumn(
                name: "telephone",
                table: "tms_user");
        }
    }
}
