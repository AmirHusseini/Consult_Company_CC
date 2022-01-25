using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Consult_Company.Data.Migrations
{
    public partial class addedprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uppdrags",
                columns: table => new
                {
                    IdTask = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    TaskRoles = table.Column<int>(type: "int", nullable: false),
                    ProjectIdProject = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uppdrags", x => x.IdTask);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    IdProject = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTask = table.Column<int>(type: "int", nullable: false),
                    TaskIdTask = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.IdProject);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_IdentUserId",
                        column: x => x.IdentUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projects_Uppdrags_TaskIdTask",
                        column: x => x.TaskIdTask,
                        principalTable: "Uppdrags",
                        principalColumn: "IdTask",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_IdentUserId",
                table: "Projects",
                column: "IdentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TaskIdTask",
                table: "Projects",
                column: "TaskIdTask");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Uppdrags_TaskIdTask",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Uppdrags");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
