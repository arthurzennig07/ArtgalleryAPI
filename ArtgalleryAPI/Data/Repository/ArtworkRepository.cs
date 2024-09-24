using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ArtGalleryAPI.Data;
using ArtgalleryAPI.Data.Repository;
using ArtGalleryAPI.Models;

namespace ArtGalleryAPI.Repositories
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly ArtGalleryContext _context;

        public ArtworkRepository(ArtGalleryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artwork>> GetAllAsync()
        {
            return await _context.Artworks.ToListAsync();
        }

        public async Task<Artwork> GetByIdAsync(int id)
        {
            return await _context.Artworks.FindAsync(id);
        }

        public async Task AddAsync(Artwork artwork)
        {
            await _context.Artworks.AddAsync(artwork);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Artwork artwork)
        {
            _context.Entry(artwork).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var artwork = await _context.Artworks.FindAsync(id);
            if (artwork != null)
            {
                _context.Artworks.Remove(artwork);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ArtworkExistsAsync(int id)
        {
            return await _context.Artworks.AnyAsync(e => e.Id == id);
        }
    }
}