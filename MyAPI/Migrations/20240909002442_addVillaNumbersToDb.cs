using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyAPI.Migrations
{
    /// <inheritdoc />
    public partial class addVillaNumbersToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "villaNumbers",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaID = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villaNumbers", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_villaNumbers_villas_VillaID",
                        column: x => x.VillaID,
                        principalTable: "villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 9, 3, 24, 41, 334, DateTimeKind.Local).AddTicks(8779));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 9, 3, 24, 41, 334, DateTimeKind.Local).AddTicks(8833));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 9, 3, 24, 41, 334, DateTimeKind.Local).AddTicks(8836));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 9, 3, 24, 41, 334, DateTimeKind.Local).AddTicks(8838));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 9, 3, 24, 41, 334, DateTimeKind.Local).AddTicks(8840));

            migrationBuilder.CreateIndex(
                name: "IX_villaNumbers_VillaID",
                table: "villaNumbers",
                column: "VillaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villaNumbers");

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 5, 20, 8, 29, 385, DateTimeKind.Local).AddTicks(3052));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 5, 20, 8, 29, 385, DateTimeKind.Local).AddTicks(3104));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 5, 20, 8, 29, 385, DateTimeKind.Local).AddTicks(3107));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 5, 20, 8, 29, 385, DateTimeKind.Local).AddTicks(3110));

            migrationBuilder.UpdateData(
                table: "villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 9, 5, 20, 8, 29, 385, DateTimeKind.Local).AddTicks(3112));
        }
    }
}
