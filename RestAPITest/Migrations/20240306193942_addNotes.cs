﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestAPITest.Migrations
{
    /// <inheritdoc />
    public partial class addNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "categories",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "categories");
        }
    }
}
