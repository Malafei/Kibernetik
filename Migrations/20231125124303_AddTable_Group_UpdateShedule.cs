using Microsoft.EntityFrameworkCore.Migrations;

namespace Kibernetik.Migrations
{
    public partial class AddTable_Group_UpdateShedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name_group",
                table: "tblShedules");

            migrationBuilder.AddColumn<int>(
                name: "name_groupid",
                table: "tblShedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tblGroups",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name_group = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblGroups", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblShedules_name_groupid",
                table: "tblShedules",
                column: "name_groupid");

            migrationBuilder.AddForeignKey(
                name: "FK_tblShedules_tblGroups_name_groupid",
                table: "tblShedules",
                column: "name_groupid",
                principalTable: "tblGroups",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblShedules_tblGroups_name_groupid",
                table: "tblShedules");

            migrationBuilder.DropTable(
                name: "tblGroups");

            migrationBuilder.DropIndex(
                name: "IX_tblShedules_name_groupid",
                table: "tblShedules");

            migrationBuilder.DropColumn(
                name: "name_groupid",
                table: "tblShedules");

            migrationBuilder.AddColumn<string>(
                name: "name_group",
                table: "tblShedules",
                type: "TEXT",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
