using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeMat.Infra.Migrations {
	public partial class Initial : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.EnsureSchema(
				name: "spc");

			migrationBuilder.CreateTable(
				name: "suppliers",
				schema: "spc",
				columns: table => new {
					id = table.Column<int>(type: "integer", nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
					name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
					company_reason = table.Column<string>(type: "VARCHAR(255)", nullable: true),
					telephone = table.Column<string>(type: "text", nullable: true),
					email = table.Column<string>(type: "text", nullable: true),
					street = table.Column<string>(type: "text", nullable: true),
					number = table.Column<string>(type: "text", nullable: true),
					neighborhood = table.Column<string>(type: "text", nullable: true),
					city = table.Column<string>(type: "text", nullable: true),
					state = table.Column<string>(type: "text", nullable: true),
					country = table.Column<string>(type: "text", nullable: true),
					zip_code = table.Column<string>(type: "text", nullable: true),
					document = table.Column<string>(type: "text", nullable: true),
					document_type = table.Column<int>(type: "integer", nullable: true, defaultValue: 1)
				},
				constraints: table => {
					table.PrimaryKey("PK_suppliers", x => x.id);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.DropTable(
				name: "suppliers",
				schema: "spc");
		}
	}
}
