using System;
using System.Collections.Generic;
using Entity.Models.Learning;
using Entitys.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class Quiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                schema: "learning",
                table: "video_of_course",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "learning",
                table: "short_video",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                schema: "learning",
                table: "short_video",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "learning",
                table: "seminar_video",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                schema: "learning",
                table: "seminar_video",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "quizzes",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    course_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    description = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    total_score = table.Column<int>(type: "integer", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quizzes", x => x.id);
                    table.ForeignKey(
                        name: "FK_quizzes_category_course_id",
                        column: x => x.course_id,
                        principalSchema: "learning",
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quiz_id = table.Column<long>(type: "bigint", nullable: false),
                    order_number = table.Column<int>(type: "integer", nullable: false),
                    question_type = table.Column<int>(type: "integer", nullable: false),
                    question_content = table.Column<MultiLanguageField>(type: "jsonb", nullable: true),
                    image_link = table.Column<string>(type: "text", nullable: true),
                    doc_link = table.Column<string>(type: "text", nullable: true),
                    options = table.Column<List<SimpleQuestionOption>>(type: "jsonb", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalSchema: "learning",
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_video_of_course_course_id",
                schema: "learning",
                table: "video_of_course",
                column: "course_id");

            migrationBuilder.CreateIndex(
                name: "IX_short_video_author_id",
                schema: "learning",
                table: "short_video",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_short_video_category_id",
                schema: "learning",
                table: "short_video",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_seminar_video_author_id",
                schema: "learning",
                table: "seminar_video",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_seminar_video_category_id",
                schema: "learning",
                table: "seminar_video",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_author_id",
                schema: "learning",
                table: "course",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_course_category_id",
                schema: "learning",
                table: "course",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_author_id",
                schema: "learning",
                table: "article",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_category_id",
                schema: "learning",
                table: "article",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_quiz_id",
                schema: "learning",
                table: "questions",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "IX_quizzes_course_id",
                schema: "learning",
                table: "quizzes",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "FK_article_author_author_id",
                schema: "learning",
                table: "article",
                column: "author_id",
                principalSchema: "learning",
                principalTable: "author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_article_category_category_id",
                schema: "learning",
                table: "article",
                column: "category_id",
                principalSchema: "learning",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_author_author_id",
                schema: "learning",
                table: "course",
                column: "author_id",
                principalSchema: "learning",
                principalTable: "author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_course_category_category_id",
                schema: "learning",
                table: "course",
                column: "category_id",
                principalSchema: "learning",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seminar_video_author_author_id",
                schema: "learning",
                table: "seminar_video",
                column: "author_id",
                principalSchema: "learning",
                principalTable: "author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_seminar_video_category_category_id",
                schema: "learning",
                table: "seminar_video",
                column: "category_id",
                principalSchema: "learning",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_short_video_author_author_id",
                schema: "learning",
                table: "short_video",
                column: "author_id",
                principalSchema: "learning",
                principalTable: "author",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_short_video_category_category_id",
                schema: "learning",
                table: "short_video",
                column: "category_id",
                principalSchema: "learning",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_video_of_course_course_course_id",
                schema: "learning",
                table: "video_of_course",
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
                name: "FK_article_author_author_id",
                schema: "learning",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_article_category_category_id",
                schema: "learning",
                table: "article");

            migrationBuilder.DropForeignKey(
                name: "FK_course_author_author_id",
                schema: "learning",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_course_category_category_id",
                schema: "learning",
                table: "course");

            migrationBuilder.DropForeignKey(
                name: "FK_seminar_video_author_author_id",
                schema: "learning",
                table: "seminar_video");

            migrationBuilder.DropForeignKey(
                name: "FK_seminar_video_category_category_id",
                schema: "learning",
                table: "seminar_video");

            migrationBuilder.DropForeignKey(
                name: "FK_short_video_author_author_id",
                schema: "learning",
                table: "short_video");

            migrationBuilder.DropForeignKey(
                name: "FK_short_video_category_category_id",
                schema: "learning",
                table: "short_video");

            migrationBuilder.DropForeignKey(
                name: "FK_video_of_course_course_course_id",
                schema: "learning",
                table: "video_of_course");

            migrationBuilder.DropTable(
                name: "questions",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "quizzes",
                schema: "learning");

            migrationBuilder.DropIndex(
                name: "IX_video_of_course_course_id",
                schema: "learning",
                table: "video_of_course");

            migrationBuilder.DropIndex(
                name: "IX_short_video_author_id",
                schema: "learning",
                table: "short_video");

            migrationBuilder.DropIndex(
                name: "IX_short_video_category_id",
                schema: "learning",
                table: "short_video");

            migrationBuilder.DropIndex(
                name: "IX_seminar_video_author_id",
                schema: "learning",
                table: "seminar_video");

            migrationBuilder.DropIndex(
                name: "IX_seminar_video_category_id",
                schema: "learning",
                table: "seminar_video");

            migrationBuilder.DropIndex(
                name: "IX_course_author_id",
                schema: "learning",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_course_category_id",
                schema: "learning",
                table: "course");

            migrationBuilder.DropIndex(
                name: "IX_article_author_id",
                schema: "learning",
                table: "article");

            migrationBuilder.DropIndex(
                name: "IX_article_category_id",
                schema: "learning",
                table: "article");

            migrationBuilder.AlterColumn<int>(
                name: "course_id",
                schema: "learning",
                table: "video_of_course",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "learning",
                table: "short_video",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                schema: "learning",
                table: "short_video",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "category_id",
                schema: "learning",
                table: "seminar_video",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "author_id",
                schema: "learning",
                table: "seminar_video",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
