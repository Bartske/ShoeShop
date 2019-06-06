using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Models.ValidationClasses
{
    public class PasswordValidate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            string val = value.ToString();

            string Message = string.Empty;

            if (!val.Any(char.IsUpper))
            {
                Message = "The password needs a minimum of 1 capital letter!";
                return new ValidationResult(Message);
            }
            else if (!val.Any(char.IsLower))
            {
                Message = "The password needs a minimum of 1 lowercase letter!";
                return new ValidationResult(Message);
            }
            else if (!val.Any(char.IsNumber))
            {
                Message = "The password needs a minimum of 1 number!";
                return new ValidationResult(Message);
            }
            else if (!val.Any(ch => !Char.IsLetterOrDigit(ch)))
            {
                Message = "The password needs a minimum of 1 special sign!";
                return new ValidationResult(Message);
            }
            else if (val.Length < 8)
            {
                Message = "The password needs to be atleast 8 characters long!";
                return new ValidationResult(Message);
            }

            return ValidationResult.Success;

        }
    }
}
