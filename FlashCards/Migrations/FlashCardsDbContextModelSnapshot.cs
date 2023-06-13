﻿// <auto-generated />
using FlashCards.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FlashCards.Migrations
{
    [DbContext(typeof(FlashCardsDbContext))]
    partial class FlashCardsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FlashCards.Models.Card", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BackSide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("CardListId")
                        .HasColumnType("bigint");

                    b.Property<string>("FrontSide")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SpacedRepetitionId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CardListId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("FlashCards.Models.CardList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CardLists");
                });

            modelBuilder.Entity("FlashCards.Models.Card", b =>
                {
                    b.HasOne("FlashCards.Models.CardList", null)
                        .WithMany("Cards")
                        .HasForeignKey("CardListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FlashCards.Models.CardList", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
