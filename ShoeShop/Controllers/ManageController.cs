using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Models;
using ShoeShop.ViewModels;

namespace ShoeShop.Controllers
{
    public class ManageController : Controller
    {
        ShoeLogic shoeLogic;
        AccountLogic accountLogic;

        public ManageController()
        {
            shoeLogic = new ShoeLogic();
            accountLogic = new AccountLogic();
        }


        public IActionResult Index()
        {
            return View(new ManageViewModel() {
                newShoeViewModel = new NewShoeViewModel() { Brands = shoeLogic.GetBrands(), Groups = shoeLogic.GetGroups()},
                shoeListViewModel = new ShoeListViewModel() { Shoes = shoeLogic.GetShoes()},
                accountListViewModel = new AccountListViewModel() { Accounts = accountLogic.GetAllAccounts()},
                registerViewModel = new RegisterViewModel() { account = new Account(), login = new Login()}
            });
        }

        public IActionResult CreateShoe(NewShoeViewModel model)
        {
            Shoe shoe = model.newShoe;
            shoe.Brand = shoeLogic.GetBrands().Single(b => b.ID == model.BrandID);
            shoe.Group = shoeLogic.GetGroups().Single(g => g.ID == model.GroupID);

            var fileName = Path.GetFileName(model.file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\product", fileName);
            using (var fileSteam = new FileStream(filePath, FileMode.Create))
            {
                model.file.CopyToAsync(fileSteam);
            }

            shoe.img = fileName;

            shoeLogic.CreateShoe(shoe);

            return Index();
        }

        [HttpGet]
        public IActionResult EditShoe(int id)
        {
            return View(new EditShoeViewModel()
            {
                Brands = shoeLogic.GetBrands(),
                Groups = shoeLogic.GetGroups(),
                shoe = shoeLogic.GetShoe(id)
            });
        }

        [HttpPost]
        public IActionResult EditShoe(EditShoeViewModel model)
        {

            Shoe shoe = model.shoe;
            shoe.Brand = shoeLogic.GetBrands().Single(b => b.ID == model.BrandID);
            shoe.Group = shoeLogic.GetGroups().Single(g => g.ID == model.GroupID);

            if (model.file != null)
            {
                var fileName = Path.GetFileName(model.file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\product", fileName);
                using (var fileSteam = new FileStream(filePath, FileMode.Create))
                {
                    model.file.CopyToAsync(fileSteam);
                }

                shoe.img = fileName;
            }
            else
            {
                shoe.img = shoeLogic.GetShoe(shoe.ID).img;
            }
            
            shoeLogic.EditShoe(shoe);

            return Index();
        }

        public void DeleteShoe(int id)
        {
            shoeLogic.DeleteShoe(id);
        }

        [HttpGet]
        public IActionResult EditAccount(int id)
        {
            return View(new EditAccountViewModel()
            {
                account = accountLogic.GetAccountByID(id),
                login = accountLogic.GetAccountByID(id).Login,
                EditButton=true
            });
        }

        [HttpPost]
        public IActionResult EditAccount(EditAccountViewModel model)
        {
            accountLogic.UpdateAccount(model.account);
            return Index();
        }

        public void DeleteAccount(int id)
        {
            accountLogic.DeleteAccount(id);
        }
    }
}