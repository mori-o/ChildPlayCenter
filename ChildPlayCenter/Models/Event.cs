using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public int AverageAge { get; set; }
        public int TypeId { get; set; }
        public string? Description { get; set; }
        public int? AnimatorId { get; set; }

        // Связанные сущности
        public Service? Service { get; set; }
        public Person? Animator { get; set; } // Навигационное свойство для аниматора
    }
}
