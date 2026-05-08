using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniSIMRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dokters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NoSIP = table.Column<string>(type: "TEXT", nullable: false),
                    NamaDokter = table.Column<string>(type: "TEXT", nullable: false),
                    Spesialisasi = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pasiens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NoRekamMedis = table.Column<string>(type: "TEXT", nullable: false),
                    NamaLengkap = table.Column<string>(type: "TEXT", nullable: false),
                    NIK = table.Column<string>(type: "TEXT", nullable: false),
                    JenisKelamin = table.Column<string>(type: "TEXT", nullable: false),
                    GolonganDarah = table.Column<string>(type: "TEXT", nullable: false),
                    TanggalLahir = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasiens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PashwordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RekamMediss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PasienId = table.Column<int>(type: "INTEGER", nullable: false),
                    DokterId = table.Column<int>(type: "INTEGER", nullable: false),
                    TanggalPeriksa = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KeluhanUtama = table.Column<string>(type: "TEXT", nullable: false),
                    Diagnosa = table.Column<string>(type: "TEXT", nullable: false),
                    Tindakan = table.Column<string>(type: "TEXT", nullable: false),
                    ResepObat = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RekamMediss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RekamMediss_Dokters_DokterId",
                        column: x => x.DokterId,
                        principalTable: "Dokters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RekamMediss_Pasiens_PasienId",
                        column: x => x.PasienId,
                        principalTable: "Pasiens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RekamMediss_DokterId",
                table: "RekamMediss",
                column: "DokterId");

            migrationBuilder.CreateIndex(
                name: "IX_RekamMediss_PasienId",
                table: "RekamMediss",
                column: "PasienId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RekamMediss");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Dokters");

            migrationBuilder.DropTable(
                name: "Pasiens");
        }
    }
}
