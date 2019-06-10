using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoeShop.ViewModels;
using Models;
using Logic;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace ShoeShop.Controllers
{
    public class AccountController : Controller
    {
        AccountLogic AccountLogic = new AccountLogic();
        ShopLogic shopLogic = new ShopLogic();

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
                return View("Login", login);

            login = AccountLogic.CheckLogin(login);
            Account account = AccountLogic.GetAccount(login.ID);

            if (account.MiddleName == null)
                HttpContext.Session.SetString("FullName", account.FirstName + " " + account.LastName);
            else
                HttpContext.Session.SetString("FullName", account.FirstName + " " + account.MiddleName + " " + account.LastName);
            
            HttpContext.Session.SetString("AccountID", account.ID.ToString());

            if (account.Admin)
            {
                return RedirectToAction("Index", "Manage");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        public IActionResult Account(int AccountID)
        {
            AccountViewModel model = new AccountViewModel()
            {
                editAccountViewModel = new EditAccountViewModel()
                {
                    account = AccountLogic.GetAccountByID(AccountID),
                    login = AccountLogic.GetAccountByID(AccountID).Login,
                    EditButton = true
                },
                orderListViewModel = new orderListViewModel()
                {
                    orders = shopLogic.GetOrderFromUser(AccountID)
                }
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Register", model);

            //Upload the data
            model.account.Login = AccountLogic.CreateLogin(model.login);
            AccountLogic.CreateAccount(model.account);
           
            return View(model);
        }

        public void ResetPassword(int ID)
        {
            AccountLogic.ResetPassword(ID);
        }
    }
}