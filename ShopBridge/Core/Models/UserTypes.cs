using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Core.Models
{
    public class UserTypes
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
