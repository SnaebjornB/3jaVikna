using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class OrderHistoryAnotherFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderHistory_ReviewViewModelID",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ReviewViewModelID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ReviewViewModelID",
                table: "OrderItems");

            migrationBuilder.CreateTable(
                name: "OrderHistoryBookViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReviewViewModelID = table.Column<int>(nullable: true),
                    author = table.Column<string>(nullable: true),
                    bookID = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistoryBookViewModel", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderHistoryBookViewModel_OrderHistory_ReviewViewModelID",
                        column: x => x.ReviewViewModelID,
                        principalTable: "OrderHistory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryBookViewModel_ReviewViewModelID",
                table: "OrderHistoryBookViewModel",
                column: "ReviewViewModelID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderHistoryBookViewModel");

            migrationBuilder.AddColumn<int>(
                name: "ReviewViewModelID",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ReviewViewModelID",
                table: "OrderItems",
                column: "ReviewViewModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderHistory_ReviewViewModelID",
                table: "OrderItems",
                column: "ReviewViewModelID",
                principalTable: "OrderHistory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
