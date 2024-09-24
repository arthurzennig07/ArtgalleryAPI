using System.Collections.Generic;
using System.Threading.Tasks;
using ArtGalleryAPI.Models;

namespace ArtGalleryAPI.Services
{
    public interface IArtworkService
    {
        Task<IEnumerable<Artwork>> GetAllArtworksAsync();
        Task<Artwork> GetArtworkByIdAsync(int id);
        Task AddArtworkAsync(Artwork artwork);
        Task UpdateArtworkAsync(Artwork artwork);
        Task DeleteArtworkAsync(int id);
        Task AddStockAsync(int id, StockChangeRequest request);
        Task RemoveStockAsync(int id, StockChangeRequest request);
        Task<bool> ArtworkExistsAsync(int id);
    }
}