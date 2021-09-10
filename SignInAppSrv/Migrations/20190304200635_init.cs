using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SignInAppSrv.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    creatorId = table.Column<int>(nullable: false),
                    modifyTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TimeFrom = table.Column<TimeSpan>(nullable: false),
                    TimeTo = table.Column<TimeSpan>(nullable: false),
                    DayId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupMemberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    userIdentifier = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMemberships_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupMemberships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Days",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Monday" },
                    { 2, "Tuesday" },
                    { 3, "Wednesday" },
                    { 4, "Thursday" },
                    { 5, "Friday" },
                    { 6, "Saturday" },
                    { 7, "Sunday" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Identifier", "Name", "creatorId", "modifyTime" },
                values: new object[] { 1, "tajnagrupa", "Plan lekcji", 1, new DateTime(2019, 3, 4, 21, 6, 35, 717, DateTimeKind.Local) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FirstName", "LastName", "Password", "PasswordHash", "PasswordSalt", "PhoneNumber", "Username" },
                values: new object[,]
                {
                    { 1, "gcieplechowicz@gmail.com", "Grzegorz", "Cieplechowicz", "atomics1", null, null, "573248012", "gcieplechowicz" },
                    { 2, "gcieplechowicz2@gmail.com", "Anna", "Cieplechowicz", "atomics1", null, null, "573248012", "acieplechowicz" },
                    { 3, "gcieplechowicz3@gmail.com", "Jan", "Cieplechowicz", "atomics1", null, null, "573248012", "jcieplechowicz" }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "DayId", "Description", "GroupId", "Name", "TimeFrom", "TimeTo" },
                values: new object[,]
                {
                    { 1, 1, "200", 1, "Religia", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 45, 0, 0) },
                    { 25, 5, "205", 1, "Angielski", new TimeSpan(0, 12, 10, 0, 0), new TimeSpan(0, 12, 55, 0, 0) },
                    { 24, 5, "205", 1, "WOS", new TimeSpan(0, 11, 20, 0, 0), new TimeSpan(0, 12, 5, 0, 0) },
                    { 23, 5, "205", 1, "Przyroda", new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 11, 15, 0, 0) },
                    { 22, 5, "254", 1, "Polski", new TimeSpan(0, 9, 40, 0, 0), new TimeSpan(0, 10, 25, 0, 0) },
                    { 21, 5, "155", 1, "Chemia", new TimeSpan(0, 8, 50, 0, 0), new TimeSpan(0, 9, 35, 0, 0) },
                    { 20, 5, "200", 1, "Religia", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 45, 0, 0) },
                    { 19, 4, "205", 1, "W-F", new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 11, 15, 0, 0) },
                    { 18, 4, "254", 1, "WOS", new TimeSpan(0, 9, 40, 0, 0), new TimeSpan(0, 10, 25, 0, 0) },
                    { 17, 4, "155", 1, "Historia", new TimeSpan(0, 8, 50, 0, 0), new TimeSpan(0, 9, 35, 0, 0) },
                    { 16, 4, "200", 1, "Polski", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 45, 0, 0) },
                    { 14, 3, "205", 1, "Matematyka", new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 11, 15, 0, 0) },
                    { 15, 3, "205", 1, "WDŻ", new TimeSpan(0, 11, 20, 0, 0), new TimeSpan(0, 12, 5, 0, 0) },
                    { 12, 3, "155", 1, "Chemia", new TimeSpan(0, 8, 50, 0, 0), new TimeSpan(0, 9, 35, 0, 0) },
                    { 10, 3, "200", 1, "W-F", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 45, 0, 0) },
                    { 9, 2, "205", 1, "Polski", new TimeSpan(0, 11, 20, 0, 0), new TimeSpan(0, 12, 5, 0, 0) },
                    { 8, 2, "205", 1, "Biologia", new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 11, 15, 0, 0) },
                    { 7, 2, "254", 1, "Matematyka", new TimeSpan(0, 9, 40, 0, 0), new TimeSpan(0, 10, 25, 0, 0) },
                    { 6, 2, "155", 1, "Muzyka", new TimeSpan(0, 8, 50, 0, 0), new TimeSpan(0, 9, 35, 0, 0) },
                    { 5, 2, "200", 1, "W-F", new TimeSpan(0, 8, 0, 0, 0), new TimeSpan(0, 8, 45, 0, 0) },
                    { 4, 1, "205", 1, "Przyroda", new TimeSpan(0, 10, 30, 0, 0), new TimeSpan(0, 11, 15, 0, 0) },
                    { 3, 1, "254", 1, "Matematyka", new TimeSpan(0, 9, 40, 0, 0), new TimeSpan(0, 10, 25, 0, 0) },
                    { 2, 1, "155", 1, "Polski", new TimeSpan(0, 8, 50, 0, 0), new TimeSpan(0, 9, 35, 0, 0) },
                    { 13, 3, "254", 1, "Matematyka", new TimeSpan(0, 9, 40, 0, 0), new TimeSpan(0, 10, 25, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "GroupMemberships",
                columns: new[] { "Id", "GroupId", "UserId", "userIdentifier" },
                values: new object[,]
                {
                    { 2, 1, 2, "Anna Cieplechowicz" },
                    { 1, 1, 1, "Grzegorz Cieplechowicz" },
                    { 3, 1, 3, "Jan Cieplechowicz" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_DayId",
                table: "Assignments",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_GroupId",
                table: "GroupMemberships",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupMemberships_UserId",
                table: "GroupMemberships",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "GroupMemberships");

            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
