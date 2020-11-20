using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order_List.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Status()
        {
            Orders = new List<Order>();
        }
    }
}