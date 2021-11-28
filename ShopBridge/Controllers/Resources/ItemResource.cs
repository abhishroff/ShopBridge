using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Controllers.Resources
{
    public class ItemResource
    {
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        //[Required]
        //public int UpdatedByUserId { get; set; }
    }
}
