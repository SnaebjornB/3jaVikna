using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class addingListOfReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewEntity_Books_BookEntityID",
                table: "ReviewEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity");

            migrationBuilder.RenameTable(
                name: "ReviewEntity",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewEntity_BookEntityID",
                table: "Reviews",
                newName: "IX_Reviews_BookEntityID");

            migrationBuilder.AddColumn<int>(
                name: "BookID",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookEntityID",
                table: "Reviews",
                column: "BookEntityID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookEntityID",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BookID",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "ReviewEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_BookEntityID",
                table: "ReviewEntity",
                newName: "IX_ReviewEntity_BookEntityID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewEntity",
                table: "ReviewEntity",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewEntity_Books_BookEntityID",
                table: "ReviewEntity",
                column: "BookEntityID",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
