using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Login
    {
        public int ID { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "This field is required!")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is required!")]
        [ValidationClasses.PasswordValidate]
        public string Password { get; set; }

        public string HashedPassword { get; set; }

        public string Salt { get; set; }
    }
}
