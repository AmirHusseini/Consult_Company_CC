using Microsoft.EntityFrameworkCore.Migrations;

namespace Consult_Company.Data.Migrations
{
    public partial class last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uppdrags_Projects_ProjectIdProject",
                table: "Uppdrags");

            migrationBuilder.DropIndex(
                name: "IX_Uppdrags_ProjectIdProject",
                table: "Uppdrags");

            migrationBuilder.DropColumn(
                name: "ProjectIdProject",
                table: "Uppdrags");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Projects");

            migrationBuilder.AddColumn<int>(
                name: "ProjectIdProject",
                table: "Uppdrags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Uppdrags_ProjectIdProject",
                table: "Uppdrags",
                column: "ProjectIdProject");

            migrationBuilder.AddForeignKey(
                name: "FK_Uppdrags_Projects_ProjectIdProject",
                table: "Uppdrags",
                column: "ProjectIdProject",
                principalTable: "Projects",
                principalColumn: "IdProject",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
