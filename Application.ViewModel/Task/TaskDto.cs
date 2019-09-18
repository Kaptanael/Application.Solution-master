using System;
using System.ComponentModel.DataAnnotations;

namespace Application.ViewModel.Task
{
    public class TaskDto
    {
        public int Id { get; set; }

        [Required, MaxLength(64)]
        public string Name { get; set; }

        [Required, MaxLength(64)]
        public string OldName { get; set; }

        [Required, MaxLength(512)]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
