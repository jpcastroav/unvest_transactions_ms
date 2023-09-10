using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace unvest_transactions_ms.Migrations
{
    /// <inheritdoc />
    public partial class TransactionsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "balance",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valor = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_balance", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "operacion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operacion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "transaccion",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo = table.Column<int>(type: "int", nullable: false),
                    valor_accion = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    cantidad = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_empresa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaccion", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balance");

            migrationBuilder.DropTable(
                name: "operacion");

            migrationBuilder.DropTable(
                name: "transaccion");
        }
    }
}
