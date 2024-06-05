using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CategoryDTO
    {
        [Required, MaxLength(20, ErrorMessage = "name must be less than 20 characters long")]
        public string? CategoryName { get; set; }
    }
}
