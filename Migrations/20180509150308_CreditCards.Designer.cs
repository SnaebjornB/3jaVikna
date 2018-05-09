﻿// <auto-generated />
using BookCave.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BookCave.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180509150308_CreditCards")]
    partial class CreditCards
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BookCave.Models.EntityModels.AddressEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<string>("country");

                    b.Property<string>("houseNumber");

                    b.Property<string>("streetName");

                    b.Property<string>("userID");

                    b.Property<int>("zip");

                    b.HasKey("ID");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.BookEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ISBN");

                    b.Property<string>("author");

                    b.Property<string>("category");

                    b.Property<string>("country");

                    b.Property<string>("description");

                    b.Property<double>("discount");

                    b.Property<string>("image");

                    b.Property<string>("language");

                    b.Property<int>("noOfCopiesAvailable");

                    b.Property<int>("noOfRatings");

                    b.Property<int>("noOfSoldUnits");

                    b.Property<int>("numberOfPages");

                    b.Property<double>("price");

                    b.Property<string>("publisher");

                    b.Property<double>("rating");

                    b.Property<string>("title");

                    b.Property<int>("year");

                    b.HasKey("ID");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.CCardEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("month");

                    b.Property<string>("number");

                    b.Property<string>("userID");

                    b.Property<int>("year");

                    b.HasKey("ID");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.OrderItemEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("bookAuthor");

                    b.Property<int>("bookID");

                    b.Property<string>("bookName");

                    b.Property<string>("customerID");

                    b.Property<double>("price");

                    b.Property<int>("quantity");

                    b.HasKey("ID");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BookCave.Models.EntityModels.ReviewEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookID");

                    b.Property<double>("rating");

                    b.Property<string>("review");

                    b.Property<string>("userID");

                    b.Property<string>("username");

                    b.HasKey("ID");

                    b.ToTable("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
