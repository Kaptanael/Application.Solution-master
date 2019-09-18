using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 64, nullable: false),
                    LastName = table.Column<string>(maxLength: 64, nullable: false),
                    Email = table.Column<string>(maxLength: 64, nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 14, nullable: false),
                    Address = table.Column<string>(maxLength: 512, nullable: false),
                    Role = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Value",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Value", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Task",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    Description = table.Column<string>(maxLength: 512, nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_UserId",
                table: "Task",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task");

            migrationBuilder.DropTable(
                name: "Value");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
