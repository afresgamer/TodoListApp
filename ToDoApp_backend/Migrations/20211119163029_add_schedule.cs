using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp_backend.Migrations
{
    public partial class add_schedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageTime = table.Column<int>(type: "int", nullable: false),
                    LineFlg = table.Column<bool>(type: "bit", nullable: false),
                    SlackFlg = table.Column<bool>(type: "bit", nullable: false),
                    LineAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinePassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SlackPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryMasterId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules");
        }
    }
}
