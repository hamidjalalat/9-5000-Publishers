using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class fieldofstudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FieldOfStudyId",
                table: "Books",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FieldOfStudyId",
                table: "Books",
                column: "FieldOfStudyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_FieldOfStudys_FieldOfStudyId",
                table: "Books",
                column: "FieldOfStudyId",
                principalTable: "FieldOfStudys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_FieldOfStudys_FieldOfStudyId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FieldOfStudyId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FieldOfStudyId",
                table: "Books");
        }
    }
}
