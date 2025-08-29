﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WFConfin.Migrations
{
    /// <inheritdoc />
    public partial class StatusEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Estado",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Estado");
        }
    }
}
