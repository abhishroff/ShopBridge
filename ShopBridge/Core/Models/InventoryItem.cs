using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Core.Models
{
    public class InventoryItem
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        
        [StringLength(250)]
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public Users UpdatedByUser { get; set; }
        public int UpdatedByUserId { get; set; }

    }
}
