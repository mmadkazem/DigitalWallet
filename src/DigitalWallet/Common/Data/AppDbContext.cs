namespace DigitalWallet.Common.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Account
    public DbSet<User> Users { get; set; }

    // Wallet
    public DbSet<UserWallet> UserWallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserWallet>()
            .HasOne(u => u.User)
            .WithOne(w => w.UserWallet)
            .HasForeignKey<User>(u => u.UserWalletId);

        var converter = new ValueConverter<decimal, double>
        (
            v => (double)v,
            v => (decimal)v
        );

        modelBuilder.Entity<UserWallet>()
            .Property(w => w.Balance)
            .HasConversion(converter);

        modelBuilder.Entity<UserWallet>()
            .Property(w => w.BlockBalance)
            .HasConversion(converter);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.WalletReceipt);
            // .WithMany()
            // .HasForeignKey(t => t.WalletReceiptId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.WalletSender);
            // .WithMany()
            // .HasForeignKey(t => t.WalletSenderId);

        // modelBuilder.Entity<User>().Property(u => u.UserWalletId).IsRequired(false);
        // modelBuilder.Entity<UserWallet>().Property(u => u.UserId).IsRequired(false);

        base.OnModelCreating(modelBuilder);
    }
}