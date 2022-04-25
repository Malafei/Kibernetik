using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kibernetik.Migrations
{
    public partial class editShedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "tblShedules");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "tblLesson",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "tblLesson");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "tblShedules",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
