using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class quiz_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_quizzes_course_id",
                schema: "learning",
                table: "quizzes");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_course_id",
                schema: "learning",
                table: "quizzes",
                column: "course_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_quizzes_course_id",
                schema: "learning",
                table: "quizzes");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_course_id",
                schema: "learning",
                table: "quizzes",
                column: "course_id");
        }
    }
}
