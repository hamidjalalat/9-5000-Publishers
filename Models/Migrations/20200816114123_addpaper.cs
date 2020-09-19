using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class addpaper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Paper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(nullable: false),
                    Paper1 = table.Column<string>(nullable: true),
                    Paper2 = table.Column<string>(nullable: true),
                    Paper3 = table.Column<string>(nullable: true),
                    Paper4 = table.Column<string>(nullable: true),
                    Paper5 = table.Column<string>(nullable: true),
                    Paper6 = table.Column<string>(nullable: true),
                    Paper7 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paper", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Paper");
        }
    }
}
