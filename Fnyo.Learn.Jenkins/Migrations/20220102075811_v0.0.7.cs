using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fnyo.Learn.Jenkins.Migrations
{
    public partial class v007 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tms_score",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    student_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    @class = table.Column<int>(name: "class", type: "int", nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    score = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    is_best = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    examine_type = table.Column<int>(type: "int", nullable: false),
                    import_time = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tms_score", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tms_score");
        }
    }
}
