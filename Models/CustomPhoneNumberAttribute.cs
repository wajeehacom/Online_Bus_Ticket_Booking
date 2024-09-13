

//using System.ComponentModel.DataAnnotations;
//using System.Text.RegularExpressions;

//namespace WebApplication1.Models
//{
//    public class CustomPhoneNumberAttribute : ValidationAttribute
//    {
//        private readonly string _pattern;

//        public CustomPhoneNumberAttribute(string pattern = @"^(0\d{10}|\+92\d{10})$")
//        {
//            _pattern = pattern;
//        }

//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {
//            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
//            {
//                return new ValidationResult("Phone number is required.");
//            }

//            var phoneNumber = value.ToString();
//            if (!Regex.IsMatch(phoneNumber, _pattern))
//            {
//                return new ValidationResult("Phone number is not valid.");
//            }

//            return ValidationResult.Success;
//        }
//    }
//}




using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication1.Models
{
    public class CustomCountryNameAttribute : ValidationAttribute
    {
        private readonly string _pattern;

        public CustomCountryNameAttribute(string pattern = @"^[A-Za-z]+$")
        {
            _pattern = pattern;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Country name is required.");
            }

            var countryName = value.ToString();
            if (!Regex.IsMatch(countryName, _pattern))
            {
                return new ValidationResult("Country name must only contain letters.");
            }

            return ValidationResult.Success;
        }
    }
}
