using Betting.Domain;
using Microsoft.EntityFrameworkCore;

namespace Betting.Infrastructure.Persistence
{
    public partial class BettingDbContext : DbContext
    {
        public BettingDbContext()
        {
        }

        public BettingDbContext(DbContextOptions<BettingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = default!;
        public virtual DbSet<CasinoWager> CasinoWagers { get; set; } = default!;
        public virtual DbSet<Game> Games { get; set; } = default!;
        public virtual DbSet<Provider> Providers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CasinoWager>(entity =>
            {
                entity.ToTable("CasinoWager");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(30, 15)");

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.SessionData).IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.CasinoWagers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CasinoWager_Account");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.CasinoWagers)
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CasinoWager_Game");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Game");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Theme)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.Games)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Game_Provider");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.ToTable("Provider");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
