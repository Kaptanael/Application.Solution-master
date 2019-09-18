using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string FirstName { get; set; }

        [Required, MaxLength(64)]
        public string LastName { get; set; }

        [Required, MaxLength(64)]
        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        [Required, MaxLength(14)]
        public string MobileNumber { get; set; }

        [Required]
        [MaxLength(512)]
        public string Address { get; set; }

        [Required, MaxLength(64)]
        public string Role { get; set; }

        public ICollection<UserTask> Tasks { get; set; }
    }
}
