using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fluent_BookAuthor_ManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fluent_BookAuthor",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_BookAuthor", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthor_Fluent_Author_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Fluent_Author",
                        principalColumn: "Author_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fluent_BookAuthor_Fluent_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Fluent_Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookAuthor_AuthorId",
                table: "Fluent_BookAuthor",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fluent_BookAuthor");
        }
    }
}
