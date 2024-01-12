using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class ExamEdited : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "question_in_exams",
                newName: "question_in_exams",
                newSchema: "learning");

            migrationBuilder.RenameTable(
                name: "exams",
                newName: "exams",
                newSchema: "learning");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_score",
                schema: "learning",
                table: "quizzes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<decimal>(
                name: "passing_score",
                schema: "learning",
                table: "quizzes",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "total_ball",
                schema: "learning",
                table: "questions",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "possible_answer",
                schema: "learning",
                table: "questions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "accumulated_ball",
                schema: "learning",
                table: "question_in_exams",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "checked",
                schema: "learning",
                table: "question_in_exams",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "written_answer",
                schema: "learning",
                table: "question_in_exams",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "possible_answer",
                schema: "learning",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "accumulated_ball",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.DropColumn(
                name: "checked",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.DropColumn(
                name: "written_answer",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.RenameTable(
                name: "question_in_exams",
                schema: "learning",
                newName: "question_in_exams");

            migrationBuilder.RenameTable(
                name: "exams",
                schema: "learning",
                newName: "exams");

            migrationBuilder.AlterColumn<int>(
                name: "total_score",
                schema: "learning",
                table: "quizzes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<int>(
                name: "passing_score",
                schema: "learning",
                table: "quizzes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "total_ball",
                schema: "learning",
                table: "questions",
                type: "integer",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);
        }
    }
}
