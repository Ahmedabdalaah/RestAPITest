using System.ComponentModel.DataAnnotations;

namespace RestAPITest.Data.Models
{
    public class DTOLogin
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
