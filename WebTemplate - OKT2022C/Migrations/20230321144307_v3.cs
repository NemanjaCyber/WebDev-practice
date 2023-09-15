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
                name: "FK_Radnici_Silosi_SilosID",
                table: "Radnici");

            migrationBuilder.DropIndex(
                name: "IX_Radnici_SilosID",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "IspraznioKolicinu",
                table: "Radnici");

            migrationBuilder.DropColumn(
                name: "SilosID",
                table: "Radnici");

            migrationBuilder.CreateTable(
                name: "Praznjenja",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IspraznjenaKolicina = table.Column<int>(type: "int", nullable: false),
                    RadnikID = table.Column<int>(type: "int", nullable: true),
                    SilosID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Praznjenja", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Praznjenja_Radnici_RadnikID",
                        column: x => x.RadnikID,
                        principalTable: "Radnici",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Praznjenja_Silosi_SilosID",
                        column: x => x.SilosID,
                        principalTable: "Silosi",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Praznjenja_RadnikID",
                table: "Praznjenja",
                column: "RadnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Praznjenja_SilosID",
                table: "Praznjenja",
                column: "SilosID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Praznjenja");

            migrationBuilder.AddColumn<int>(
                name: "IspraznioKolicinu",
                table: "Radnici",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
