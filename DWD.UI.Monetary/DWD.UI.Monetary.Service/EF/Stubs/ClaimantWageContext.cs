#nullable disable

namespace DWD.UI.Monetary.Service.EF.Stubs
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public partial class ClaimantWageContext : DbContext
    {
        public ClaimantWageContext()
        {
        }

        public ClaimantWageContext(DbContextOptions<ClaimantWageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClaimantWage> ClaimantWages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

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
}
