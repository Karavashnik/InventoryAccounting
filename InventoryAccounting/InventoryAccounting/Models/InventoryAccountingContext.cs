using System;
using InventoryAccounting.Models.DB;
using Microsoft.EntityFrameworkCore;

namespace InventoryAccounting.Models
{
    public partial class InventoryAccountingContext : DbContext
    {
        public InventoryAccountingContext()
        {
        }

        public InventoryAccountingContext(DbContextOptions<InventoryAccountingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acts> Acts { get; set; }
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Persons> Persons { get; set; }
        public virtual DbSet<Rooms> Rooms { get; set; }
        public virtual DbSet<Tmc> Tmc { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompilationDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.Acts)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_Acts_Contracts1");
            });

            modelBuilder.Entity<Companies>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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

                entity.Property(e => e.Unp).HasColumnName("UNP");
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CompilationDate).HasColumnType("date");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contracts_Companies");
            });

            modelBuilder.Entity<Persons>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

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
                entity.ToTable("TMC");

                entity.HasIndex(e => e.RoomId);

                entity.Property(e => e.Id).ValueGeneratedNever();

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
                    .HasConstraintName("FK_TMC_Acts1");

                entity.HasOne(d => d.ResponsiblePerson)
                    .WithMany(p => p.Tmc)
                    .HasForeignKey(d => d.ResponsiblePersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TMC_Persons");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Tmc)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TMC_Rooms");
            });
        }
    }
}
