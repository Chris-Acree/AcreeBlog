﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AcreeBlog.Data.Migrations
{
    public partial class _025a4AddSlugToBlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "BlogPost",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "BlogPost");
        }
    }
}
