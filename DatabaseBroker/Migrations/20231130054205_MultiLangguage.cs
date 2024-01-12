using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class MultiLangguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_video_course_category",
                schema: "learning",
                table: "video_course_category");

            migrationBuilder.RenameTable(
                name: "video_course_category",
                schema: "learning",
                newName: "category",
                newSchema: "learning");

            migrationBuilder.AddPrimaryKey(
                name: "PK_category",
                schema: "learning",
                table: "category",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_category",
                schema: "learning",
                table: "category");

            migrationBuilder.RenameTable(
                name: "category",
                schema: "learning",
                newName: "video_course_category",
                newSchema: "learning");

            migrationBuilder.AddPrimaryKey(
                name: "PK_video_course_category",
                schema: "learning",
                table: "video_course_category",
                column: "id");
        }
    }
}
