using Xunit;
using Moq;
using ArtGalleryAPI.Services;
using ArtGalleryAPI.Repositories;
using ArtGalleryAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ArtgalleryAPI.Data.Repository;

namespace ArtGalleryAPI.Tests
{
    public class ArtworkServiceTests
    {
        private readonly Mock<IArtworkRepository> _mockRepo;
        private readonly ArtworkService _service;

        public ArtworkServiceTests()
        {
            _mockRepo = new Mock<IArtworkRepository>();
            _service = new ArtworkService(_mockRepo.Object);
        }

        [Fact]
        public async Task AddStockAsync_WithValidPosterStock_IncreasesPosterStockQty()
        {
            // Arrange
            var artworkId = 1;
            var artwork = new Artwork { Id = artworkId, PosterStockQty = 10 };
            _mockRepo.Setup(repo => repo.GetByIdAsync(artworkId)).ReturnsAsync(artwork);

            var request = new StockChangeRequest
            {
                StockType = "poster",
                Quantity = 5
            };

            // Act
            await _service.AddStockAsync(artworkId, request);

            // Assert
            Assert.Equal(15, artwork.PosterStockQty);
            _mockRepo.Verify(repo => repo.UpdateAsync(artwork), Times.Once);
        }
        
    }
}