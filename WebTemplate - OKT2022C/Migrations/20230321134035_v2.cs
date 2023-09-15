using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Silosi_Radnici_RadnikID",
                table: "Silosi");

            migrationBuilder.DropIndex(
                name: "IX_Silosi_RadnikID",
                table: "Silosi");

            migrationBuilder.DropColumn(
                name: "RadnikID",
                table: "Silosi");

            migrationBuilder.AddColumn<int>(
                name: "SilosID",
                table: "Radnici",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Radnici_SilosID",
                table: "Radnici",
                column: "SilosID");

            migrationBuilder.AddForeignKey(
                name: "FK_Radnici_Silosi_SilosID",
                table: "Radnici",
                column: "SilosID",
                principalTable: "Silosi",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Radnici_Silosi_SilosID",
                table: "Radnici");

            migrationBuilder.DropIndex(
                name: "IX_Radnici_SilosID",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "SilosID",
                table: "Radnici");

            migrationBuilder.AddColumn<int>(
                name: "RadnikID",
                table: "Silosi",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Silosi_RadnikID",
                table: "Silosi",
                column: "RadnikID");

            migrationBuilder.AddForeignKey(
                name: "FK_Silosi_Radnici_RadnikID",
                table: "Silosi",
                column: "RadnikID",
                principalTable: "Radnici",
                principalColumn: "ID");
        }
    }
}
