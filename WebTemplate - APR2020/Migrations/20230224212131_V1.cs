using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Klubovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Klubovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Police",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oznaka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxDVD = table.Column<int>(type: "int", nullable: false),
                    TrenutnoDVD = table.Column<int>(type: "int", nullable: false),
                    MojKlubID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Police", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Police_Klubovi_MojKlubID",
                        column: x => x.MojKlubID,
                        principalTable: "Klubovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Police_MojKlubID",
                table: "Police",
                column: "MojKlubID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Police");

            migrationBuilder.DropTable(
                name: "Klubovi");
        }
    }
}
