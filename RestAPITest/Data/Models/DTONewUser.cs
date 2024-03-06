using System.ComponentModel.DataAnnotations;

namespace RestAPITest.Data.Models
{
    public class DTONewUser
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
