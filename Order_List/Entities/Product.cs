using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order_List.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PhotoUrl { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public Product()
        {
            Orders = new List<Order>();
        }
    }
}