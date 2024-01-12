using System.Collections.Generic;
using Entitys.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DatabaseBroker.Migrations
{
    /// <inheritdoc />
    public partial class ColumnNameEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "aftor",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "video_course",
                schema: "learning");

            migrationBuilder.RenameColumn(
                name: "aftor_id",
                schema: "learning",
                table: "short_video",
                newName: "author_id");

            migrationBuilder.RenameColumn(
                name: "discription",
                schema: "learning",
                table: "article",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "aftor_id",
                schema: "learning",
                table: "article",
                newName: "author_id");

            migrationBuilder.CreateTable(
                name: "author",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_author", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "seminar_video",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    video_linc = table.Column<string>(type: "text", nullable: false),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    hashtag_id = table.Column<List<int>>(type: "integer[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_seminar_video", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author",
                schema: "learning");

            migrationBuilder.DropTable(
                name: "seminar_video",
                schema: "learning");

            migrationBuilder.RenameColumn(
                name: "author_id",
                schema: "learning",
                table: "short_video",
                newName: "aftor_id");

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "learning",
                table: "article",
                newName: "discription");

            migrationBuilder.RenameColumn(
                name: "author_id",
                schema: "learning",
                table: "article",
                newName: "aftor_id");

            migrationBuilder.CreateTable(
                name: "aftor",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<MultiLanguageField>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aftor", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "video_course",
                schema: "learning",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aftor_id = table.Column<int>(type: "integer", nullable: true),
                    category_id = table.Column<int>(type: "integer", nullable: true),
                    hashtag_id = table.Column<List<int>>(type: "integer[]", nullable: false),
                    title = table.Column<MultiLanguageField>(type: "jsonb", nullable: false),
                    video_linc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_video_course", x => x.id);
                });
        }
    }
}
