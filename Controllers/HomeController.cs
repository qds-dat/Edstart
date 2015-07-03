using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edstart.Models;
using System.Threading.Tasks;
using System.Web.Security;
using Edstart.Services;
using Edstart.Framework;
namespace Edstart.Controllers
{
    public class ActionAnonymous : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext  context)
        {
            
            if (context.HttpContext.User.Identity.IsAuthenticated && context.ActionDescriptor.ActionName !="Logout")
            {
                if(context.HttpContext.User.IsInRole("School"))
                    context.Result = new RedirectResult("/School/Index");                                 
                else if(context.HttpContext.User.IsInRole("Investor"))
                    context.Result = new RedirectResult("/Investor/Index");  
                else if(context.HttpContext.User.IsInRole("Parent"))
                    context.Result = new RedirectResult("/Parent/Index");  
                return;
            }
            base.OnActionExecuting(context);
        }
    }

    [ActionAnonymous]
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private AccountService accountService;
        public HomeController() {
            accountService = new AccountService(db);
        }
        public ActionResult Index(string a = "")
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(EdStartEmail model)
        {
            string result = "";
            Email_Service es = new Email_Service();
            if (!ModelState.IsValid)
            {
                result = ModelState.Values.First().Errors.First().ErrorMessage;
            }
            else
            {
                if (es.EmailInteresting(model.Message))
                    result = "Your email has been added to our invite list. Thanks!";
                else
                    result = "Please try again !";
            }

            ViewBag.message = result ;
            return View();
        }

        public ActionResult Login()
        {
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            if(!ModelState.IsValid)
            {
                return View(account);
            }

            var result = accountService.Login(account);
            if(!result.State){
                ViewBag.Message = result.Message;
                return View(account);
            }

            eRole role = (eRole)result.RetVal;
                       
            string controll = "Home";
            switch (role)
            {
                case eRole.Parent:
                    controll = eRole.Parent.ToString();
                    break;
                case eRole.Investor:
                    controll = eRole.Investor.ToString();
                    break;
                case eRole.School:
                    controll = eRole.School.ToString();
                    break;
            }

            return RedirectToAction("Index",controll);
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SessionFactory.ClearAllSession();
            return View("Index");
        }

        public ActionResult About(EdStartEmail a)
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}