using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Core.Models
{
    public class Users
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Email { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
        public UserTypes UserType { get; set; }
        public int UserTypeId { get; set; }
    }
}
