using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BookCave.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ISBN = table.Column<int>(nullable: false),
                    author = table.Column<string>(nullable: true),
                    category = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    discount = table.Column<double>(nullable: false),
                    language = table.Column<string>(nullable: true),
                    noOfCopiesAvailable = table.Column<int>(nullable: false),
                    noOfSoldUnits = table.Column<int>(nullable: false),
                    numberOfPages = table.Column<int>(nullable: false),
                    price = table.Column<double>(nullable: false),
                    publisher = table.Column<string>(nullable: true),
                    rating = table.Column<double>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
