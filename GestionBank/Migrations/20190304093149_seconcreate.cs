using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionBank.Migrations
{
    public partial class seconcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Solde",
                table: "Comptes",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Solde",
                table: "Comptes");
        }
    }
}
