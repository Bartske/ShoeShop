using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ShoeShop.ViewModels
{
    public class CheckOutViewModel
    {
        public EditAccountViewModel accountModel { get; set; }
        public List<BagItem> items { get; set; }
    }
}
