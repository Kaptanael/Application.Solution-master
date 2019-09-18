using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.User
{
    public class UserForRegisterDto
    {
        [Required, MaxLength(64)]
        public string FirstName { get; set; }

        [Required, MaxLength(64)]
        public string LastName { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }

        [Required]
        [MinLength(4),MaxLength(8)]
        public string Password { get; set; }

        [Required, MaxLength(14)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(512)]
        public string Address { get; set; }

        [Required, MaxLength(64)]
        public string Role { get; set; }
    }
}
