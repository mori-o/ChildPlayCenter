using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Event
    {
        public int Id { get; set; }
        [Required]
        public int ServiceId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public int AverageAge { get; set; }
        [Required]
        public int TypeId { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public int? AnimatorId { get; set; }

        public Service? Service { get; set; }
        public Person? Animator { get; set; }
    }
}
