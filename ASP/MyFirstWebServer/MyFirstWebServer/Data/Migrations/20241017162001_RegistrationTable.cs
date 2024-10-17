using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstWebServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class RegistrationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreaModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityTypeModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTypeModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    AreaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityModel_AreaModel_AreaId",
                        column: x => x.AreaId,
                        principalTable: "AreaModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityModelCityTypeModel",
                columns: table => new
                {
                    CitiesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CityTypesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityModelCityTypeModel", x => new { x.CitiesId, x.CityTypesId });
                    table.ForeignKey(
                        name: "FK_CityModelCityTypeModel_CityModel_CitiesId",
                        column: x => x.CitiesId,
                        principalTable: "CityModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityModelCityTypeModel_CityTypeModel_CityTypesId",
                        column: x => x.CityTypesId,
                        principalTable: "CityTypeModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreetModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreetModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StreetModel_CityModel_CityId",
                        column: x => x.CityId,
                        principalTable: "CityModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HouseModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Slug = table.Column<string>(type: "TEXT", nullable: false),
                    StreetId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseModel_StreetModel_StreetId",
                        column: x => x.StreetId,
                        principalTable: "StreetModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityModel_AreaId",
                table: "CityModel",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_CityModelCityTypeModel_CityTypesId",
                table: "CityModelCityTypeModel",
                column: "CityTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseModel_StreetId",
                table: "HouseModel",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_StreetModel_CityId",
                table: "StreetModel",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityModelCityTypeModel");

            migrationBuilder.DropTable(
                name: "HouseModel");

            migrationBuilder.DropTable(
                name: "CityTypeModel");

            migrationBuilder.DropTable(
                name: "StreetModel");

            migrationBuilder.DropTable(
                name: "CityModel");

            migrationBuilder.DropTable(
                name: "AreaModel");
        }
    }
}
