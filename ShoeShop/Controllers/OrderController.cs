using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using ShoeShop.ViewModels;
using Logic;

namespace ShoeShop.Controllers
{
    public class OrderController : Controller
    {
        ShopLogic shopLogic;

        public OrderController()
        {
            shopLogic = new ShopLogic();
        }

        public IActionResult ShoppingCart()
        {
            int AccountID = Convert.ToInt32(HttpContext.Session.GetString("AccountID"));

            ShoppingCartViewModel model = new ShoppingCartViewModel()
            {
                AccountID = AccountID,
                items = shopLogic.GetBagItems(AccountID)
            };
            int subtotal = 0;
            for (int i = 0; i < model.items.Count; i++)
            {
                subtotal += model.items[i].shoe.Price;
            }

            model.SubTotal = subtotal;

            return View(model);
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}