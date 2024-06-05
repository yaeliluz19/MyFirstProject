using MyFirstProject;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        [Required, DataType("dd-mm-yyyy")]
        public DateTime? OrderDate { get; set; }
        [Required]
        public int? OrderSum { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual ICollection<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
