using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoService.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class nombres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparacion_Clientes_ClienteId",
                table: "Reparacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Reparacion_Tecnicos_TecnicoId",
                table: "Reparacion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores");

            migrationBuilder.RenameTable(
                name: "Tecnicos",
                newName: "Tecnico");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameTable(
                name: "Administradores",
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
                name: "FK_Reparacion_Cliente_ClienteId",
                table: "Reparacion",
                column: "ClienteId",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reparacion_Tecnico_TecnicoId",
                table: "Reparacion",
                column: "TecnicoId",
                principalTable: "Tecnico",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reparacion_Cliente_ClienteId",
                table: "Reparacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Reparacion_Tecnico_TecnicoId",
                table: "Reparacion");

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
                newName: "Tecnicos");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameTable(
                name: "Administrador",
                newName: "Administradores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tecnicos",
                table: "Tecnicos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reparacion_Clientes_ClienteId",
                table: "Reparacion",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reparacion_Tecnicos_TecnicoId",
                table: "Reparacion",
                column: "TecnicoId",
                principalTable: "Tecnicos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
