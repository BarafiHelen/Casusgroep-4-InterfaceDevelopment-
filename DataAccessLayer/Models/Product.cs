using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DataAccessLayer.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        [Display(Name = "Product Image URL")]
        public string ImageUrl { get; set; }

        public bool IsActive { get; set; } = true;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        // Nieuw toegevoegd voor sale-functionaliteit
        [Display(Name = "Original Price")]
        [Range(0.01, 10000, ErrorMessage = "Original Price must be between 0.01 and 10000")]
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        public decimal? OriginalPrice { get; set; }

        [Display(Name = "Is On Sale")]
        public bool IsOnSale { get; set; } = false;
    }
}
