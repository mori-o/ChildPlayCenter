using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ServiceId { get; set; }
        public DateTime AddedAt { get; set; }
        public Client? Client { get; set; }
        public Service? Service { get; set; }
    }
}
