using System.ComponentModel.DataAnnotations;

namespace Common.Contracts.v1.Account
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
