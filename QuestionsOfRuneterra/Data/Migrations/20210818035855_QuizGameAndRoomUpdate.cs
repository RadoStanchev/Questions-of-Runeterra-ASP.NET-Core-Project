using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuestionsOfRuneterra.Data.Migrations
{
    public partial class QuizGameAndRoomUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizGameSessions_Answers_SelectedAnswerId",
                table: "QuizGameSessions");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "SelectedAnswerId",
                table: "QuizGameSessions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedOn",
                table: "QuizGames",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "QuizGames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGameSessions_Answers_SelectedAnswerId",
                table: "QuizGameSessions",
                column: "SelectedAnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuizGameSessions_Answers_SelectedAnswerId",
                table: "QuizGameSessions");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "QuizGames");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedAnswerId",
                table: "QuizGameSessions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinishedOn",
                table: "QuizGames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuizGameSessions_Answers_SelectedAnswerId",
                table: "QuizGameSessions",
                column: "SelectedAnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
