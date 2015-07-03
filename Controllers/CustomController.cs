using Edstart.Framework;
using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Edstart.Controllers
{
    public class CustomSchoolController : Controller
    {
        private SchoolService schoolService = new SchoolService();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is AllowAnonymousAttribute)) return;
            if (SessionFactory.ParentId == 0)
            {
                string Email = HttpContext.User.Identity.Name;
                var result = schoolService.GetSchoolInformationByEmail(Email);
                if (!result.State)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary 
                        { 
                            { "controller", "Home" }, 
                            { "action", "Logout" } 
                        });
                }
                SessionFactory.SchoolId = ((School)result.RetVal).ID;
            }
        }
    }


    public class CustomParentController : Controller
    {
        private ParentService parentService = new ParentService();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is AllowAnonymousAttribute)) return;
            if (SessionFactory.ParentId == 0)
            {
                string Email = HttpContext.User.Identity.Name;
                var result = parentService.GetParentInformationByEmail(Email);
                if (!result.State)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary 
                        { 
                            { "controller", "Home" }, 
                            { "action", "Logout" } 
                        });
                }
                SessionFactory.ParentId = ((Parent)result.RetVal).ID;
            }

        }

    }

    public class CustomInvestorController : Controller
    {
        private InvestorService investorService = new InvestorService();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object[] attributes = filterContext.ActionDescriptor.GetCustomAttributes(true);
            if (attributes.Any(a => a is AllowAnonymousAttribute)) return;
            if (SessionFactory.InvestorId == 0)
            {
                string Email = HttpContext.User.Identity.Name;
                var result = investorService.GetInvestorInformationByEmail(Email);
                if (!result.State)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary 
                        { 
                            { "controller", "Home" }, 
                            { "action", "Logout" } 
                        });
                }
                SessionFactory.InvestorId = ((Investor)result.RetVal).ID;
            }

        }

    }

}