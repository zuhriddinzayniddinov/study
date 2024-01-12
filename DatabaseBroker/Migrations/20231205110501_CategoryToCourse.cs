using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class CategoryToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_category_course_id",
                schema: "learning",
                table: "quizzes");

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_course_course_id",
                schema: "learning",
                table: "quizzes",
                column: "course_id",
                principalSchema: "learning",
                principalTable: "course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_quizzes_course_course_id",
                schema: "learning",
                table: "quizzes");

            migrationBuilder.AddForeignKey(
                name: "FK_quizzes_category_course_id",
                schema: "learning",
                table: "quizzes",
                column: "course_id",
                principalSchema: "learning",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
