using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LeMat.Infra.Migrations
{
    public partial class AddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                schema: "lemat",
                table: "suppliers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                schema: "lemat",
                table: "suppliers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.CreateTable(
                name: "products",
                schema: "lemat",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    suggested_sell_price = table.Column<decimal>(type: "numeric", nullable: false),
                    maximum_discount_percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    image_path = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    supplier_id = table.Column<int>(type: "integer", nullable: true),
                    stock_available = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                    table.ForeignKey(
                        name: "FK_products_suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalSchema: "lemat",
                        principalTable: "suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_supplier_id",
                schema: "lemat",
                table: "products",
                column: "supplier_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products",
                schema: "lemat");

            migrationBuilder.DropColumn(
                name: "created_at",
                schema: "lemat",
                table: "suppliers");

            migrationBuilder.DropColumn(
                name: "modified_at",
                schema: "lemat",
                table: "suppliers");
        }
    }
}
