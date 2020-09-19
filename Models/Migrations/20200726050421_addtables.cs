using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class addtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasConceptualPoints",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasGoldenTips",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLetterCharts",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasLetterTables",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasMovies",
                table: "Lessons",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ConceptualPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    PageBaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConceptualPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoldenTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    PageBaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoldenTips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterCharts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    PageBaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterCharts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LetterTables",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    PageBaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LetterTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    PageBaseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConceptualPoints");

            migrationBuilder.DropTable(
                name: "GoldenTips");

            migrationBuilder.DropTable(
                name: "LetterCharts");

            migrationBuilder.DropTable(
                name: "LetterTables");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropColumn(
                name: "HasConceptualPoints",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "HasGoldenTips",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "HasLetterCharts",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "HasLetterTables",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "HasMovies",
                table: "Lessons");
        }
    }
}
