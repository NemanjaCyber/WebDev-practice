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
                name: "FK_Magacini_Iverice_IvericaID",
                table: "Magacini");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Magacini",
                table: "Magacini");

            migrationBuilder.RenameTable(
                name: "Magacini",
                newName: "Otpadci");

            migrationBuilder.RenameIndex(
                name: "IX_Magacini_IvericaID",
                table: "Otpadci",
                newName: "IX_Otpadci_IvericaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Otpadci",
                table: "Otpadci",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "NarucenePloce",
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
                    table.PrimaryKey("PK_NarucenePloce", x => x.ID);
                    table.ForeignKey(
                        name: "FK_NarucenePloce_Iverice_IvericaID",
                        column: x => x.IvericaID,
                        principalTable: "Iverice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NarucenePloce_IvericaID",
                table: "NarucenePloce",
                column: "IvericaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Otpadci_Iverice_IvericaID",
                table: "Otpadci",
                column: "IvericaID",
                principalTable: "Iverice",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Otpadci_Iverice_IvericaID",
                table: "Otpadci");

            migrationBuilder.DropTable(
                name: "NarucenePloce");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Otpadci",
                table: "Otpadci");

            migrationBuilder.RenameTable(
                name: "Otpadci",
                newName: "Magacini");

            migrationBuilder.RenameIndex(
                name: "IX_Otpadci_IvericaID",
                table: "Magacini",
                newName: "IX_Magacini_IvericaID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Magacini",
                table: "Magacini",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Magacini_Iverice_IvericaID",
                table: "Magacini",
                column: "IvericaID",
                principalTable: "Iverice",
                principalColumn: "ID");
        }
    }
}
