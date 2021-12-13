#nullable disable

namespace DWD.UI.Monetary.Service.Frameworks;

using System;
using Microsoft.EntityFrameworkCore;
using DWD.UI.Monetary.Service.Models.Stubs;

/// <summary>
/// Database context for ClaimantWage
/// </summary>
public partial class ClaimantWageContext : DbContext
{
    /// <summary>
    /// Instantiates a new ClaimantWageContext
    /// </summary>
    public ClaimantWageContext()
    {
    }

    /// <summary>
    /// Instantiates a new instance of the ClaimantWageContext
    /// </summary>
    /// <param name="options">Options for configuring the context</param>
    public ClaimantWageContext(DbContextOptions<ClaimantWageContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// ClaimantWages DB set
    /// </summary>
    public virtual DbSet<ClaimantWage> ClaimantWages { get; set; }

    /// <summary>
    /// Handler for the OnConfiguring event
    /// </summary>
    /// <param name="optionsBuilder">Object used to configure the context</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    /// <summary>
    /// Handler for the OnModelCreating event
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (null == modelBuilder)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        _ = modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF8");

        modelBuilder.Entity<ClaimantWage>(entity =>
        {
            entity.ToTable("claimant_wages");

            entity.HasIndex(e => new { e.ClaimantId, e.WageYear, e.WageQuarter }, "claimant_wages_idx")
                .IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.ClaimantId)
                .HasMaxLength(20)
                .HasColumnName("claimant_id");

            entity.Property(e => e.TotalWages)
                .HasColumnType("money")
                .HasColumnName("total_wages");

            entity.Property(e => e.WageQuarter).HasColumnName("wage_quarter");

            entity.Property(e => e.WageYear).HasColumnName("wage_year");
        });

        this.OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
