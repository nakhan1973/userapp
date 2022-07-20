
using System.ComponentModel.DataAnnotations;


namespace userDemo1.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Required")]
        [EmailAddress]
        public string UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}