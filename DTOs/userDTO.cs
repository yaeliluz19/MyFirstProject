using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class userDTO
    {
        [Required,EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;
        [MaxLength(20, ErrorMessage = "first name must be less than 20 characters long")]
        public string? FirstName { get; set; }
        [MaxLength(20, ErrorMessage = "last name must be less than 20 characters long")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Invalid password")]
        public string Password { get; set; } = null!;
    }
}
