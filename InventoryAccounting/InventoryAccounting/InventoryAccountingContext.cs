using System;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Protocols;

namespace InventoryAccounting
{
    public partial class InventoryAccountingContext : DbContext
    {
        public InventoryAccountingContext(DbContextOptions<InventoryAccountingContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Acts> Acts { get; set; }
        public virtual DbSet<CompanyName> CompanyName { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<ResponsiblePersons> ResponsiblePersons { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Tmc> Tmc { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompilationDate).HasColumnType("date");

                entity.HasOne(d => d.ContractNumberNavigation)
                    .WithMany(p => p.Acts)
                    .HasForeignKey(d => d.ContractNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acts_Contracts");
            });

            modelBuilder.Entity<CompanyName>(entity =>
            {
                entity.HasKey(e => e.Unp);

                entity.Property(e => e.Unp)
                    .HasColumnName("UNP")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.DirectorsName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DirectorsPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.ContractNumber);

                entity.Property(e => e.ContractNumber).ValueGeneratedNever();

                entity.Property(e => e.CompanyUnp).HasColumnName("CompanyUNP");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.CompanyUnpNavigation)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CompanyUnp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contracts_CompanyName");
            });

            modelBuilder.Entity<ResponsiblePersons>(entity =>
            {
                entity.HasKey(e => e.PersonnelNumber);

                entity.Property(e => e.PersonnelNumber).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DateOfEmployment).HasColumnType("date");

                entity.Property(e => e.Education)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PassportDetails)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Post)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rooms>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tmc>(entity =>
            {
                entity.HasKey(e => e.InventoryNumber);

                entity.ToTable("TMC");

                entity.Property(e => e.InventoryNumber).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WarrantyDate).HasColumnType("date");

                entity.Property(e => e.WriteOffDate).HasColumnType("date");

                entity.HasOne(d => d.Act)
                    .WithMany(p => p.Tmc)
                    .HasForeignKey(d => d.ActId)
                    .HasConstraintName("FK_TMC_Acts");

                entity.HasOne(d => d.PesponsiblePersonNumberNavigation)
                    .WithMany(p => p.Tmc)
                    .HasForeignKey(d => d.PesponsiblePersonNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryName_ResponsiblePersons");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Tmc)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TMC_Rooms");
            });
        }
    }
}
