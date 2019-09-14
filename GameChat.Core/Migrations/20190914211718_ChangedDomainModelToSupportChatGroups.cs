using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChat.Core.Migrations
{
    public partial class ChangedDomainModelToSupportChatGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_User_Participant1Id",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_User_Participant2Id",
                table: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_Participant1Id",
                table: "Conversation");

            migrationBuilder.DropIndex(
                name: "IX_Conversation_Participant2Id",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "Participant1Id",
                table: "Conversation");

            migrationBuilder.DropColumn(
                name: "Participant2Id",
                table: "Conversation");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Conversation",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConversationParticipant",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false),
                    ConversationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConversationParticipant", x => new { x.ConversationId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_ConversationParticipant_Conversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConversationParticipant_User_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConversationParticipant_ParticipantId",
                table: "ConversationParticipant",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConversationParticipant");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Conversation");

            migrationBuilder.AddColumn<int>(
                name: "Participant1Id",
                table: "Conversation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Participant2Id",
                table: "Conversation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_Participant1Id",
                table: "Conversation",
                column: "Participant1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Conversation_Participant2Id",
                table: "Conversation",
                column: "Participant2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_User_Participant1Id",
                table: "Conversation",
                column: "Participant1Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_User_Participant2Id",
                table: "Conversation",
                column: "Participant2Id",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
