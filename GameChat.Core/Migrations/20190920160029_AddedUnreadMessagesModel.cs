using Microsoft.EntityFrameworkCore.Migrations;

namespace GameChat.Core.Migrations
{
    public partial class AddedUnreadMessagesModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ReadByAllParticipants",
                table: "Messages",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "UnreadMessages",
                columns: table => new
                {
                    MessageId = table.Column<int>(nullable: false),
                    ParticipantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnreadMessages", x => new { x.MessageId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_UnreadMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnreadMessages_Users_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnreadMessages_ParticipantId",
                table: "UnreadMessages",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnreadMessages");

            migrationBuilder.DropColumn(
                name: "ReadByAllParticipants",
                table: "Messages");
        }
    }
}
