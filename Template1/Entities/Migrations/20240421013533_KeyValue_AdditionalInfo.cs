using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template1.Entities.Migrations
{
    /// <inheritdoc />
    public partial class KeyValue_AdditionalInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyValues",
                columns: table => new
                {
                    KeyValueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyValues", x => x.KeyValueId);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalInfos",
                columns: table => new
                {
                    AdditionalInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyValueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalInfos", x => x.AdditionalInfoId);
                    table.ForeignKey(
                        name: "FK_AdditionalInfos_KeyValues_KeyValueId",
                        column: x => x.KeyValueId,
                        principalTable: "KeyValues",
                        principalColumn: "KeyValueId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalInfos_KeyValueId",
                table: "AdditionalInfos",
                column: "KeyValueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalInfos");

            migrationBuilder.DropTable(
                name: "KeyValues");
        }
    }
}
