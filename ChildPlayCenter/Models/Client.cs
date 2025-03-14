using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Client
    {
        public int Id { get; set; }
        [Required]
        public int PersonId { get; set; }
        [Required]
        [StringLength(200)]
        public string? ContactInfo { get; set; }
        [Required]
        public int IncomeLevelId { get; set; }
        public Person? Person { get; set; }
    }
}
