using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DBContext.Main.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_SFs",
                table: "User_SFs");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "User_SFs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_SFs",
                table: "User_SFs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_SFs_User1Id",
                table: "User_SFs",
                column: "User1Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User_SFs",
                table: "User_SFs");

            migrationBuilder.DropIndex(
                name: "IX_User_SFs_User1Id",
                table: "User_SFs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "User_SFs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User_SFs",
                table: "User_SFs",
                columns: new[] { "User1Id", "User2Id" });
        }
    }
}
