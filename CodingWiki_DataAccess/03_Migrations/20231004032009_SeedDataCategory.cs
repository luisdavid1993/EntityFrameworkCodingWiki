using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodingWiki_DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Name]) VALUES ('Catagory 1')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Name]) VALUES ('Catagory 2')");
            migrationBuilder.Sql("INSERT INTO [dbo].[Categories]([Name]) VALUES ('Catagory 3')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete [dbo].[Categories] where Name = 'Catagory 1'");
            migrationBuilder.Sql("Delete [dbo].[Categories] where Name = 'Catagory 2'");
            migrationBuilder.Sql("Delete [dbo].[Categories] where Name = 'Catagory 3'");
        }
    }
}
