using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Order_List.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        public int StatusId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Status Status { get; set; }
    }
}