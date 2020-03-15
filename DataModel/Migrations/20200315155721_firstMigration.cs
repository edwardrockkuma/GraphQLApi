using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataModel.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("13909a3a-8d00-48da-8653-fab12e287757"), "John's address", "John " });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("e32fdaeb-d5bc-4410-9b21-bb553e0e77d3"), "Jane's address", "Jane " });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Description", "OwnerId", "Type" },
                values: new object[] { new Guid("bed27949-bd09-47ee-a698-ec0830224c40"), "Cash account for our users", new Guid("13909a3a-8d00-48da-8653-fab12e287757"), 0 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Description", "OwnerId", "Type" },
                values: new object[] { new Guid("e8b98949-a614-47b5-bfc0-d942ac07d47a"), "Savings account for our users", new Guid("e32fdaeb-d5bc-4410-9b21-bb553e0e77d3"), 1 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Description", "OwnerId", "Type" },
                values: new object[] { new Guid("1258601a-dd8f-4d79-855a-ec61c02924e5"), "Income account for our users", new Guid("e32fdaeb-d5bc-4410-9b21-bb553e0e77d3"), 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Owners");
        }
    }
}
