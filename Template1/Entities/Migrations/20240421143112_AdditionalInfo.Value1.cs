using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template1.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalInfoValue1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Value1",
                table: "AdditionalInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value1",
                table: "AdditionalInfos");
        }
    }
}
