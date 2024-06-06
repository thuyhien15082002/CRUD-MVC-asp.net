using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class ProductDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";

        [Required, MaxLength(100)]
        public string Category { get; set; } = "";
        
        public IFormFile? Image { get; set; }


        [Required]
        public decimal Price { get; set; }


        [Required, MaxLength(400)]
        public string Description { get; set; } = "";

        [Required]
        public int Status { get; set; }
    }
}
