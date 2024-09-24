using System.Collections.Generic;
using System.Threading.Tasks;
using ArtgalleryAPI.Data.Repository;
using Microsoft.AspNetCore.Mvc;
using ArtGalleryAPI.Models;
using ArtGalleryAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtworksController : ControllerBase
    {
        private readonly IArtworkRepository _repository;

        public ArtworksController(IArtworkRepository repository)
        {
            _repository = repository;
        }

        // GET: api/artworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artwork>>> GetArtworks()
        {
            var artworks = await _repository.GetAllAsync();
            return Ok(artworks);
        }

        // GET: api/artworks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Artwork>> GetArtwork(int id)
        {
            var artwork = await _repository.GetByIdAsync(id);

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
            await _repository.AddAsync(artwork);
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
                await _repository.UpdateAsync(artwork);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ArtworkExistsAsync(id))
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
            var artwork = await _repository.GetByIdAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

        // POST: api/artworks/{id}/stock
        [HttpPost("{id}/stock")]
        public async Task<IActionResult> AddStock(int id, [FromBody] int quantity)
        {
            var artwork = await _repository.GetByIdAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            artwork.Stock += quantity;
            await _repository.UpdateAsync(artwork);

            return NoContent();
        }

        // DELETE: api/artworks/{id}/stock
        [HttpDelete("{id}/stock")]
        public async Task<IActionResult> RemoveStock(int id, [FromBody] int quantity)
        {
            var artwork = await _repository.GetByIdAsync(id);
            if (artwork == null)
            {
                return NotFound();
            }

            artwork.Stock -= quantity;
            if (artwork.Stock < 0)
            {
                return BadRequest("Insufficient stock.");
            }

            await _repository.UpdateAsync(artwork);

            return NoContent();
        }
    }
}
