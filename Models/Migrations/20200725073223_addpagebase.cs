using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class addpagebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PageBases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PageNumber = table.Column<int>(nullable: false),
                    ServerPath = table.Column<string>(nullable: false),
                    SessionNumber = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    Enable = table.Column<bool>(nullable: false),
                    BookId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageBases_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageBases_BookId",
                table: "PageBases",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageBases");
        }
    }
}
