using Core.Entities;
using DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TelegramDbContext : DbContext
    {
        public DbSet<TelegramUser> TelegramUser { get; set; }
        public DbSet<VpnClient> VpnClient { get; set; }
        public DbSet<UserPayments> UserPayments { get; set; }
        public TelegramDbContext(DbContextOptions<TelegramDbContext> options) : base(options){}
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserPaymentsConfiguration());
            modelBuilder.ApplyConfiguration(new TelegramUserConfiguration());
            modelBuilder.ApplyConfiguration(new VpnClientConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
