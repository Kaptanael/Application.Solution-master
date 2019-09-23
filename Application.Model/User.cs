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
        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }          
    }
}
