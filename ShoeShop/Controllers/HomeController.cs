using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using ShoeShop.ViewModels;
using Microsoft.AspNetCore.Http;


namespace ShoeShop.Controllers
{
    public class HomeController : Controller
    {
        ShoeLogic shoeLogic;

        public HomeController()
        {
            shoeLogic = new ShoeLogic();
        }

        public IActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();

            model.BestSelling = shoeLogic.GetBestSellingShoes();
            model.Banner = shoeLogic.GetBestSellingShoes().Take(2).ToList();
            model.Latest = shoeLogic.GetLatestShoes();
            
            return View(model);
        }
        
        public IActionResult Contact()
        {
            return View();
        }
    }
}
