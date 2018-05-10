using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class OrderHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReviewViewModelID",
                table: "OrderItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AddressViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    houseNumber = table.Column<string>(nullable: true),
                    streetName = table.Column<string>(nullable: true),
                    zip = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressViewModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CCardInfoViewModel",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    month = table.Column<int>(nullable: false),
                    number = table.Column<string>(nullable: false),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CCardInfoViewModel", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderHistory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    addressID = table.Column<int>(nullable: true),
                    cardID = table.Column<int>(nullable: true),
                    paid = table.Column<bool>(nullable: false),
                    payPal = table.Column<bool>(nullable: false),
                    totalPrice = table.Column<double>(nullable: false),
                    userID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderHistory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderHistory_AddressViewModel_addressID",
                        column: x => x.addressID,
                        principalTable: "AddressViewModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderHistory_CCardInfoViewModel_cardID",
                        column: x => x.cardID,
                        principalTable: "CCardInfoViewModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ReviewViewModelID",
                table: "OrderItems",
                column: "ReviewViewModelID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_addressID",
                table: "OrderHistory",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_cardID",
                table: "OrderHistory",
                column: "cardID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_OrderHistory_ReviewViewModelID",
                table: "OrderItems",
                column: "ReviewViewModelID",
                principalTable: "OrderHistory",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_OrderHistory_ReviewViewModelID",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderHistory");

            migrationBuilder.DropTable(
                name: "AddressViewModel");

            migrationBuilder.DropTable(
                name: "CCardInfoViewModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ReviewViewModelID",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ReviewViewModelID",
                table: "OrderItems");
        }
    }
}
