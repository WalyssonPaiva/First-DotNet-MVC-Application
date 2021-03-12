using Microsoft.EntityFrameworkCore;
using MVCStore.Domain.Entities;

namespace MVCStore.Infra.Data.Context {
    public class PgDbContext : DbContext {
        
        public PgDbContext(DbContextOptions options) : base(options) {
            
        }
        public DbSet<Product> Products { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PgDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}