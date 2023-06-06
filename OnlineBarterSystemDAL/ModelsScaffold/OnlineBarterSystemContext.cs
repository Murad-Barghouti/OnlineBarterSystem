/*using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineBarterSystemDAL.ModelsScaffold
{
    public partial class OnlineBarterSystemContext : DbContext
    {
        public OnlineBarterSystemContext()
        {
        }

        public OnlineBarterSystemContext(DbContextOptions<OnlineBarterSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Barter> Barters { get; set; } = null!;
        public virtual DbSet<BarterState> BarterStates { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<City> Cities { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=OnlineBarterSystem;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Barter>(entity =>
            {
                entity.ToTable("Barter");

                entity.Property(e => e.CreationDate)
                    .HasPrecision(0)
                    .HasDefaultValueSql("(sysdatetimeoffset())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.BarterState)
                    .WithMany(p => p.Barters)
                    .HasForeignKey(d => d.BarterStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barter_BarterStateId_BarterState_Id");

                entity.HasOne(d => d.GiveType)
                    .WithMany(p => p.BarterGiveTypes)
                    .HasForeignKey(d => d.GiveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barter_GiveTypeId_SubCategory_Id");

                entity.HasOne(d => d.Initiator)
                    .WithMany(p => p.BarterInitiators)
                    .HasForeignKey(d => d.InitiatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barter_InitiatorId_User_Id");

                entity.HasOne(d => d.Joiner)
                    .WithMany(p => p.BarterJoiners)
                    .HasForeignKey(d => d.JoinerId)
                    .HasConstraintName("FK_Barter_JoinerId_User_Id");

                entity.HasOne(d => d.ReceiveType)
                    .WithMany(p => p.BarterReceiveTypes)
                    .HasForeignKey(d => d.ReceiveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barter_ReceiveTypeId_SubCategory_Id");
            });

            modelBuilder.Entity<BarterState>(entity =>
            {
                entity.ToTable("BarterState");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategory_CategoryId_Category_Id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_CityId_City_Id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
*/