using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartCareerPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Instructor",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Courses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    InteractionType = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AdditionalData = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCourseInteractions_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseInteractions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2580), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2580), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2580), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2580), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2580), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2590), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2600), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2610), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2620), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2660), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2670), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2680), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2690), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2700), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2710), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2720), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2730), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2740), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2740), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 24, 2, 59, 14, 922, DateTimeKind.Utc).AddTicks(2740), new List<string>() });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId_CourseId",
                table: "Enrollments",
                columns: new[] { "UserId", "CourseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseInteractions_CourseId",
                table: "UserCourseInteractions",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseInteractions_UserId",
                table: "UserCourseInteractions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "UserCourseInteractions");

            migrationBuilder.DropColumn(
                name: "Instructor",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "Courses");

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5220), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5220), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5230), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5240), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5250), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 36,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 37,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 38,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 39,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 40,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5260), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 41,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 42,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 43,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 44,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 45,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 46,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5270), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 47,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 48,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 49,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 50,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 51,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 52,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 53,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 54,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 55,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 56,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 57,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 58,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 59,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 60,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 61,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5280), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 62,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 63,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 64,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 65,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 66,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 67,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 68,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 69,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 70,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 71,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5290), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 72,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 73,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 74,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 75,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 76,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 77,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 78,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5300), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 79,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 80,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 81,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 82,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 83,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 84,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 85,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 86,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 87,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 88,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 89,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 90,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 91,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 92,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5310), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 93,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 94,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 95,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 96,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 97,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 98,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 99,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 101,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 102,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 103,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 104,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 105,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 106,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5320), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 107,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 108,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 109,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 110,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 111,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 112,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 113,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 114,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 115,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 116,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 117,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 118,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5330), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 119,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 120,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 121,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 122,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 123,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 124,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 125,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 126,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 127,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 128,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 129,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 130,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 131,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 132,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5340), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 133,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 134,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 135,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 136,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 137,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 138,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 139,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 140,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 141,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 142,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 143,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 144,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 145,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 146,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 147,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5350), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 148,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 149,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), new List<string>() });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: 150,
                columns: new[] { "CreatedAt", "RelatedSkills" },
                values: new object[] { new DateTime(2025, 7, 18, 13, 3, 29, 652, DateTimeKind.Utc).AddTicks(5360), new List<string>() });
        }
    }
}
