using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class OrderHistoryAnotherDamnFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistoryBookViewModel_OrderHistory_ReviewViewModelID",
                table: "OrderHistoryBookViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistoryBookViewModel",
                table: "OrderHistoryBookViewModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistoryBookViewModel_ReviewViewModelID",
                table: "OrderHistoryBookViewModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistory",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "ReviewViewModelID",
                table: "OrderHistoryBookViewModel");

            migrationBuilder.RenameTable(
                name: "OrderHistoryBookViewModel",
                newName: "OrderHistoryBooks");

            migrationBuilder.RenameTable(
                name: "OrderHistory",
                newName: "CurrentOrder");

            migrationBuilder.AddColumn<string>(
                name: "orderHistoryID",
                table: "OrderHistoryBooks",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistoryBooks",
                table: "OrderHistoryBooks",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CurrentOrder",
                table: "CurrentOrder",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "CurrentOrderBooks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bookAuthor = table.Column<string>(nullable: true),
                    bookID = table.Column<int>(nullable: false),
                    bookName = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    quantity = table.Column<int>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentOrderBooks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    address = table.Column<string>(nullable: true),
                    timeStamp = table.Column<string>(nullable: true),
                    totalPrice = table.Column<double>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentOrderBooks");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderHistoryBooks",
                table: "OrderHistoryBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CurrentOrder",
                table: "CurrentOrder");

            migrationBuilder.DropColumn(
                name: "orderHistoryID",
                table: "OrderHistoryBooks");

            migrationBuilder.RenameTable(
                name: "OrderHistoryBooks",
                newName: "OrderHistoryBookViewModel");

            migrationBuilder.RenameTable(
                name: "CurrentOrder",
                newName: "OrderHistory");

            migrationBuilder.AddColumn<int>(
                name: "ReviewViewModelID",
                table: "OrderHistoryBookViewModel",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistoryBookViewModel",
                table: "OrderHistoryBookViewModel",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderHistory",
                table: "OrderHistory",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistoryBookViewModel_ReviewViewModelID",
                table: "OrderHistoryBookViewModel",
                column: "ReviewViewModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistoryBookViewModel_OrderHistory_ReviewViewModelID",
                table: "OrderHistoryBookViewModel",
                column: "ReviewViewModelID",
                principalTable: "OrderHistory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
