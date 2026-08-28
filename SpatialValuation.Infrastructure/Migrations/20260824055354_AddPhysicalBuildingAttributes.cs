using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpatialValuation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPhysicalBuildingAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BuildingFootprintSquareMeters",
                table: "properties",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "FinishGrade",
                table: "properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfStories",
                table: "properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearBuilt",
                table: "properties",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PropertySales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PropertyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SalePrice = table.Column<double>(type: "double precision", nullable: false),
                    SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertySales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertySales_properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertySales_PropertyId",
                table: "PropertySales",
                column: "PropertyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertySales");

            migrationBuilder.DropColumn(
                name: "BuildingFootprintSquareMeters",
                table: "properties");

            migrationBuilder.DropColumn(
                name: "FinishGrade",
                table: "properties");

            migrationBuilder.DropColumn(
                name: "NumberOfStories",
                table: "properties");

            migrationBuilder.DropColumn(
                name: "YearBuilt",
                table: "properties");
        }
    }
}
