using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prodavnice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zarada = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnice", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Iverice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duzina = table.Column<int>(type: "int", nullable: false),
                    Sirina = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iverice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Iverice_Prodavnice_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Magacini",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sara = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duzina = table.Column<int>(type: "int", nullable: false),
                    Sirina = table.Column<int>(type: "int", nullable: false),
                    IvericaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Magacini", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Magacini_Iverice_IvericaID",
                        column: x => x.IvericaID,
                        principalTable: "Iverice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Iverice_ProdavnicaID",
                table: "Iverice",
                column: "ProdavnicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Magacini_IvericaID",
                table: "Magacini",
                column: "IvericaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Magacini");

            migrationBuilder.DropTable(
                name: "Iverice");

            migrationBuilder.DropTable(
                name: "Prodavnice");
        }
    }
}
