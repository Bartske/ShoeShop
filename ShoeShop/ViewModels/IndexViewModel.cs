using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace ShoeShop.ViewModels
{
    public class IndexViewModel
    {
        public List<Shoe> Banner { get; set; }
        public List<Shoe> Latest { get; set; }
        public List<Shoe> BestSelling { get; set; }
    }
}
