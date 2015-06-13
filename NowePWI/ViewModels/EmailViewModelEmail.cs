using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NowePWI.ViewModels
{
    public class EmailViewModelEmail
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Wymagane")]
        [Display(Name = "Tytul", ResourceType = typeof(Resource))]
        public string Title { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "Wymagane")]
        [Display(Name = "Wiadomosc", ResourceType = typeof(Resource))]
        public string Message { get; set; }

    }
}