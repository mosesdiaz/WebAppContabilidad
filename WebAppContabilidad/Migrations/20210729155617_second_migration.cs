using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppContabilidad.Migrations
{
    public partial class second_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SistemaAuxiliar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemaAuxiliar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeCuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origen = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeCuenta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoDeMoneda",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Codigo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TasaDeCambio = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoDeMoneda", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CuentaContable",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoDeCuenta = table.Column<int>(type: "int", nullable: true),
                    PermiteTransacciones = table.Column<bool>(type: "bit", nullable: false),
                    CuentaMayor = table.Column<int>(type: "int", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaContable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CuentaContable_CuentaContable_CuentaMayor",
                        column: x => x.CuentaMayor,
                        principalTable: "CuentaContable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CuentaContable_TipoDeCuenta_TipoDeCuenta",
                        column: x => x.TipoDeCuenta,
                        principalTable: "TipoDeCuenta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CuentaContable_CuentaMayor",
                table: "CuentaContable",
                column: "CuentaMayor");

            migrationBuilder.CreateIndex(
                name: "IX_CuentaContable_TipoDeCuenta",
                table: "CuentaContable",
                column: "TipoDeCuenta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CuentaContable");

            migrationBuilder.DropTable(
                name: "SistemaAuxiliar");

            migrationBuilder.DropTable(
                name: "TipoDeMoneda");

            migrationBuilder.DropTable(
                name: "TipoDeCuenta");
        }
    }
}
