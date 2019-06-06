using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoeShop.ViewModels
{
    public class ManageViewModel
    {
        public NewShoeViewModel newShoeViewModel { get; set; }
        public ShoeListViewModel shoeListViewModel { get; set; }
        public RegisterViewModel registerViewModel { get; set; }
        public AccountListViewModel accountListViewModel { get; set; }
    }
}
