
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using NowePWI.App_Start;
using NowePWI.Models;
using NowePWI.ViewModels;

namespace NowePWI.Controllers
{
    public class HomeController : MyBaseController
    {
        // GET: Home


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ChangeLanguage(string lang)
        {
            new SiteLanguages().SetLanguage(lang);
            try
            {
                return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Prefix = "EmailViewModelEmail")]EmailViewModelEmail viewmodel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    string title = viewmodel.Title + " od: " + User.Identity.GetUserName();
                    MailMessage mail = new MailMessage("pietruczukhubert@gmail.com", "pietruczukhubert@gmail.com", title, viewmodel.Message);
                    SmtpClient klient = new SmtpClient("smtp.gmail.com");

                    klient.Port = 587;
                    klient.Credentials = new NetworkCredential("pietruczukhubert@gmail.com", "***********");//pamiętać żeby zmienić hasło 
                    klient.EnableSsl = true;
                    klient.Send(mail);
                }
                catch (Exception)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public HomeController()
        {
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Index", "Home");
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(IndexViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.LoginViewModel.Email, model.LoginViewModel.Password, model.LoginViewModel.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Index");
                default:
                    TempData["errors"] = "Nieudana proba logowania";
                    return View("Index", model);
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(IndexViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.RegisterViewModel.Email, Email = model.RegisterViewModel.Email, UserData = new UserData() };
                var result = await UserManager.CreateAsync(user, model.RegisterViewModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                TempData["errors"] = "Nieudana proba rejestracji";
                return View("Index", model);
            }

            // If we got this far, something failed, redisplay form
            return RedirectToAction("Index", "Home", model);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }



        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, create account with external provider login
                    // in reality, we might ask for providing e-mail (+ confirming it)
                    // we also need some error checking logic (ie. verification if user doesn't already exist)

                    var user = new ApplicationUser
                    {
                        UserName = loginInfo.Email,
                        Email = loginInfo.Email,
                        UserData = new UserData { Email = loginInfo.Email }
                    };

                    var registrationResult = await UserManager.CreateAsync(user);
                    if (registrationResult.Succeeded)
                    {
                        registrationResult = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                        if (registrationResult.Succeeded)
                        {
                            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                            return RedirectToLocal(returnUrl);
                        }
                        else
                            throw new Exception("External provider association error");
                    }
                    else
                        throw new Exception("Registration error");
            }
        }


        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        public ActionResult SendCode(string returnurl)
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        public void CreateCookies()
        {
            HttpCookie ciastko = new HttpCookie("torcik");
            ciastko.Values.Add("Powiadomienie", "Zamknij");
            ciastko.Expires.AddMinutes(30);
            Response.Cookies.Add(ciastko);

            //return RedirectToAction("Index", "Home");
        }
    }
}