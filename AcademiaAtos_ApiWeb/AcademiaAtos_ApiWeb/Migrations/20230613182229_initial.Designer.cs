﻿// <auto-generated />
using AcademiaAtos_ApiWeb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcademiaAtos_ApiWeb.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20230613182229_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcademiaAtos_ApiWeb.DataModels.Email", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pessoaid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("pessoaid");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("AcademiaAtos_ApiWeb.DataModels.Pessoa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("AcademiaAtos_ApiWeb.DataModels.Email", b =>
                {
                    b.HasOne("AcademiaAtos_ApiWeb.DataModels.Pessoa", "pessoa")
                        .WithMany("emails")
                        .HasForeignKey("pessoaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("pessoa");
                });

            modelBuilder.Entity("AcademiaAtos_ApiWeb.DataModels.Pessoa", b =>
                {
                    b.Navigation("emails");
                });
#pragma warning restore 612, 618
        }
    }
}
