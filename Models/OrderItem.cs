using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public Shoe shoe { get; set; }
    }
}
