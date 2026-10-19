using System.ComponentModel.DataAnnotations;

namespace StoreManagment.Auth
{
    public class LoginRequestDto
    {
      

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;
    }
}
