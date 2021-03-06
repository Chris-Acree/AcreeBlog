﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AcreeBlog.Data.Migrations
{
    public partial class _023b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHomePage",
                table: "Topic",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnHomePage",
                table: "Topic");
        }
    }
}
