using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoService.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class cambiosfotosucursal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Sucursales");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Empresas");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Sucursales",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
