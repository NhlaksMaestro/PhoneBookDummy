using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhoneBook.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhoneBook",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneBook", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneBookUserId = table.Column<int>(type: "INTEGER", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_PhoneBook_PhoneBookUserId",
                        column: x => x.PhoneBookUserId,
                        principalTable: "PhoneBook",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PhoneBook",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate" },
                values: new object[] { 1, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2122), "Edward", "Zeyn", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(1927) });

            migrationBuilder.InsertData(
                table: "PhoneBook",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate" },
                values: new object[] { 2, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2282), "Simon", "Gi", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2281) });

            migrationBuilder.InsertData(
                table: "PhoneBook",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate" },
                values: new object[] { 3, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2284), "King", "Kinh", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2283) });

            migrationBuilder.InsertData(
                table: "PhoneBook",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate" },
                values: new object[] { 4, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2285), "Frek", "Lek", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2285) });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate", "PhoneBookUserId", "PhoneNumber" },
                values: new object[] { 1, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2595), "Sui", "Lui", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2594), 1, "0817771662" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate", "PhoneBookUserId", "PhoneNumber" },
                values: new object[] { 2, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2766), "Jean", "Jut", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2765), 2, "0817819662" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate", "PhoneBookUserId", "PhoneNumber" },
                values: new object[] { 3, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2768), "Sam", "Lam", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2767), 3, "0819182662" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "CreatedDate", "FirstName", "LastName", "ModifiedDate", "PhoneBookUserId", "PhoneNumber" },
                values: new object[] { 4, new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2770), "Oros", "Loros", new DateTime(2021, 6, 29, 20, 33, 17, 129, DateTimeKind.Utc).AddTicks(2769), 4, "0817718928" });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PhoneBookUserId",
                table: "Contacts",
                column: "PhoneBookUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "PhoneBook");
        }
    }
}
