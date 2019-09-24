using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.User
{
    public class UserForRegisterRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8")]
        public string Password { get; set; }
    }
}
