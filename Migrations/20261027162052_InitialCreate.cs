using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FatigueStrengthApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FatiguePredictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExperimentId = table.Column<int>(type: "INTEGER", nullable: false),
                    PredictedStrength = table.Column<double>(type: "REAL", nullable: false),
                    TargetStrength = table.Column<double>(type: "REAL", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FatiguePredictions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptimizationRecommendations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExperimentId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecommendedMaterial = table.Column<string>(type: "TEXT", nullable: false),
                    RecommendedLayerThickness = table.Column<double>(type: "REAL", nullable: false),
                    RecommendedInfillDensity = table.Column<double>(type: "REAL", nullable: false),
                    RecommendedInfillPattern = table.Column<string>(type: "TEXT", nullable: false),
                    Explanation = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptimizationRecommendations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PrintExperiments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MaterialType = table.Column<string>(type: "TEXT", nullable: false),
                    LayerThickness = table.Column<double>(type: "REAL", nullable: false),
                    InfillDensity = table.Column<double>(type: "REAL", nullable: false),
                    InfillPattern = table.Column<string>(type: "TEXT", nullable: false),
                    FatigueStrength = table.Column<double>(type: "REAL", nullable: true),
                    CycleCount = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintExperiments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FatiguePredictions");

            migrationBuilder.DropTable(
                name: "OptimizationRecommendations");

            migrationBuilder.DropTable(
                name: "PrintExperiments");
        }
    }
}
