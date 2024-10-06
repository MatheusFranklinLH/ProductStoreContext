using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeMat.Infra.Migrations
{
    public partial class FixImageFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_path",
                schema: "lemat",
                table: "images",
                newName: "image_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image_name",
                schema: "lemat",
                table: "images",
                newName: "image_path");
        }
    }
}
