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
                name: "FK_Sastojci_Stolovi_StoID",
                table: "Sastojci");

            migrationBuilder.DropIndex(
                name: "IX_Sastojci_StoID",
                table: "Sastojci");

            migrationBuilder.DropColumn(
                name: "StoID",
                table: "Sastojci");

            migrationBuilder.CreateTable(
                name: "Spoj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SastojakID = table.Column<int>(type: "int", nullable: true),
                    StoID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spoj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Spoj_Sastojci_SastojakID",
                        column: x => x.SastojakID,
                        principalTable: "Sastojci",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Spoj_Stolovi_StoID",
                        column: x => x.StoID,
                        principalTable: "Stolovi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_SastojakID",
                table: "Spoj",
                column: "SastojakID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_StoID",
                table: "Spoj",
                column: "StoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spoj");

            migrationBuilder.AddColumn<int>(
                name: "StoID",
                table: "Sastojci",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sastojci_StoID",
                table: "Sastojci",
                column: "StoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sastojci_Stolovi_StoID",
                table: "Sastojci",
                column: "StoID",
                principalTable: "Stolovi",
                principalColumn: "ID");
        }
    }
}
