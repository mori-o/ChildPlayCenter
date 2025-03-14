using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Role? Role { get; set; }
    }
}
