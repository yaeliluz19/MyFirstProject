using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ProductDTO
    {
       
        public int ProductId { get; set; }
        [Required, MaxLength(30, ErrorMessage = "name must be less than 30 characters long")]
        public string ProductName { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }


    }
}
