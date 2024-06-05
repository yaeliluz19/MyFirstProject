using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Invalid password")]
        public string Password { get; set; }
        [Required, EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
    }
}
