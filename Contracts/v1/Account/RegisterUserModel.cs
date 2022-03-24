using Common.Contracts.v1.Organisation;
using System.ComponentModel.DataAnnotations;

namespace Common.Contracts.v1.Account
{
    public class RegisterUserModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CountryCode { get; set; }

        public OrganisationPayload Organisation { get; set; }
    }

    public class AddUserToOrganisationModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string CountryCode { get; set; }

        [Required]
        public int OrganisationId { get; set; }

    }
}
