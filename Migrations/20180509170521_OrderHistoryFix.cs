using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class OrderHistoryFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_AddressViewModel_addressID",
                table: "OrderHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderHistory_CCardInfoViewModel_cardID",
                table: "OrderHistory");

            migrationBuilder.DropTable(
                name: "AddressViewModel");

            migrationBuilder.DropTable(
                name: "CCardInfoViewModel");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_addressID",
                table: "OrderHistory");

            migrationBuilder.DropIndex(
                name: "IX_OrderHistory_cardID",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "addressID",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "cardID",
                table: "OrderHistory");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "card",
                table: "OrderHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "OrderHistory");

            migrationBuilder.DropColumn(
                name: "card",
                table: "OrderHistory");

            migrationBuilder.AddColumn<int>(
                name: "addressID",
                table: "OrderHistory",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "cardID",
                table: "OrderHistory",
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

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_addressID",
                table: "OrderHistory",
                column: "addressID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderHistory_cardID",
                table: "OrderHistory",
                column: "cardID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_AddressViewModel_addressID",
                table: "OrderHistory",
                column: "addressID",
                principalTable: "AddressViewModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHistory_CCardInfoViewModel_cardID",
                table: "OrderHistory",
                column: "cardID",
                principalTable: "CCardInfoViewModel",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
