using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class ReviewsList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookEntityID",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookEntityID",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BookEntityID",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookEntityID",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookEntityID",
                table: "Reviews",
                column: "BookEntityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookEntityID",
                table: "Reviews",
                column: "BookEntityID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
