using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GarageClickerBackend.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerItems_PlayerId",
                table: "PlayerItems");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_PlayerId_ItemId",
                table: "PlayerItems",
                columns: new[] { "PlayerId", "ItemId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerItems_PlayerId_ItemId",
                table: "PlayerItems");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerItems_PlayerId",
                table: "PlayerItems",
                column: "PlayerId");
        }
    }
}
