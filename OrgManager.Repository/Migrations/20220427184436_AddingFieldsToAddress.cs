﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgManager.Repository.Migrations
{
    public partial class AddingFieldsToAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Addresses",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Addresses");
        }
    }
}
