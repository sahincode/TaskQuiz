using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPronia.Migrations
{
    public partial class AddinIsHoverToIMage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHover",
                table: "Images",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHover",
                table: "Images");
        }
    }
}
