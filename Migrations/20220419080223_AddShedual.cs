using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kibernetik.Migrations
{
    public partial class AddShedual : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblShedules",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_group = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblShedules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tblLesson",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_lesson = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    teacher = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    type = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    classroom = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    sheduleid = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLesson", x => x.id);
                    table.ForeignKey(
                        name: "FK_tblLesson_tblShedules_sheduleid",
                        column: x => x.sheduleid,
                        principalTable: "tblShedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblLesson_sheduleid",
                table: "tblLesson",
                column: "sheduleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblLesson");

            migrationBuilder.DropTable(
                name: "tblShedules");
        }
    }
}
