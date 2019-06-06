using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Order
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
        public List<OrderItem> items { get; set; }
    }
}
