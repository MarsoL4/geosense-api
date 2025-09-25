using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSense.API.Migrations
{
    /// <inheritdoc />
    public partial class MotoProblemaIdentificadoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PROBLEMA_IDENTIFICADO",
                table: "MOTO",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PROBLEMA_IDENTIFICADO",
                table: "MOTO",
                type: "NVARCHAR2(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(255)",
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
