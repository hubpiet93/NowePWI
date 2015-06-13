using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace NowePWI.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "WymaganyEmail")]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "WymaganeHaslo")]
        [DataType(DataType.Password)]
        [Display(Name = "Haslo", ResourceType = typeof(Resource))]
        [MembershipPassword(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "HasloInfo")]
        public string Password { get; set; }

        [Display(Name = "ZapamietajMnie", ResourceType = typeof(Resource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceName = "WymaganyEmail", ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "WymaganeHaslo", ErrorMessageResourceType = typeof(Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "HasloInfo", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [MembershipPassword(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "HasloInfo")]
        [Display(Name = "Haslo", ResourceType = typeof(Resource))]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "HasloPotwierdz", ResourceType = typeof(Resource))]
        [Compare("Password", ErrorMessageResourceName = "HasloPorownanie", ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resource))]
        public string Email { get; set; }
    }
}