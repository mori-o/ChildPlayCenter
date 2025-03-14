using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class GameMachine
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Name { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        [Required]
        public int UpdatedById { get; set; }
        public MachineStatus? Status { get; set; }
    }
}
