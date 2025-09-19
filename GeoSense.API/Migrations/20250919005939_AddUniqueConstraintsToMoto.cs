using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoSense.API.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintsToMoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MOTO_VAGA_ID",
                table: "MOTO");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_CHASSI",
                table: "MOTO",
                column: "CHASSI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_PLACA",
                table: "MOTO",
                column: "PLACA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_VAGA_ID",
                table: "MOTO",
                column: "VAGA_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MOTO_CHASSI",
                table: "MOTO");

            migrationBuilder.DropIndex(
                name: "IX_MOTO_PLACA",
                table: "MOTO");

            migrationBuilder.DropIndex(
                name: "IX_MOTO_VAGA_ID",
                table: "MOTO");

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_VAGA_ID",
                table: "MOTO",
                column: "VAGA_ID");
        }
    }
}
