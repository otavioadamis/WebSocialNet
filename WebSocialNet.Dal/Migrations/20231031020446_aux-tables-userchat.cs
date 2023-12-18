using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocialNet.Dal.Migrations
{
    /// <inheritdoc />
    public partial class auxtablesuserchat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Chats",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
