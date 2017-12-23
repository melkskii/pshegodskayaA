using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace _8Ball
{
    class WrongEmailValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            if (String.IsNullOrWhiteSpace(value.ToString()))
            {
                // Logger.Log.Error("Email field is empty");
                throw new ArgumentNullException("Email field is empty");

            }
            if (Regex.IsMatch(value.ToString(), @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                return new ValidationResult(true, String.Empty);
            }
            else return new ValidationResult(false, "Wrong email!");
        }
    }
}
