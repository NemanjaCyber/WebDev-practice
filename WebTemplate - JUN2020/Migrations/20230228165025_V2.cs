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
            migrationBuilder.CreateTable(
                name: "Mecevi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lokacija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S1 = table.Column<int>(type: "int", nullable: false),
                    S2 = table.Column<int>(type: "int", nullable: false),
                    PS11 = table.Column<int>(type: "int", nullable: false),
                    PS12 = table.Column<int>(type: "int", nullable: false),
                    PS21 = table.Column<int>(type: "int", nullable: false),
                    PS22 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mecevi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Godine = table.Column<int>(type: "int", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false),
                    MecNaKojemUcestvujeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igraci_Mecevi_MecNaKojemUcestvujeID",
                        column: x => x.MecNaKojemUcestvujeID,
                        principalTable: "Mecevi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_MecNaKojemUcestvujeID",
                table: "Igraci",
                column: "MecNaKojemUcestvujeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Mecevi");
        }
    }
}
