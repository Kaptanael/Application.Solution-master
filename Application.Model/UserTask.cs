using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Model
{
    [Table("Task")]
    public class UserTask
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        [NotMapped]
        public string UserName { get; set; }
    }
}
