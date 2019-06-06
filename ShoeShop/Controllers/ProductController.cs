using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ShoeShop.ViewModels;

namespace ShoeShop.Controllers
{
    public class ProductController : Controller
    {
        ShoeLogic shoeLogic;
        ShopLogic shopLogic;

        public ProductController()
        {
            shoeLogic = new ShoeLogic();
            shopLogic = new ShopLogic();
        }

        public IActionResult Index()
        {
            ProductIndexViewModel model = new ProductIndexViewModel()
            {
                ProductOverViewViewModel = new ProductOverViewViewModel()
                {
                    shoes = shoeLogic.GetProductResults("", "", "", "", 6, 1)
                },
                brands = shoeLogic.GetBrands(),
                groups = shoeLogic.GetGroups(),
                Colors = shoeLogic.GetColors()
            };
            return View(model);
        }

        public IActionResult Product(int ID)
        {
            return View(
                new ProductViewModel()
                {
                    Shoe = shoeLogic.GetShoe(ID)
                }
            );
        }

        public PartialViewResult ProductOverView(string brand, string group, string Color, string sort, int NumOfResults, int Page)
        {
            ProductOverViewViewModel model = new ProductOverViewViewModel()
            {
                shoes = shoeLogic.GetProductResults(brand, group, Color, sort, NumOfResults, Page)
            };
            return PartialView(model);
        }

        public void AddToBag(int ProductID, int AccountID)
        {
            shopLogic.AddToBag(ProductID, AccountID);
        }
    }
}