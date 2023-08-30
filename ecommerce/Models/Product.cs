using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using e_commerce.Data.Enums;
using ecommerce.Areas.Identity.Data;

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

            public DateTime PublishedDate { get; set; }

            [NotMapped]
            public IFormFile? Image { get; set; }

            public string? ImageURL { get; set; }

            [ForeignKey("User")]
            public string SellerId { get; set; }

            [NotMapped]
            public User? Seller { get; set; }
        }
    }

}
