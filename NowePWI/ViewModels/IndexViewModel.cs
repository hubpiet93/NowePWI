using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NowePWI.ViewModels
{
    public class IndexViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public RegisterViewModel RegisterViewModel { get; set; }
        public ExternalLoginConfirmationViewModel ExternalLoginConfirmationViewModel { get; set; }
        public EmailViewModelEmail EmailViewModelEmail { get; set; }
    }
}