using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Services.ServiceHelpers.CustomValidators
{
    public class MaximumStringLengthAllowedValidatorAttribute : ValidationAttribute
    {
        private int _stringLength { get; set; }
        private string _defaultErrorMessage { get; set; } = "{0} can't be longer than {1} characters long!";
        
        public MaximumStringLengthAllowedValidatorAttribute(int stringLength)
        {
            _stringLength = stringLength;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }

            string property = (string)value;
            if (string.IsNullOrEmpty(property))
            {
                return null;
            }

            if (property.Length > _stringLength)
            {
                return new ValidationResult(string.Format(ErrorMessage ?? _defaultErrorMessage, validationContext.DisplayName, _stringLength));
            }

            return ValidationResult.Success;
        }
    }
}
