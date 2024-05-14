﻿// <auto-generated />
using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(UniversityDbContext))]
    [Migration("20240510172941_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AppCore.Models.country", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("country_name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("id");

                    b.ToTable("country");
                });

            modelBuilder.Entity("AppCore.Models.ranking_criteria", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("criteria_name")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<int?>("ranking_system_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("ranking_system_id");

                    b.ToTable("ranking_criteria");
                });

            modelBuilder.Entity("AppCore.Models.ranking_system", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<string>("system_name")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("id");

                    b.ToTable("ranking_system");
                });

            modelBuilder.Entity("AppCore.Models.university", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("int");

                    b.Property<int?>("country_id")
                        .HasColumnType("int");

                    b.Property<string>("university_name")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("id");

                    b.HasIndex("country_id");

                    b.ToTable("university");
                });

            modelBuilder.Entity("AppCore.Models.university_ranking_year", b =>
                {
                    b.Property<int?>("ranking_criteria_id")
                        .HasColumnType("int");

                    b.Property<int?>("score")
                        .HasColumnType("int");

                    b.Property<int?>("university_id")
                        .HasColumnType("int");

                    b.Property<int?>("year")
                        .HasColumnType("int");

                    b.HasIndex("ranking_criteria_id");

                    b.HasIndex("university_id");

                    b.ToTable("university_ranking_year");
                });

            modelBuilder.Entity("AppCore.Models.university_year", b =>
                {
                    b.Property<int?>("num_students")
                        .HasColumnType("int");

                    b.Property<int?>("pct_female_students")
                        .HasColumnType("int");

                    b.Property<int?>("pct_international_students")
                        .HasColumnType("int");

                    b.Property<decimal?>("student_staff_ratio")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int?>("university_id")
                        .HasColumnType("int");

                    b.Property<int?>("year")
                        .HasColumnType("int");

                    b.HasIndex("university_id");

                    b.ToTable("university_year");
                });

            modelBuilder.Entity("AppCore.Models.ranking_criteria", b =>
                {
                    b.HasOne("AppCore.Models.ranking_system", "ranking_system")
                        .WithMany("ranking_criteria")
                        .HasForeignKey("ranking_system_id")
                        .HasConstraintName("fk_rc_rs");

                    b.Navigation("ranking_system");
                });

            modelBuilder.Entity("AppCore.Models.university", b =>
                {
                    b.HasOne("AppCore.Models.country", "country")
                        .WithMany("university")
                        .HasForeignKey("country_id")
                        .HasConstraintName("fk_uni_cnt");

                    b.Navigation("country");
                });

            modelBuilder.Entity("AppCore.Models.university_ranking_year", b =>
                {
                    b.HasOne("AppCore.Models.ranking_criteria", "ranking_criteria")
                        .WithMany()
                        .HasForeignKey("ranking_criteria_id")
                        .HasConstraintName("fk_ury_rc");

                    b.HasOne("AppCore.Models.university", "university")
                        .WithMany()
                        .HasForeignKey("university_id")
                        .HasConstraintName("fk_ury_uni");

                    b.Navigation("ranking_criteria");

                    b.Navigation("university");
                });

            modelBuilder.Entity("AppCore.Models.university_year", b =>
                {
                    b.HasOne("AppCore.Models.university", "university")
                        .WithMany()
                        .HasForeignKey("university_id")
                        .HasConstraintName("fk_uy_uni");

                    b.Navigation("university");
                });

            modelBuilder.Entity("AppCore.Models.country", b =>
                {
                    b.Navigation("university");
                });

            modelBuilder.Entity("AppCore.Models.ranking_system", b =>
                {
                    b.Navigation("ranking_criteria");
                });
#pragma warning restore 612, 618
        }
    }
}
