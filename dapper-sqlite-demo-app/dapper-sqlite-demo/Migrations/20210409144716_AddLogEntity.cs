using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dapper_sqlite_demo.Migrations
{
    public partial class AddLogEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    HttpVerb = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    RequestHost = table.Column<string>(nullable: true),
                    RequestPath = table.Column<string>(nullable: true),
                    RequestQueryString = table.Column<string>(nullable: true),
                    RequestBody = table.Column<string>(nullable: true),
                    ResponseStatusCode = table.Column<int>(nullable: false),
                    ResponseBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");
        }
    }
}
