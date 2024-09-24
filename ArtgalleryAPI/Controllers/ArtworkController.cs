using System.Collections.Generic;
using System.Threading.Tasks;
using ArtgalleryAPI.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using ArtGalleryAPI.Models;
using ArtGalleryAPI.Repositories;
using ArtGalleryAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkService _artworkService;

        public ArtworksController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        // GET: api/artworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artwork>>> GetArtworks()
        {
            var artworks = await _artworkService.GetAllArtworksAsync();
            return Ok(artworks);
        }

        // GET: api/artworks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Artwork>> GetArtwork(int id)
        {
            var artwork = await _artworkService.GetArtworkByIdAsync(id);

            if (artwork == null)
            {
                return NotFound();
            }

            return Ok(artwork);
        }

        // POST: api/artworks
        [HttpPost]
        public async Task<ActionResult<Artwork>> PostArtwork(Artwork artwork)
        {
            await _artworkService.AddArtworkAsync(artwork);
            return CreatedAtAction(nameof(GetArtwork), new { id = artwork.Id }, artwork);
        }

        // PUT: api/artworks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtwork(int id, Artwork artwork)
        {
            if (id != artwork.Id)
            {
                return BadRequest();
            }

            try
            {
                await _artworkService.UpdateArtworkAsync(artwork);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _artworkService.ArtworkExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/artworks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtwork(int id)
        {
            var artwork = await _artworkService.ArtworkExistsAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            await _artworkService.DeleteArtworkAsync(id);
            return NoContent();
        }

        // POST: api/artworks/{id}/stock
        [HttpPost("{id}/stock")]
        public async Task<IActionResult> AddStock(int id, [FromBody] StockChangeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artworkService.AddStockAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/artworks/{id}/stock
        [HttpDelete("{id}/stock")]
        public async Task<IActionResult> RemoveStock(int id, [FromBody] StockChangeRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _artworkService.RemoveStockAsync(id, request);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
