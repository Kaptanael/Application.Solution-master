using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.User
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
