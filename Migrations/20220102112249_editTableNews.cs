using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kibernetik.Migrations
{
    public partial class editTableNews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "tblNews",
                type: "TEXT",
                nullable: false,
                defaultValue: DateTime.Now);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "tblNews");
        }
    }
}
