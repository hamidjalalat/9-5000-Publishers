using Microsoft.EntityFrameworkCore.Migrations;

namespace Models.Migrations
{
    public partial class addshamsi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EndCreateDateShamsi",
                table: "Books",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndCreateDateShamsi",
                table: "Books");
        }
    }
}
