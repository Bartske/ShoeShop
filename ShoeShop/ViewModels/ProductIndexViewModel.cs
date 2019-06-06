using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.ViewModels
{
    public class ProductIndexViewModel
    {
        public ProductOverViewViewModel ProductOverViewViewModel { get; set; }
        public List<Group> groups { get; set; }
        public List<Brand> brands { get; set; }
        public List<string> Colors { get; set; }
    }
}
