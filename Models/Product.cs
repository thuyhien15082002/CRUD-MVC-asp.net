using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MVC.Models
{
    
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(100)]
        public string Image { get; set; }


        [Precision(16, 2)]
        public decimal Price { get; set; }
        
        [MaxLength(400)]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        
       


        

    }
}



