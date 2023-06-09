using System.ComponentModel.DataAnnotations;

namespace ManagingFinanceAPI.DTOModels
{
    public class UserLoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}