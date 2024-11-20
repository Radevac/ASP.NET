﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toyota_Pagination.Data;

#nullable disable

namespace Toyota_Pagination.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241120172029_CarsTable")]
    partial class CarsTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ColorModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RGB")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Colors");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ConfigurationColorsModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ColorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ConfigurationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MainImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ConfigurationId");

                    b.ToTable("ConfigurationColors");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ConfigurationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ModelId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ToyotaModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ToyotaModels");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ConfigurationColorsModel", b =>
                {
                    b.HasOne("WebApplicationGeo.Models.Cars.Toyota.ColorModel", "Color")
                        .WithMany("Configurations")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplicationGeo.Models.Cars.Toyota.ConfigurationModel", "Configuration")
                        .WithMany("Colors")
                        .HasForeignKey("ConfigurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Color");

                    b.Navigation("Configuration");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ConfigurationModel", b =>
                {
                    b.HasOne("WebApplicationGeo.Models.Cars.Toyota.ToyotaModel", "Model")
                        .WithMany("Configurations")
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ColorModel", b =>
                {
                    b.Navigation("Configurations");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ConfigurationModel", b =>
                {
                    b.Navigation("Colors");
                });

            modelBuilder.Entity("WebApplicationGeo.Models.Cars.Toyota.ToyotaModel", b =>
                {
                    b.Navigation("Configurations");
                });
#pragma warning restore 612, 618
        }
    }
}