﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(DemoBackendDbContext))]
    [Migration("20230911115702_Fix_Enoikiaseis_Table_Entries_Table")]
    partial class FixEnoikiaseisTableEntriesTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Models.Entities.Enoikiasi", b =>
                {
                    b.Property<int>("IDKasetas")
                        .HasColumnType("int");

                    b.Property<int>("IDPelati")
                        .HasColumnType("int");

                    b.Property<DateTime>("Apo")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Eos")
                        .HasColumnType("datetime2");

                    b.HasKey("IDKasetas", "IDPelati", "Apo");

                    b.HasIndex("IDPelati");

                    b.ToTable("Enoikiasi", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Kaseta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IDTainias")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Posotita")
                        .HasColumnType("int");

                    b.Property<decimal>("Timi")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tipos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IDTainias");

                    b.ToTable("Kasetes", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Pelatis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Onoma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tilefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pelatis", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Models.Entities.Sintelestis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Onoma")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Sintelestis", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Tainia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Titlos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Xronia")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Tainia", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Tn_sn", b =>
                {
                    b.Property<int>("IDTainias")
                        .HasColumnType("int");

                    b.Property<int>("IDSintelesti")
                        .HasColumnType("int");

                    b.Property<string>("Rolos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDTainias", "IDSintelesti");

                    b.HasIndex("IDSintelesti");

                    b.ToTable("Tn_sn", (string)null);
                });

            modelBuilder.Entity("Models.Entities.Enoikiasi", b =>
                {
                    b.HasOne("Models.Entities.Kaseta", "Kaseta")
                        .WithMany("Enoikiasis")
                        .HasForeignKey("IDKasetas")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.Pelatis", "Pelatis")
                        .WithMany("Enoikiasis")
                        .HasForeignKey("IDPelati")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kaseta");

                    b.Navigation("Pelatis");
                });

            modelBuilder.Entity("Models.Entities.Kaseta", b =>
                {
                    b.HasOne("Models.Entities.Tainia", "Tainia")
                        .WithMany("Kasetes")
                        .HasForeignKey("IDTainias")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tainia");
                });

            modelBuilder.Entity("Models.Entities.Tn_sn", b =>
                {
                    b.HasOne("Models.Entities.Sintelestis", "Sintelestis")
                        .WithMany("TainiesSintelestes")
                        .HasForeignKey("IDSintelesti")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.Entities.Tainia", "Tainia")
                        .WithMany("TainiesSintelestes")
                        .HasForeignKey("IDTainias")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sintelestis");

                    b.Navigation("Tainia");
                });

            modelBuilder.Entity("Models.Entities.Kaseta", b =>
                {
                    b.Navigation("Enoikiasis");
                });

            modelBuilder.Entity("Models.Entities.Pelatis", b =>
                {
                    b.Navigation("Enoikiasis");
                });

            modelBuilder.Entity("Models.Entities.Sintelestis", b =>
                {
                    b.Navigation("TainiesSintelestes");
                });

            modelBuilder.Entity("Models.Entities.Tainia", b =>
                {
                    b.Navigation("Kasetes");

                    b.Navigation("TainiesSintelestes");
                });
#pragma warning restore 612, 618
        }
    }
}