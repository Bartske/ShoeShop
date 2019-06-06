using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Account
    {
        public int ID { get; set; }

        [DisplayName("First Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string FirstName { get; set; }

        [DisplayName("Middel Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string LastName { get; set; }

        [DisplayName("Phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "This field is required!")]
        public int PhoneNumber { get; set; }

        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }

        [DisplayName("Country")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string Country { get; set; }

        [DisplayName("Address")]
        [RegularExpression(@"^[a-zA-Z0-9\s*]+$", ErrorMessage = "You can only use letters and numbers")]
        [Required(ErrorMessage = "This field is required!")]
        public string Address { get; set; }

        [DisplayName("City")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string City { get; set; }

        [DisplayName("ZIP code")]
        [DataType(DataType.PostalCode)]
        [Required(ErrorMessage = "This field is required!")]
        public string ZIPcode { get; set; }
        
        public Login Login { get; set; }

        public bool Admin { get; set; }

        public List<BagItem> Bag { get; set; }
    }
}
