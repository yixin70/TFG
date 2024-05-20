using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TFG.Migrations
{
    /// <inheritdoc />
    public partial class Log_MediaStory_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InstagramLogId",
                table: "InstagramStory",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "InstagramLogId",
                table: "InstagramMedia",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuspicious",
                table: "InstagramLog",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_InstagramStory_InstagramLogId",
                table: "InstagramStory",
                column: "InstagramLogId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstagramMedia_InstagramLogId",
                table: "InstagramMedia",
                column: "InstagramLogId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramMedia_InstagramLog_InstagramLogId",
                table: "InstagramMedia",
                column: "InstagramLogId",
                principalTable: "InstagramLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramStory_InstagramLog_InstagramLogId",
                table: "InstagramStory",
                column: "InstagramLogId",
                principalTable: "InstagramLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstagramMedia_InstagramLog_InstagramLogId",
                table: "InstagramMedia");

            migrationBuilder.DropForeignKey(
                name: "FK_InstagramStory_InstagramLog_InstagramLogId",
                table: "InstagramStory");

            migrationBuilder.DropIndex(
                name: "IX_InstagramStory_InstagramLogId",
                table: "InstagramStory");

            migrationBuilder.DropIndex(
                name: "IX_InstagramMedia_InstagramLogId",
                table: "InstagramMedia");

            migrationBuilder.DropColumn(
                name: "InstagramLogId",
                table: "InstagramStory");

            migrationBuilder.DropColumn(
                name: "InstagramLogId",
                table: "InstagramMedia");

            migrationBuilder.DropColumn(
                name: "IsSuspicious",
                table: "InstagramLog");
        }
    }
}
