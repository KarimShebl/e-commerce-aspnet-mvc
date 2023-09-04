using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using e_commerce.Data.Enums;
using ecommerce.Areas.Identity.Data;
using System.Diagnostics.CodeAnalysis;

namespace ecommerce.Models
{
    namespace e_commerce.Models
    {
        public class Product
        {
            [Key]
            public int Id { get; set; }

            [Required]
            public string? Name { get; set; }

            [Required]
            public string? Description { get; set; }

            [Required]
            public Category Category { get; set; }

            public int Price { get; set; }

            [NotMapped, Required, Display(Name = "Choose the product image")]
            public IFormFile? Image { get; set; }

            public string? ImageURL { get; set; }

            [ForeignKey("User"), Required]
            public string SellerId { get; set; }
            public string SellerName { get; set; }

            [ForeignKey("User"), AllowNull]
            public string CartId { get; set; }

            [Required]
            public int Quantity { get; set; }

            public bool CartProduct { get; set; }

            public int OriginalId { get; set; }
        }
    }

}
