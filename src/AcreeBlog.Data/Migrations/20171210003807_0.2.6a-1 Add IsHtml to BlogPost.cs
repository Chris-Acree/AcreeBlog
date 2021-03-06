﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AcreeBlog.Data.Migrations
{
    public partial class _026a1AddIsHtmltoBlogPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHtml",
                table: "BlogPost",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHtml",
                table: "BlogPost");
        }
    }
}
