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
                    shoes = shoeLogic.GetProductResults("", "", "", "", 6, 1),
                    Page = 1,
                    NumberOfPages = CalculateNumberOfPages(shoeLogic.GetProductFromFilter("", "", "", "").Count, 6)
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

        [HttpGet]
        public PartialViewResult ProductOverView(string brand, string group, string Color, string sort, int NumOfResults, int Page)
        {
            brand = (brand == "undefined") ? "" : brand;
            group = (group == "undefined") ? "" : group;
            Color = (Color == "undefined") ? "" : Color;
            sort = (sort == "undefined") ? "" : sort;

            ProductOverViewViewModel model = new ProductOverViewViewModel()
            {
                shoes = shoeLogic.GetProductResults(brand, group, Color, sort, NumOfResults, Page),
                Page = Page,
            };
            model.NumberOfPages = CalculateNumberOfPages(shoeLogic.GetProductFromFilter(brand, group, Color, sort).Count, NumOfResults);
            return PartialView(model);
        }

        public void AddToBag(int ProductID, int AccountID)
        {
            shopLogic.AddToBag(ProductID, AccountID);
        }

        private int CalculateNumberOfPages(int NumberOfProducts, int NumberOrResults)
        {
            int Pages = NumberOfProducts / NumberOfProducts;
            if (NumberOrResults * Pages <  NumberOfProducts)
            {
                Pages += 1;
            }
            return Pages;
        }
    }
}