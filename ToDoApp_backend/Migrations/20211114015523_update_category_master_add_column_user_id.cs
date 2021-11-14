using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp_backend.Migrations
{
    public partial class update_category_master_add_column_user_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "CategoryMasters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CategoryMasters");
        }
    }
}
