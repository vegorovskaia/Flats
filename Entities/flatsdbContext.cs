using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public partial class flatsdbContext : DbContext
    {
        public flatsdbContext()
        {
        }

        public flatsdbContext(DbContextOptions<flatsdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartments> Apartments { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }
        public virtual DbSet<Houses> Houses { get; set; }
        public virtual DbSet<Regions> Regions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartments>(entity =>
            {
                entity.HasKey(e => e.ApartmentId);

                entity.Property(e => e.ApartmentId).ValueGeneratedNever();

                entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Sall)
                    .HasColumnName("SAll")
                    .HasColumnType("decimal(8, 2)");

                entity.HasOne(d => d.House)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.HouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Apartments_Houses");
            });

            modelBuilder.Entity<Districts>(entity =>
            {
                entity.HasKey(e => e.DistrictId);

                entity.Property(e => e.DistrictId).ValueGeneratedNever();

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Districts_Regions");
            });

            modelBuilder.Entity<Houses>(entity =>
            {
                entity.HasKey(e => e.HouseId);

                entity.Property(e => e.HouseId).ValueGeneratedNever();

                entity.Property(e => e.ComplexName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.HouseNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Houses_Districts");
            });

            modelBuilder.Entity<Regions>(entity =>
            {
                entity.HasKey(e => e.RegionId);

                entity.Property(e => e.RegionId).ValueGeneratedNever();

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
