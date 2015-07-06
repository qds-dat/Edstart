using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edstart.DTO;
using Edstart.Framework;
namespace Edstart.Controllers
{
    [Authorize(Roles="School")]
    public class SchoolController : CustomSchoolController
    {
        private DatabaseContext db = new DatabaseContext();
        private AccountService accountService;
        private SchoolService schoolService;

        public SchoolController()
        {
            accountService = new AccountService(db);
            schoolService = new SchoolService(db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        // GET: Borrowers/Register
        public ActionResult Register()
        {
            return View(new SchoolRegister());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="sample"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]     
        public ActionResult Register(SchoolRegister model, bool sample = false)
        {
            if(sample)
                return View(new SchoolRegister(true)); 

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = model.school.Register(db, model.file);
                if (result.State)
                {
                    var result1 = accountService.Login(model.school.Account);
                    if (!result1.State)
                        return RedirectToAction("Login", "Home");

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View(model);
                }
            }
        }

        public ActionResult Index()
        {
            string Email = HttpContext.User.Identity.Name;
            var result = schoolService.GetSchoolInformationByEmail(Email);
            if (!result.State)
            {
                return RedirectToAction("Login","Home");
            }
            School investor = (School)result.RetVal; // await db.Schools.FindAsync();

            return View(investor);
        }

        public ViewResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Dashboard(SchoolDashboardFilter filter)
        {
            var result = schoolService.GetSchoolDashboard(SessionFactory.SchoolId, filter);
            List<Parent> listParent = (List<Parent>)result.RetVal;
            return View("Partials/Dashboard", listParent);
        }
    }
}