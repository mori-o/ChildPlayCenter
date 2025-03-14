using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Client
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string? ContactInfo { get; set; }
        public int IncomeLevelId { get; set; }
        public Person? Person { get; set; }
    }
}
