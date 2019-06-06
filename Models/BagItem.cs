using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class BagItem
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public int ProductID { get; set; }
        public Shoe shoe { get; set; }
    }
}
