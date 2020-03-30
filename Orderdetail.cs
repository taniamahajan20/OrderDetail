using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderDetail
{
    public class Orderdetail
    {
        public User UserDetails { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
