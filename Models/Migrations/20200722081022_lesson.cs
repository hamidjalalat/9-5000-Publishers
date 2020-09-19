using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class lesson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_FieldOfStudys_FieldOfStudyId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Sections_SectionId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductType_Lessons_LessonId",
                table: "ProductType");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_FieldOfStudys_FieldOFStudyId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_FieldOFStudyId",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_ProductType_LessonId",
                table: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SectionId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookTypeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FieldOfStudyId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FieldOFStudyId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "ProductType");

            migrationBuilder.AddColumn<int>(
                name: "FieldOfStudyId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonTypeId",
                table: "Lessons",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LessonTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTypes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonTypes");

            migrationBuilder.DropColumn(
                name: "FieldOfStudyId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonTypeId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "FieldOFStudyId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "ProductType",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FieldOFStudyId",
                table: "Sections",
                column: "FieldOFStudyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_LessonId",
                table: "ProductType",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SectionId",
                table: "Lessons",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookTypeId",
                table: "Books",
                column: "BookTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_FieldOfStudyId",
                table: "Books",
                column: "FieldOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookTypes_BookTypeId",
                table: "Books",
                column: "BookTypeId",
                principalTable: "BookTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FieldOfStudys_FieldOfStudyId",
                table: "Books",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Sections_SectionId",
                table: "Lessons",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductType_Lessons_LessonId",
                table: "ProductType",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_FieldOfStudys_FieldOFStudyId",
                table: "Sections",
                column: "FieldOFStudyId",
                principalTable: "FieldOfStudys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
