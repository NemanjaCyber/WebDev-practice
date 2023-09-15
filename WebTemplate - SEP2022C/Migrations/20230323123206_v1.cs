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
                name: "Sajtovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sajtovi", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Biljke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vidjanja = table.Column<int>(type: "int", nullable: false),
                    Podrucje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cvet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    List = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stablo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SajtID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biljke", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Biljke_Sajtovi_SajtID",
                        column: x => x.SajtID,
                        principalTable: "Sajtovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "NepoznateBiljke",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Podrucje = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cvet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    List = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stablo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SajtID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NepoznateBiljke", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NepoznateBiljke_Sajtovi_SajtID",
                        column: x => x.SajtID,
                        principalTable: "Sajtovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Biljke_SajtID",
                table: "Biljke",
                column: "SajtID");

            migrationBuilder.CreateIndex(
                name: "IX_NepoznateBiljke_SajtID",
                table: "NepoznateBiljke",
                column: "SajtID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Biljke");

            migrationBuilder.DropTable(
                name: "NepoznateBiljke");

            migrationBuilder.DropTable(
                name: "Sajtovi");
        }
    }
}
