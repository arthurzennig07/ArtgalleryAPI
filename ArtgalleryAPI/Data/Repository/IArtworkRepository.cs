using ArtGalleryAPI.Models;

namespace ArtgalleryAPI.Data.Repository
{
    public interface IArtworkRepository
    {
        Task<IEnumerable<Artwork>> GetAllAsync();
        Task<Artwork> GetByIdAsync(int id);
        Task AddAsync(Artwork artwork);
        Task UpdateAsync(Artwork artwork);
        Task DeleteAsync(int id);
        Task<bool> ArtworkExistsAsync(int id);
    }
}