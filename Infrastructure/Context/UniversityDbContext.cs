#nullable disable
using System;
using System.Collections.Generic;
using AppCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context;

public partial class UniversityDbContext : DbContext
{
    //public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
    //    : base(options)
    //{
    //}

    public virtual DbSet<country> country { get; set; }

    public virtual DbSet<ranking_criteria> ranking_criteria { get; set; }

    public virtual DbSet<ranking_system> ranking_system { get; set; }

    public virtual DbSet<university> university { get; set; }

    public virtual DbSet<university_ranking_year> university_ranking_year { get; set; }

    public virtual DbSet<university_year> university_year { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string PC = Environment.MachineName;
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(
            $"DATA SOURCE={PC};DATABASE=universities;Integrated Security=true;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<country>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<ranking_criteria>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.ranking_system).WithMany(p => p.ranking_criteria).HasConstraintName("fk_rc_rs");
        });

        modelBuilder.Entity<ranking_system>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<university>(entity =>
        {
            entity.Property(e => e.id).ValueGeneratedNever();

            entity.HasOne(d => d.country).WithMany(p => p.university).HasConstraintName("fk_uni_cnt");
        });

        modelBuilder.Entity<university_ranking_year>(entity =>
        {
            entity.HasOne(d => d.ranking_criteria).WithMany().HasConstraintName("fk_ury_rc");

            entity.HasOne(d => d.university).WithMany().HasConstraintName("fk_ury_uni");
        });

        modelBuilder.Entity<university_year>(entity =>
        {
            entity.HasOne(d => d.university).WithMany().HasConstraintName("fk_uy_uni");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}