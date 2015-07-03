using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System;

namespace Edstart.Framework
{
    public static class SessionFactory
    {
        public static int InvestorId { 
            get {
                return HttpContext.Current.Session["InvestorId"] != null ? (int)HttpContext.Current.Session["InvestorId"] : 0;
            }
            set{
                HttpContext.Current.Session["InvestorId"] = value;
            } 
        }

        public static int ParentId
        {
            get
            {
                return HttpContext.Current.Session["ParentId"] != null ? (int)HttpContext.Current.Session["ParentId"] : 0;
            }
            set
            {
                HttpContext.Current.Session["ParentId"] = value;
            }
        }

        public static int SchoolId
        {
            get
            {
                return HttpContext.Current.Session["SchoolId"] != null ? (int)HttpContext.Current.Session["SchoolId"] : 0;
            }
            set
            {
                HttpContext.Current.Session["SchoolId"] = value;
            }
        }

        public static void ClearAllSession()
        {
            InvestorId = 0;
            ParentId = 0;
            SchoolId = 0;
        }
    }
}