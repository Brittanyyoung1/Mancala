using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SS.Mancala.PL.Migrations
{
    /// <inheritdoc />
    public partial class updatedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tblMoves",
                keyColumn: "Id",
                keyValue: new Guid("a2ec88f9-4788-4f9a-b36c-77618f535c1d"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("047f7b3f-0fd2-4521-86b0-982562f5ce31"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("04b473af-55cd-42fd-8d69-bfd6423b4df3"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("12a63a82-83d3-48ae-aa89-0effdb222f60"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("3de61ff6-ed6f-49ac-9195-d661b730d8ed"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("62c72d7f-779a-489e-bf89-afa77e92978d"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("64e6a829-5068-4912-8c12-70f380017ad1"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("b69d3f48-faaf-4f25-9acc-47a19e80bfa6"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("d66f1526-c696-494a-a426-0c1dc255a580"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("da99d358-7cf8-49a1-aacb-bdbba09d06b9"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("daf70485-8514-4f02-966b-e8206fb36c68"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("ea791f7f-e7c1-4f44-9a9a-091dcc0092ed"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("fbab2125-414a-4098-89ad-fe4ed0a45a55"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("fbca6639-5a7d-4943-9ffe-c8daab7ffcc9"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("fde58d83-c6df-4591-8908-2f11dc72f12c"));

            migrationBuilder.DeleteData(
                table: "tblGames",
                keyColumn: "Id",
                keyValue: new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"));

            migrationBuilder.DeleteData(
                table: "tblPlayers",
                keyColumn: "Id",
                keyValue: new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"));

            migrationBuilder.DeleteData(
                table: "tblPlayers",
                keyColumn: "Id",
                keyValue: new Guid("899bdc98-0153-4a8c-a133-1473ad223116"));

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(28)", unicode: false, maxLength: 28, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser_Id", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "tblGames",
                columns: new[] { "Id", "CurrentTurn", "DateCreated", "IsGameOver", "Player1Id", "Player2Id", "Status" },
                values: new object[] { new Guid("da550b0e-e193-48fd-951c-7b301571addc"), new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), new DateTime(2024, 12, 2, 11, 38, 15, 8, DateTimeKind.Local).AddTicks(1413), false, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), "Active" });

            migrationBuilder.InsertData(
                table: "tblPlayers",
                columns: new[] { "Id", "Score", "Username" },
                values: new object[,]
                {
                    { new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 0, "Taylor" },
                    { new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 0, "Brittany" }
                });

            migrationBuilder.InsertData(
                table: "tblUser",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "UserId" },
                values: new object[,]
                {
                    { new Guid("5b16c6d1-d68a-4962-9055-de8953ec2a74"), "Brian", "Foote", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "bfoote" },
                    { new Guid("86469d5d-af11-46fc-9ec9-f493abebfc71"), "John", "Doro", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "jdoro" },
                    { new Guid("c3b04087-ef02-410a-9cfa-302edb72b419"), "Steve", "Marin", "pYfdnNb8sO0FgS4H0MRSwLGOIME=", "smarin" },
                    { new Guid("c9444061-81af-452b-aa66-b0aa16d5eb1f"), "Other", "Other", "X1BEO/529yeajg8vCpiXXNv/OOk=", "sophie" }
                });

            migrationBuilder.InsertData(
                table: "tblMoves",
                columns: new[] { "Id", "GameId", "IsExtraTurn", "MoveNo", "PlayerId", "SourcePit", "StonesMoved", "TimeStamp" },
                values: new object[] { new Guid("bcf8e7db-9cc5-46ed-ae32-2fcd1facb176"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 1, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 2, 3, new DateTime(2024, 12, 2, 11, 38, 15, 8, DateTimeKind.Local).AddTicks(5666) });

            migrationBuilder.InsertData(
                table: "tblPits",
                columns: new[] { "Id", "GameId", "IsMancala", "PitPosition", "PlayerId", "Stones" },
                values: new object[,]
                {
                    { new Guid("0d443cfb-4310-449e-b927-1d4da3cbac8d"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 8, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("12507b6d-9dd8-4e0f-b979-4f284bcc6f27"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 3, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 },
                    { new Guid("149556a3-ec35-4bba-810c-8f0d4a8cd435"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 4, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 },
                    { new Guid("41772f81-8e04-4427-8d4b-0e9ceecdfe0a"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), true, 14, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 0 },
                    { new Guid("4d766438-6083-4424-bfc2-11e87c88a43a"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 12, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("63a83d7c-4252-408e-9fde-56d7aa781616"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 2, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 },
                    { new Guid("7c921e1e-1d96-4f8d-a066-2c042cd9a4c3"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 13, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("7fc6d333-9bc0-4f8d-8255-9189a7dbef26"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 5, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 },
                    { new Guid("95301b8d-f924-4c67-97de-0a92984f8b5b"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 9, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("9ab60cad-fe1e-4fb7-82b4-5ed083645f34"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), true, 7, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 0 },
                    { new Guid("a1deafad-abf9-4faf-90ac-d4f70c91399c"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 11, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("c0ff3d22-75f9-416f-83f1-9d3cd62fae6e"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 10, new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"), 4 },
                    { new Guid("c71c7660-69af-4dda-bd22-73f6f259b910"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 1, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 },
                    { new Guid("d5d8c483-f97e-459d-8ec5-dd3f7001057b"), new Guid("da550b0e-e193-48fd-951c-7b301571addc"), false, 6, new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"), 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DeleteData(
                table: "tblMoves",
                keyColumn: "Id",
                keyValue: new Guid("bcf8e7db-9cc5-46ed-ae32-2fcd1facb176"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("0d443cfb-4310-449e-b927-1d4da3cbac8d"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("12507b6d-9dd8-4e0f-b979-4f284bcc6f27"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("149556a3-ec35-4bba-810c-8f0d4a8cd435"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("41772f81-8e04-4427-8d4b-0e9ceecdfe0a"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("4d766438-6083-4424-bfc2-11e87c88a43a"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("63a83d7c-4252-408e-9fde-56d7aa781616"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("7c921e1e-1d96-4f8d-a066-2c042cd9a4c3"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("7fc6d333-9bc0-4f8d-8255-9189a7dbef26"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("95301b8d-f924-4c67-97de-0a92984f8b5b"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("9ab60cad-fe1e-4fb7-82b4-5ed083645f34"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("a1deafad-abf9-4faf-90ac-d4f70c91399c"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("c0ff3d22-75f9-416f-83f1-9d3cd62fae6e"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("c71c7660-69af-4dda-bd22-73f6f259b910"));

            migrationBuilder.DeleteData(
                table: "tblPits",
                keyColumn: "Id",
                keyValue: new Guid("d5d8c483-f97e-459d-8ec5-dd3f7001057b"));

            migrationBuilder.DeleteData(
                table: "tblGames",
                keyColumn: "Id",
                keyValue: new Guid("da550b0e-e193-48fd-951c-7b301571addc"));

            migrationBuilder.DeleteData(
                table: "tblPlayers",
                keyColumn: "Id",
                keyValue: new Guid("19b965ef-7f46-49b7-bd57-e031ea6670c3"));

            migrationBuilder.DeleteData(
                table: "tblPlayers",
                keyColumn: "Id",
                keyValue: new Guid("87e41b9b-7fdc-4064-a0eb-685b2dab1211"));

            migrationBuilder.InsertData(
                table: "tblGames",
                columns: new[] { "Id", "CurrentTurn", "DateCreated", "IsGameOver", "Player1Id", "Player2Id", "Status" },
                values: new object[] { new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), new DateTime(2024, 11, 22, 0, 37, 33, 276, DateTimeKind.Local).AddTicks(8690), false, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), "Active" });

            migrationBuilder.InsertData(
                table: "tblPlayers",
                columns: new[] { "Id", "Score", "Username" },
                values: new object[,]
                {
                    { new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 0, "Taylor" },
                    { new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 0, "Brittany" }
                });

            migrationBuilder.InsertData(
                table: "tblMoves",
                columns: new[] { "Id", "GameId", "IsExtraTurn", "MoveNo", "PlayerId", "SourcePit", "StonesMoved", "TimeStamp" },
                values: new object[] { new Guid("a2ec88f9-4788-4f9a-b36c-77618f535c1d"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 1, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 2, 3, new DateTime(2024, 11, 22, 0, 37, 33, 277, DateTimeKind.Local).AddTicks(6609) });

            migrationBuilder.InsertData(
                table: "tblPits",
                columns: new[] { "Id", "GameId", "IsMancala", "PitPosition", "PlayerId", "Stones" },
                values: new object[,]
                {
                    { new Guid("047f7b3f-0fd2-4521-86b0-982562f5ce31"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 6, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("04b473af-55cd-42fd-8d69-bfd6423b4df3"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 3, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("12a63a82-83d3-48ae-aa89-0effdb222f60"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 4, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("3de61ff6-ed6f-49ac-9195-d661b730d8ed"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 2, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("62c72d7f-779a-489e-bf89-afa77e92978d"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 10, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 },
                    { new Guid("64e6a829-5068-4912-8c12-70f380017ad1"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 13, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 },
                    { new Guid("b69d3f48-faaf-4f25-9acc-47a19e80bfa6"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 8, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 },
                    { new Guid("d66f1526-c696-494a-a426-0c1dc255a580"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 5, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("da99d358-7cf8-49a1-aacb-bdbba09d06b9"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), true, 14, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 0 },
                    { new Guid("daf70485-8514-4f02-966b-e8206fb36c68"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 11, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 },
                    { new Guid("ea791f7f-e7c1-4f44-9a9a-091dcc0092ed"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 1, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 4 },
                    { new Guid("fbab2125-414a-4098-89ad-fe4ed0a45a55"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), true, 7, new Guid("199e0690-d8cf-49c2-991b-cbc633633aa3"), 0 },
                    { new Guid("fbca6639-5a7d-4943-9ffe-c8daab7ffcc9"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 9, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 },
                    { new Guid("fde58d83-c6df-4591-8908-2f11dc72f12c"), new Guid("bcba9320-cf7f-483c-9a82-827dd7a78880"), false, 12, new Guid("899bdc98-0153-4a8c-a133-1473ad223116"), 4 }
                });
        }
    }
}
