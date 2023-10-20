using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebSocialNet.Dal.Migrations
{
    /// <inheritdoc />
    public partial class chattable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Sender = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ChatId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<string>(type: "text", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    ChatName = table.Column<string>(type: "text", nullable: false),
                    IsGroupChat = table.Column<bool>(type: "boolean", nullable: false),
                    UsersId = table.Column<List<string>>(type: "text[]", nullable: false),
                    UserAdminId = table.Column<string>(type: "text", nullable: true),
                    LatestMessageId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_Chats_Message_LatestMessageId",
                        column: x => x.LatestMessageId,
                        principalTable: "Message",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_LatestMessageId",
                table: "Chats",
                column: "LatestMessageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");
        }
    }
}
