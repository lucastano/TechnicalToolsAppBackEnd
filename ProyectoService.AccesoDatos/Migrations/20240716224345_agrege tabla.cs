using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoService.AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class agregetabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Reparacion_ReparacionId",
                table: "Mensajes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mensajes",
                table: "Mensajes");

            migrationBuilder.RenameTable(
                name: "Mensajes",
                newName: "Mensaje");

            migrationBuilder.RenameIndex(
                name: "IX_Mensajes_ReparacionId",
                table: "Mensaje",
                newName: "IX_Mensaje_ReparacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Mensajes_EmisorId",
                table: "Mensaje",
                newName: "IX_Mensaje_EmisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Mensajes_DestinatarioId",
                table: "Mensaje",
                newName: "IX_Mensaje_DestinatarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mensaje",
                table: "Mensaje",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Mensaje_Reparacion_ReparacionId",
                table: "Mensaje",
                column: "ReparacionId",
                principalTable: "Reparacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensaje_Reparacion_ReparacionId",
                table: "Mensaje");

            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Mensaje",
                table: "Mensaje");

            migrationBuilder.RenameTable(
                name: "Mensaje",
                newName: "Mensajes");

            migrationBuilder.RenameIndex(
                name: "IX_Mensaje_ReparacionId",
                table: "Mensajes",
                newName: "IX_Mensajes_ReparacionId");

            migrationBuilder.RenameIndex(
                name: "IX_Mensaje_EmisorId",
                table: "Mensajes",
                newName: "IX_Mensajes_EmisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Mensaje_DestinatarioId",
                table: "Mensajes",
                newName: "IX_Mensajes_DestinatarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Mensajes",
                table: "Mensajes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Reparacion_ReparacionId",
                table: "Mensajes",
                column: "ReparacionId",
                principalTable: "Reparacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
