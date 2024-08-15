using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SensorApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_codigo = table.Column<int>(type: "integer", nullable: false),
                    descripcion_larga = table.Column<string>(type: "text", nullable: false),
                    descripcion_med = table.Column<string>(type: "text", nullable: false),
                    descripcion_corta = table.Column<string>(type: "text", nullable: false),
                    abreviacion = table.Column<string>(type: "text", nullable: false),
                    observacion = table.Column<string>(type: "text", nullable: false),
                    fecha_creacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_modificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    estado = table.Column<char>(type: "character(1)", nullable: false),
                    unidad = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SensorData",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    codigo_parametro = table.Column<int>(type: "integer", nullable: false),
                    parametro_sensores_id = table.Column<int>(type: "integer", nullable: false),
                    nombre_parametro = table.Column<string>(type: "text", nullable: false),
                    fecha_dato = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_numero = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SensorData", x => x.id);
                    table.ForeignKey(
                        name: "FK_SensorData_Parameters_parametro_sensores_id",
                        column: x => x.parametro_sensores_id,
                        principalTable: "Parameters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SensorData_parametro_sensores_id",
                table: "SensorData",
                column: "parametro_sensores_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SensorData");

            migrationBuilder.DropTable(
                name: "Parameters");
        }
    }
}
