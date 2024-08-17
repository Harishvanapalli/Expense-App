using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data_Access_Layer.helpers
{
    public class CustomValidationAttributes : ValidationAttribute
    {

        private readonly int _maxMembers;
        public CustomValidationAttributes(int maxMembers)
        {
            _maxMembers = maxMembers;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as List<string>;

            if(list == null)
            {
                return new ValidationResult("Invalid Members type");
            }
            if(list.Count == 0)
            {
                return new ValidationResult("Members list should not be empty");
            }
            if(list.Count > _maxMembers)
            {
                return new ValidationResult($"The Group can't contain more than {_maxMembers} members");
            }
            return ValidationResult.Success;
        }
    }

    public class SpliAmongValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as List<string>;
            if(list == null)
            {
                return new ValidationResult("The type of Split Among list is Invalid");
            }
            if(list.Count == 0)
            {
                return new ValidationResult("The Split Among List can't be empty");
            }
            return ValidationResult.Success;
        }
    }

    public class PasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            var password = value.ToString();

            if (password.Length < 8 || password.Length > 12) return false;

            if (!Regex.IsMatch(password, @"[a-z]")) return false;
            if (!Regex.IsMatch(password, @"[A-Z]")) return false;
            if (!Regex.IsMatch(password, @"[0-9]")) return false;
            if (!Regex.IsMatch(password, @"[\W_]")) return false;

            return true;
        }
    }
}
