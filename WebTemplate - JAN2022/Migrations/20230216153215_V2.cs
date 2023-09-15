using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmovi_ProdukcijskeKuce_ProdukcijskaKucaID",
                table: "Filmovi");

            migrationBuilder.RenameColumn(
                name: "ProdukcijskaKucaID",
                table: "Filmovi",
                newName: "ProdukcijaID");

            migrationBuilder.RenameIndex(
                name: "IX_Filmovi_ProdukcijskaKucaID",
                table: "Filmovi",
                newName: "IX_Filmovi_ProdukcijaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmovi_ProdukcijskeKuce_ProdukcijaID",
                table: "Filmovi",
                column: "ProdukcijaID",
                principalTable: "ProdukcijskeKuce",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Filmovi_ProdukcijskeKuce_ProdukcijaID",
                table: "Filmovi");

            migrationBuilder.RenameColumn(
                name: "ProdukcijaID",
                table: "Filmovi",
                newName: "ProdukcijskaKucaID");

            migrationBuilder.RenameIndex(
                name: "IX_Filmovi_ProdukcijaID",
                table: "Filmovi",
                newName: "IX_Filmovi_ProdukcijskaKucaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Filmovi_ProdukcijskeKuce_ProdukcijskaKucaID",
                table: "Filmovi",
                column: "ProdukcijskaKucaID",
                principalTable: "ProdukcijskeKuce",
                principalColumn: "ID");
        }
    }
}
