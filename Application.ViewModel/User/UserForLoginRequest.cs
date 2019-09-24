using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.User
{
    public class UserForLoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
