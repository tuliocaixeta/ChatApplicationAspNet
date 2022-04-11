using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication.Data.Migrations
{
    public partial class CreateMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MessageContent = table.Column<string>(type: "TEXT", nullable: false),
                    MessageSendingTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    User = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");
        }
    }
}
