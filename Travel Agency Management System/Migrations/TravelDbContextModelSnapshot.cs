﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Travel_Agency_Management_System.Models;

#nullable disable

namespace Travel_Agency_Management_System.Migrations
{
    [DbContext(typeof(TravelDbContext))]
    partial class TravelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Travel_Agency_Management_System.Models.BookingEntry", b =>
                {
                    b.Property<int>("BookingEntryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingEntryId"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("SpotId")
                        .HasColumnType("int");

                    b.HasKey("BookingEntryId");

                    b.HasIndex("ClientId");

                    b.HasIndex("SpotId");

                    b.ToTable("BookingEntries");
                });

            modelBuilder.Entity("Travel_Agency_Management_System.Models.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClientId"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsMarried")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClientId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Travel_Agency_Management_System.Models.Spot", b =>
                {
                    b.Property<int>("SpotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SpotId"));

                    b.Property<string>("SpotName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpotId");

                    b.ToTable("Spots");
                });

            modelBuilder.Entity("Travel_Agency_Management_System.Models.BookingEntry", b =>
                {
                    b.HasOne("Travel_Agency_Management_System.Models.Client", "Client")
                        .WithMany("BookingEntries")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Travel_Agency_Management_System.Models.Spot", "Spot")
                        .WithMany("BookingEntries")
                        .HasForeignKey("SpotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Spot");
                });

            modelBuilder.Entity("Travel_Agency_Management_System.Models.Client", b =>
                {
                    b.Navigation("BookingEntries");
                });

            modelBuilder.Entity("Travel_Agency_Management_System.Models.Spot", b =>
                {
                    b.Navigation("BookingEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
