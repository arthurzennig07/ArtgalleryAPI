using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ArtGalleryAPI.Models
{
    public class Artwork
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Medium { get; set; }

        public string Dimensions { get; set; }

        public string DimensionUnit { get; set; }

        public float Weight { get; set; }

        public string WeightUnit { get; set; }

        public DateTime CreationDate { get; set; }

        public string Availability { get; set; }

        public string Ownership { get; set; }

        public DateTime? SellDate { get; set; }

        public string Category { get; set; }

        public string ImageURI { get; set; }

        public string Keyword { get; set; }

        public int Stock { get; set; }
        
    }
}