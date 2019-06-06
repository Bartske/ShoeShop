using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ShoeShop.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
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