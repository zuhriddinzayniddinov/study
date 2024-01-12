using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "video_of_course",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "user_sign_methods",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "user_certificates",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "tokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "structures",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "structure_permissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "asset",
                table: "static_files",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "short_video",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "seminar_video",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "quizzes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "question_in_exams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "auth",
                table: "permissions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "hashtag",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "exams",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "course",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "category",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "author_to_category",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "author",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_delete",
                schema: "learning",
                table: "article",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_question_in_exams_question_id",
                schema: "learning",
                table: "question_in_exams",
                column: "question_id");

            migrationBuilder.AddForeignKey(
                name: "FK_question_in_exams_questions_question_id",
                schema: "learning",
                table: "question_in_exams",
                column: "question_id",
                principalSchema: "learning",
                principalTable: "questions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_question_in_exams_questions_question_id",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.DropIndex(
                name: "IX_question_in_exams_question_id",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "video_of_course");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "user_sign_methods");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "user_certificates");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "tokens");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "structures");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "structure_permissions");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "asset",
                table: "static_files");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "short_video");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "seminar_video");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "question_in_exams");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "auth",
                table: "permissions");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "hashtag");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "exams");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "course");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "category");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "author_to_category");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "author");

            migrationBuilder.DropColumn(
                name: "is_delete",
                schema: "learning",
                table: "article");
        }
    }
}
