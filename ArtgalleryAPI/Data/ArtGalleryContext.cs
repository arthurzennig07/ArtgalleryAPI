using Microsoft.EntityFrameworkCore;
using ArtGalleryAPI.Models;

namespace ArtGalleryAPI.Data
{
    public class ArtGalleryContext : DbContext
    {
        public ArtGalleryContext(DbContextOptions<ArtGalleryContext> options)
            : base(options)
        {
        }

        public DbSet<Artwork> Artworks { get; set; }
    }
}