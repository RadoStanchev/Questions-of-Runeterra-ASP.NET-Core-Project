using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionsOfRuneterra.Data.Migrations
{
    public partial class FriendshipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserFriendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Friendships",
                newName: "SecondFriendId");

            migrationBuilder.AddColumn<string>(
                name: "FirstFriendId",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Friendships",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                columns: new[] { "FirstFriendId", "SecondFriendId" });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ApplicationUserId",
                table: "Friendships",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserId",
                table: "Friendships",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_AspNetUsers_ApplicationUserId",
                table: "Friendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_ApplicationUserId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "FirstFriendId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Friendships");

            migrationBuilder.RenameColumn(
                name: "SecondFriendId",
                table: "Friendships",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friendships",
                table: "Friendships",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ApplicationUserFriendship",
                columns: table => new
                {
                    FriendsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FriendshipsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserFriendship", x => new { x.FriendsId, x.FriendshipsId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship_AspNetUsers_FriendsId",
                        column: x => x.FriendsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserFriendship_Friendships_FriendshipsId",
                        column: x => x.FriendshipsId,
                        principalTable: "Friendships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserFriendship_FriendshipsId",
                table: "ApplicationUserFriendship",
                column: "FriendshipsId");
        }
    }
}
