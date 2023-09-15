using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTemplate.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spoj_Sastojci_SastojakID",
                table: "Spoj");

            migrationBuilder.DropForeignKey(
                name: "FK_Spoj_Stolovi_StoID",
                table: "Spoj");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spoj",
                table: "Spoj");

            migrationBuilder.RenameTable(
                name: "Spoj",
                newName: "Spojevi");

            migrationBuilder.RenameIndex(
                name: "IX_Spoj_StoID",
                table: "Spojevi",
                newName: "IX_Spojevi_StoID");

            migrationBuilder.RenameIndex(
                name: "IX_Spoj_SastojakID",
                table: "Spojevi",
                newName: "IX_Spojevi_SastojakID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spojevi",
                table: "Spojevi",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spojevi_Sastojci_SastojakID",
                table: "Spojevi",
                column: "SastojakID",
                principalTable: "Sastojci",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spojevi_Stolovi_StoID",
                table: "Spojevi",
                column: "StoID",
                principalTable: "Stolovi",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spojevi_Sastojci_SastojakID",
                table: "Spojevi");

            migrationBuilder.DropForeignKey(
                name: "FK_Spojevi_Stolovi_StoID",
                table: "Spojevi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spojevi",
                table: "Spojevi");

            migrationBuilder.RenameTable(
                name: "Spojevi",
                newName: "Spoj");

            migrationBuilder.RenameIndex(
                name: "IX_Spojevi_StoID",
                table: "Spoj",
                newName: "IX_Spoj_StoID");

            migrationBuilder.RenameIndex(
                name: "IX_Spojevi_SastojakID",
                table: "Spoj",
                newName: "IX_Spoj_SastojakID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spoj",
                table: "Spoj",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spoj_Sastojci_SastojakID",
                table: "Spoj",
                column: "SastojakID",
                principalTable: "Sastojci",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Spoj_Stolovi_StoID",
                table: "Spoj",
                column: "StoID",
                principalTable: "Stolovi",
                principalColumn: "ID");
        }
    }
}
