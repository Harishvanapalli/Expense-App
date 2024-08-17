using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpenseGroups",
                columns: table => new
                {
                    ExpenseGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Group_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Created_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Members = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Expenses = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseGroups", x => x.ExpenseGroupID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    ExpenseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Paid_by = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Split_among = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpenseGroupID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.ExpenseID);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseGroups_ExpenseGroupID",
                        column: x => x.ExpenseGroupID,
                        principalTable: "ExpenseGroups",
                        principalColumn: "ExpenseGroupID");
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ExpenseGroups",
                        principalColumn: "ExpenseGroupID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "Harish@123", "Administrator", "harishvanapalli9@gmail.com" },
                    { 2, "Ravi@123", "Administrator", "ravivanapalli9@gmail.com" },
                    { 3, "Dileep@123", "User", "dileepthondupu8@gmail.com" },
                    { 4, "Mohan@123", "User", "mohanuchula10@gmail.com" },
                    { 5, "Ramesh@123", "User", "rameshupparapalli108@gmail.com" },
                    { 6, "Naveen@123", "User", "naveenbuddha9@gmail.com" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseGroupID",
                table: "Expenses",
                column: "ExpenseGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupId",
                table: "Expenses",
                column: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ExpenseGroups");
        }
    }
}
