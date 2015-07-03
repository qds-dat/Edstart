using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using System.Web;

namespace Edstart.Services
{
    public class AccountService
    {
        private DatabaseContext db = null;

        public AccountService() {
            db = new DatabaseContext();
        }
        public AccountService(DatabaseContext context) {
            db = context;
        }

        public Result Login(Account account)
        {
            Result res = new Result();
            try
            {
                var user = db.Accounts.Where(x => x.Email == account.Email && x.Password == account.Password).FirstOrDefault();
                if (user == null)
                    return res.Fail("This Account invalid");

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddDays(15), true, user.Role.ToString());
                string encTicket = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                cookie.HttpOnly = true;
                HttpContext.Current.Response.Cookies.Add(cookie);
                return res.Success(user.Role);

            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

        public Result GetByEmail(string Email)
        {
            Result res = new Result();
            try
            {
                var user = db.Accounts.Where(x => x.Email == Email).FirstOrDefault();
                if (user == null)
                    return res.Fail("This Account invalid");

                return res.Success(user);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }
    }
}