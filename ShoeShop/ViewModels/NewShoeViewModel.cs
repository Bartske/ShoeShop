using Microsoft.AspNetCore.Http;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.ViewModels
{
    public class NewShoeViewModel
    {
        public Shoe newShoe { get; set; }
        public List<Group> Groups { get; set; }
        public List<Brand> Brands { get; set; }
        public IFormFile file { get; set; }
        public int GroupID { get; set; }
        public int BrandID { get; set; }
    }
}
