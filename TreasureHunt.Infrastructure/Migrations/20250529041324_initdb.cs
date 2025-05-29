using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TreasureHunt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TreasureMaps",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Rows = table.Column<int>(type: "int", nullable: false),
                    Columns = table.Column<int>(type: "int", nullable: false),
                    MaxLevel = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreasureMaps", x => x.MapId);
                });

            migrationBuilder.CreateTable(
                name: "MapCells",
                columns: table => new
                {
                    CellId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    RowIndex = table.Column<int>(type: "int", nullable: false),
                    ColIndex = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapCells", x => x.CellId);
                    table.ForeignKey(
                        name: "FK_MapCells_TreasureMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "TreasureMaps",
                        principalColumn: "MapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Solutions",
                columns: table => new
                {
                    SolutionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    FuelUsed = table.Column<double>(type: "float", nullable: false),
                    SolvedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Solutions", x => x.SolutionId);
                    table.ForeignKey(
                        name: "FK_Solutions_TreasureMaps_MapId",
                        column: x => x.MapId,
                        principalTable: "TreasureMaps",
                        principalColumn: "MapId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MapCells_MapId",
                table: "MapCells",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_MapId",
                table: "Solutions",
                column: "MapId");

            // var faker = new Bogus.Faker<TreasureHunt.Infrastructure.Data.Entities.TreasureMap>();
            // faker.RuleFor(m => m.Name, f => f.Company.CompanyName())
            //     .RuleFor(m => m.Rows, f => f.Random.Int(1, 500))
            //     .RuleFor(m => m.Columns, f => f.Random.Int(1, 500))
            //     .RuleFor(m => m.MaxLevel, f => f.Random.Int(1, 10))
            //     .RuleFor(m => m.CreatedAt, f => f.Date.Recent());
            // var maps = faker.Generate(20);


            // migrationBuilder.InsertData(
            //     table: "TreasureMaps",
            //     columns: new[] { "Name", "Rows", "Columns", "MaxLevel", "CreatedAt" },
            //     values: maps.Select(m => new object[] { m.Name, m.Rows, m.Columns, m.MaxLevel, m.CreatedAt }).ToArray());

            // var mapCells = new Bogus.Faker<TreasureHunt.Infrastructure.Data.Entities.MapCell>()
            //     .RuleFor(c => c.MapId, f => f.PickRandom(maps).MapId)
            //     .RuleFor(c => c.RowIndex, f => f.Random.Int(0, 499))
            //     .RuleFor(c => c.ColIndex, f => f.Random.Int(0, 499))
            //     .RuleFor(c => c.Value, f => f.Random.Int(0, 100))
            //     .Generate(20);

            // migrationBuilder.InsertData(
            //     table: "MapCells",
            //     columns: new[] { "CellId", "MapId", "RowIndex", "ColIndex", "Value" },
            //     values: mapCells.Select(c => new object[] { c.CellId, c.MapId, c.RowIndex, c.ColIndex, c.Value }).ToArray());
            // var solutions = new Bogus.Faker<TreasureHunt.Infrastructure.Data.Entities.Solution>()
            //     .RuleFor(s => s.MapId, f => f.PickRandom(maps).MapId)
            //     .RuleFor(s => s.FuelUsed, f => f.Random.Double(0, 1000))
            //     .RuleFor(s => s.SolvedAt, f => f.Date.Recent())
            //       .Generate(20);

            // migrationBuilder.InsertData(
            //     table: "Solutions",
            //     columns: new[] { "SolutionId", "MapId", "FuelUsed", "SolvedAt" },
            //     values: solutions.Select(s => new object[] { s.SolutionId, s.MapId, s.FuelUsed, s.SolvedAt }).ToArray());

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MapCells");

            migrationBuilder.DropTable(
                name: "Solutions");

            migrationBuilder.DropTable(
                name: "TreasureMaps");
        }

    }
}
