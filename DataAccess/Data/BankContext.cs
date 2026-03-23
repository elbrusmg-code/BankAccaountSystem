using DataAccess.Models;
using DataAccess.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ═══════════════════════════════
        //  CUSTOMER
        // ═══════════════════════════════
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)");

            entity.Property(c => c.NationalId)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            entity.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(c => c.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            entity.Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnType("datetime2");

            entity.Property(c => c.IsDeleted)
                .HasDefaultValue(false);

            // Unique indexes
            entity.HasIndex(c => c.NationalId).IsUnique();
            entity.HasIndex(c => c.Email).IsUnique();
        });

        // ═══════════════════════════════
        //  ACCOUNT
        // ═══════════════════════════════
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.AccountNumber)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(a => a.AccountType)
                .IsRequired()
                .HasConversion<string>()  // enum → "Checking"/"Savings"
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(a => a.Balance)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("decimal(18,2)");

            entity.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()  // enum → "Active"/"Closed"
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(a => a.OpenedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnType("datetime2");

            entity.Property(a => a.ClosedAt)
                .IsRequired(false)
                .HasColumnType("datetime2");

            // Unique index
            entity.HasIndex(a => a.AccountNumber).IsUnique();

            // Account → Customer (many-to-one)
            entity.HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ═══════════════════════════════
        //  TRANSACTION
        // ═══════════════════════════════
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Type)
                .IsRequired()
                .HasConversion<string>()  // enum → "Deposit"/"Withdrawal"/"Transfer"
                .HasMaxLength(20)
                .HasColumnType("nvarchar(20)");

            entity.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.BalanceAfter)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            entity.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(255)
                .HasColumnType("nvarchar(255)");

            entity.Property(t => t.OccurredAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnType("datetime2");

            entity.Property(t => t.RelatedAccountId)
                .IsRequired(false);

            // Transaction → Account (main)
            entity.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Transaction → Account (related - transfer üçün)
            entity.HasOne(t => t.RelatedAccount)
                .WithMany()
                .HasForeignKey(t => t.RelatedAccountId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        });
    }
}