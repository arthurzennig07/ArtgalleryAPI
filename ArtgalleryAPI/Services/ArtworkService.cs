using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArtgalleryAPI.Data.Repository;
using ArtGalleryAPI.Models;
using ArtGalleryAPI.Repositories;

namespace ArtGalleryAPI.Services
{
    public class ArtworkService : IArtworkService
    {
        private readonly IArtworkRepository _artworkRepository;

        public ArtworkService(IArtworkRepository artworkRepository)
        {
            _artworkRepository = artworkRepository;
        }

        public async Task<IEnumerable<Artwork>> GetAllArtworksAsync()
        {
            return await _artworkRepository.GetAllAsync();
        }

        public async Task<Artwork> GetArtworkByIdAsync(int id)
        {
            return await _artworkRepository.GetByIdAsync(id);
        }

        public async Task AddArtworkAsync(Artwork artwork)
        {
            await _artworkRepository.AddAsync(artwork);
        }

        public async Task UpdateArtworkAsync(Artwork artwork)
        {
            await _artworkRepository.UpdateAsync(artwork);
        }

        public async Task DeleteArtworkAsync(int id)
        {
            await _artworkRepository.DeleteAsync(id);
        }

        public async Task AddStockAsync(int id, StockChangeRequest request)
        {
            var artwork = await _artworkRepository.GetByIdAsync(id);
            if (artwork == null)
            {
                throw new KeyNotFoundException("Artwork not found.");
            }

            switch (request.StockType.ToLower())
            {
                case "artwork":
                    artwork.Stock += request.Quantity;
                    break;
                case "poster":
                    artwork.PosterStockQty += request.Quantity;
                    break;
                default:
                    throw new ArgumentException("Invalid stock type. Must be 'artwork' or 'poster'.");
            }

            await _artworkRepository.UpdateAsync(artwork);
        }

        public async Task RemoveStockAsync(int id, StockChangeRequest request)
        {
            var artwork = await _artworkRepository.GetByIdAsync(id);
            if (artwork == null)
            {
                throw new KeyNotFoundException("Artwork not found.");
            }

            switch (request.StockType.ToLower())
            {
                case "artwork":
                    if (artwork.Stock < request.Quantity)
                    {
                        throw new InvalidOperationException("Insufficient artwork stock.");
                    }
                    artwork.Stock -= request.Quantity;
                    break;
                case "poster":
                    if (artwork.PosterStockQty < request.Quantity)
                    {
                        throw new InvalidOperationException("Insufficient poster stock.");
                    }
                    artwork.PosterStockQty -= request.Quantity;
                    break;
                default:
                    throw new ArgumentException("Invalid stock type. Must be 'artwork' or 'poster'.");
            }

            await _artworkRepository.UpdateAsync(artwork);
        }

        public async Task<bool> ArtworkExistsAsync(int id)
        {
            return await _artworkRepository.ArtworkExistsAsync(id);
        }
    }
}