using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChat.Core.Migrations
{
    public partial class ChangedTableNamesToPlural : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipant_Conversation_ConversationId",
                table: "ConversationParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipant_User_ParticipantId",
                table: "ConversationParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversation_ConversationId",
                table: "Message");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationParticipant",
                table: "ConversationParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "ConversationParticipant",
                newName: "ConversationParticipants");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_Message_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ConversationId",
                table: "Messages",
                newName: "IX_Messages_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationParticipant_ParticipantId",
                table: "ConversationParticipants",
                newName: "IX_ConversationParticipants_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationParticipants",
                table: "ConversationParticipants",
                columns: new[] { "ConversationId", "ParticipantId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId",
                table: "ConversationParticipants",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Users_ParticipantId",
                table: "ConversationParticipants",
                column: "ParticipantId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Users_ParticipantId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationParticipants",
                table: "ConversationParticipants");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversation");

            migrationBuilder.RenameTable(
                name: "ConversationParticipants",
                newName: "ConversationParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Message",
                newName: "IX_Message_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ConversationId",
                table: "Message",
                newName: "IX_Message_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationParticipants_ParticipantId",
                table: "ConversationParticipant",
                newName: "IX_ConversationParticipant_ParticipantId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationParticipant",
                table: "ConversationParticipant",
                columns: new[] { "ConversationId", "ParticipantId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipant_Conversation_ConversationId",
                table: "ConversationParticipant",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipant_User_ParticipantId",
                table: "ConversationParticipant",
                column: "ParticipantId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversation_ConversationId",
                table: "Message",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_User_SenderId",
                table: "Message",
                column: "SenderId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
