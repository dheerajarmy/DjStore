using DjStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace DjStore.Data
{
    public class DjStoreContext : DbContext
    {
        public DjStoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}