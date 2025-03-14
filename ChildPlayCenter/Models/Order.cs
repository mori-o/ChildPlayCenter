using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int StatusId { get; set; }
        public Client? Client { get; set; }
    }
}
