using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WordReminder.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keyword",
                columns: table => new
                {
                    KeywordId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyword", x => x.KeywordId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    Fullname = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "KeywordMeaning",
                columns: table => new
                {
                    KeywordMeaningId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeywordId = table.Column<int>(nullable: false),
                    KeywordType = table.Column<int>(nullable: false),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordMeaning", x => x.KeywordMeaningId);
                    table.ForeignKey(
                        name: "FK_KeywordMeaning_Keyword_KeywordId",
                        column: x => x.KeywordId,
                        principalTable: "Keyword",
                        principalColumn: "KeywordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeywordMeaningSentence",
                columns: table => new
                {
                    KeywordMeaningSentenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KeywordMeaningId = table.Column<int>(nullable: false),
                    Sentence = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeywordMeaningSentence", x => x.KeywordMeaningSentenceId);
                    table.ForeignKey(
                        name: "FK_KeywordMeaningSentence_KeywordMeaning_KeywordMeaningId",
                        column: x => x.KeywordMeaningId,
                        principalTable: "KeywordMeaning",
                        principalColumn: "KeywordMeaningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KeywordMeaning_KeywordId",
                table: "KeywordMeaning",
                column: "KeywordId");

            migrationBuilder.CreateIndex(
                name: "IX_KeywordMeaningSentence_KeywordMeaningId",
                table: "KeywordMeaningSentence",
                column: "KeywordMeaningId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeywordMeaningSentence");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "KeywordMeaning");

            migrationBuilder.DropTable(
                name: "Keyword");
        }
    }
}
