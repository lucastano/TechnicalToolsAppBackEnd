using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoService.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class secambiannombresdetablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reparaciones_Cliente_ClienteId",
                table: "reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_reparaciones_Tecnico_TecnicoId",
                table: "reparaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador");

            migrationBuilder.RenameTable(
                name: "Tecnico",
                newName: "tecnicos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "clientes");

            migrationBuilder.RenameTable(
                name: "Administrador",
                newName: "administradores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tecnicos",
                table: "tecnicos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clientes",
                table: "clientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_administradores",
                table: "administradores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reparaciones_clientes_ClienteId",
                table: "reparaciones",
                column: "ClienteId",
                principalTable: "clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reparaciones_tecnicos_TecnicoId",
                table: "reparaciones",
                column: "TecnicoId",
                principalTable: "tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reparaciones_clientes_ClienteId",
                table: "reparaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_reparaciones_tecnicos_TecnicoId",
                table: "reparaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tecnicos",
                table: "tecnicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clientes",
                table: "clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_administradores",
                table: "administradores");

            migrationBuilder.RenameTable(
                name: "tecnicos",
                newName: "Tecnico");

            migrationBuilder.RenameTable(
                name: "clientes",
                newName: "Cliente");

            migrationBuilder.RenameTable(
                name: "administradores",
                newName: "Administrador");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnico",
                table: "Tecnico",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_reparaciones_Cliente_ClienteId",
                table: "reparaciones",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reparaciones_Tecnico_TecnicoId",
                table: "reparaciones",
                column: "TecnicoId",
                principalTable: "Tecnico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
