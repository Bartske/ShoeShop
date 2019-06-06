using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Shoe
    {
        public int ID { get; set; }

        [DisplayName("Image")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "This field is required!")]
        public string img { get; set; }

        [DisplayName("Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }

        [DisplayName("Price")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public int Price { get; set; }

        [DisplayName("Old Price")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public int OldPrice { get; set; }

        [DisplayName("Description")]
        [RegularExpression(@"^[a-zA-Z0-9\s*]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string Description { get; set; }

        [DisplayName("Group")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        public Group Group { get; set; }

        [DisplayName("Brand")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        public Brand Brand { get; set; }

        [DisplayName("Image")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "You can only use letters")]
        [Required(ErrorMessage = "This field is required!")]
        public string Color { get; set; }

        [DisplayName("Sale")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "The field Is Active must be checked.")]
        public bool Sale { get; set; }
        

        public DateTime DateAdded { get; set; }
        
        public List<ShoeSize> Sizes { get; set; }
    }
}
