using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Edstart.Models;
using Edstart.Services;
using Edstart.DTO;
using Edstart.Framework;
namespace Edstart.Controllers
{
    
    [Authorize(Roles = "Parent")]
    public class ParentController : CustomParentController
    {
        private DatabaseContext db = new DatabaseContext();
        private AccountService accountService;
        private ParentService parentService;
        private InvestmentService investmentService;
        public ParentController()
        {
            accountService = new AccountService(db);
            parentService = new ParentService(db);
            investmentService = new InvestmentService(db);
        }
        

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View(new ParentRegister());
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(ParentRegister model,bool sample = false)
        {
            if (sample)
                return View(new ParentRegister(sample));

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = model.parent.Register(db,model.file);
                if (result.State)
                {
                    var result1 = accountService.Login(model.parent.Account);
                    if (!result1.State)
                        return RedirectToAction("Login", "Home");

                    return RedirectToAction("AfterLogin");
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View(model);
                }
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult EmailApprove(EmailApprove emailApprove)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = parentService.ApproveEmail(emailApprove);
            if(!result.State)
                ViewBag.Message = result.Message;

            return View();
        }

        public ActionResult AfterLogin()
        {
            return View();
        }

        // GET: Borrowers
        public ActionResult Index()
        {
            var result = parentService.GetParentInformationByParentId(SessionFactory.ParentId);
            if (!result.State)
            {
                return RedirectToAction("Login", "Home");
            }
            Parent parent = (Parent)result.RetVal;

            return View(parent);
        }

        // GET: Borrowers
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: Borrowers
        [HttpPost]
        public ViewResult Dashboard(ParentDashboardFilter filter)
        {
            return View("Partials/Dashboard",parentService.GetParentDashboard(SessionFactory.ParentId,filter));
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
