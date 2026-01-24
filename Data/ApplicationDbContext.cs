using Microsoft.EntityFrameworkCore;
using MediaCatalogApp.Models;

namespace MediaCatalogApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<MediaItem> MediaItems { get; set; }
    }
}