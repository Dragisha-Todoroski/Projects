using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoGameLibraryApp.Services.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "{0} can't be blank!")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [EmailAddress(ErrorMessage = "{0} must be in a proper email address format")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "{0} can't be blank!")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "{1} and {0} must match!")]
        public string? ConfirmPassword { get; set; }

    }
}
