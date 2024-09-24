using System.ComponentModel.DataAnnotations;

namespace ArtGalleryAPI.Models
{
    public class StockChangeRequest
    {
        [Required]
        public string StockType { get; set; } // "artwork" or "poster"

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
    }
}