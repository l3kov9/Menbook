namespace Menbook.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserBeersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Beers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Beers_AuthorId",
                table: "Beers",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_AspNetUsers_AuthorId",
                table: "Beers",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_AspNetUsers_AuthorId",
                table: "Beers");

            migrationBuilder.DropIndex(
                name: "IX_Beers_AuthorId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Beers");
        }
    }
}
