using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class GameMachine
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int StatusId { get; set; }
        public DateTime LastUpdated { get; set; }
        public int UpdatedById { get; set; }
        public MachineStatus? Status { get; set; }
    }
}
