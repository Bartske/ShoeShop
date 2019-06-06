using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ShoeShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<BagItem> items { get; set; }
        public int AccountID { get; set; }
        public int SubTotal { get; set; }
    }
}
