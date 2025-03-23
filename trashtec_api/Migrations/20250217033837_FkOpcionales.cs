using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace trashtec_api.Migrations
{
    /// <inheritdoc />
    public partial class FkOpcionales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Usuarios_UsuarioId",
                table: "Dispositivos");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivos_UsuarioId",
                table: "Dispositivos");

            migrationBuilder.RenameColumn(
                name: "Contrasena",
                table: "Usuarios",
                newName: "contrasena");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Usuarios",
                newName: "idUsuario");

            migrationBuilder.RenameColumn(
                name: "IdDispositivo",
                table: "Dispositivos",
                newName: "idDispositivo");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Dispositivos",
                newName: "Llenado");

            migrationBuilder.AlterColumn<long>(
                name: "idUsuario",
                table: "Usuarios",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Dispositivoid",
                table: "Usuarios",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "idDispositivo",
                table: "Dispositivos",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "IdUsuario",
                table: "Dispositivos",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Dispositivoid",
                table: "Usuarios",
                column: "Dispositivoid");

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_IdUsuario",
                table: "Dispositivos",
                column: "IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Usuarios_IdUsuario",
                table: "Dispositivos",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Dispositivos_Dispositivoid",
                table: "Usuarios",
                column: "Dispositivoid",
                principalTable: "Dispositivos",
                principalColumn: "idDispositivo",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispositivos_Usuarios_IdUsuario",
                table: "Dispositivos");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Dispositivos_Dispositivoid",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Dispositivoid",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Dispositivos_IdUsuario",
                table: "Dispositivos");

            migrationBuilder.DropColumn(
                name: "Dispositivoid",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Dispositivos");

            migrationBuilder.RenameColumn(
                name: "contrasena",
                table: "Usuarios",
                newName: "Contrasena");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "Usuarios",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "idDispositivo",
                table: "Dispositivos",
                newName: "IdDispositivo");

            migrationBuilder.RenameColumn(
                name: "Llenado",
                table: "Dispositivos",
                newName: "UsuarioId");

            migrationBuilder.AlterColumn<int>(
                name: "IdUsuario",
                table: "Usuarios",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "IdDispositivo",
                table: "Dispositivos",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Dispositivos_UsuarioId",
                table: "Dispositivos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispositivos_Usuarios_UsuarioId",
                table: "Dispositivos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
