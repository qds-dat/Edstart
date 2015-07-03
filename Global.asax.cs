using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Edstart
{
    public class MvcApplication : System.Web.HttpApplication
    {
        AccountService accountService = new AccountService(new DatabaseContext());
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {               
                try {
                    var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    
                    //var indexOfRole = ticket.Name.LastIndexOf('_') + 1;
                    //var arr = ticket.Name.Split('|');

                   // string name = arr[0]; //ticket.Name.Substring(indexOfRole, ticket.Name.Length - indexOfRole);
                    string role = ticket.UserData; // ticket.Name.Substring(0, indexOfRole - 1);

                    //foreach (var role in user.Roles)
                    //{
                    //    claimsIdentity.AddClaim(
                    //        new Claim(ClaimTypes.Role, role));
                    //}
                   // ticket.Name = name;
                    FormsIdentity formsIdentity = new FormsIdentity(ticket);
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(formsIdentity);
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));                    
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    HttpContext.Current.User = claimsPrincipal;
                }
                catch
                {
                    HttpContext.Current.User = null;
                }
                
            }
        }
    }
}
