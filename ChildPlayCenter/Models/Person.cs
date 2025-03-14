using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildPlayCenter.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string? Login { get; set; }
        [Required]
        public string? PasswordHash { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Role? Role { get; set; }
    }
}
