using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridge.Core
{
    public class UserDetails : IUserDetails
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}
