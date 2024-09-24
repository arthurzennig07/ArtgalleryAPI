using Xunit;
using Moq;
using ArtGalleryAPI.Controllers;
using ArtGalleryAPI.Services;
using ArtGalleryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArtGalleryAPI.Tests
{
    public class ArtworksControllerTests
    {
        private readonly Mock<IArtworkService> _mockService;
        private readonly ArtworksController _controller;

        public ArtworksControllerTests()
        {
            _mockService = new Mock<IArtworkService>();
            _controller = new ArtworksController(_mockService.Object);
        }

        [Fact]
        public async Task GetArtworks_ReturnsOkResult_WithListOfArtworks()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllArtworksAsync())
                .ReturnsAsync(GetTestArtworks());

            // Act
            var result = await _controller.GetArtworks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var artworks = Assert.IsType<List<Artwork>>(okResult.Value);
            Assert.Equal(2, artworks.Count);
        }

        private List<Artwork> GetTestArtworks()
        {
            return new List<Artwork>
            {
                new Artwork { Id = 1, Title = "Artwork 1", ArtistName = "Artist A" },
                new Artwork { Id = 2, Title = "Artwork 2", ArtistName = "Artist B" }
            };
        }
        
    }
}