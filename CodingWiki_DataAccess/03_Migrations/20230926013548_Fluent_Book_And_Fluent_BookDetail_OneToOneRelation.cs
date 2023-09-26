using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Fluent_Book_And_Fluent_BookDetail_OneToOneRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Fluent_BookDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fluent_Author",
                columns: table => new
                {
                    Author_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Author", x => x.Author_Id);
                });

            migrationBuilder.CreateTable(
                name: "Fluent_Publisher",
                columns: table => new
                {
                    Publisher_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fluent_Publisher", x => x.Publisher_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fluent_BookDetail_BookId",
                table: "Fluent_BookDetail",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Fluent_BookDetail_Fluent_Book_BookId",
                table: "Fluent_BookDetail",
                column: "BookId",
                principalTable: "Fluent_Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fluent_BookDetail_Fluent_Book_BookId",
                table: "Fluent_BookDetail");

            migrationBuilder.DropTable(
                name: "Fluent_Author");

            migrationBuilder.DropTable(
                name: "Fluent_Publisher");

            migrationBuilder.DropIndex(
                name: "IX_Fluent_BookDetail_BookId",
                table: "Fluent_BookDetail");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Fluent_BookDetail");
        }
    }
}
