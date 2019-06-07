using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.ViewModels
{
    public class ProductOverViewViewModel
    {
        public List<Shoe> shoes { get; set; }
        public int Page { get; set; }
        public int NumberOfPages { get; set; }
    }
}
