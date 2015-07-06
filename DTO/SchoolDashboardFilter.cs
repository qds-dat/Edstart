using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edstart.Models;
namespace Edstart.DTO
{
    public class SchoolDashboardFilter
    {
        public string StudentName { get; set; }
        public string ParentName { get; set; }
        public int Term { get; set; }
        public eBorroweStatus? LoanStatus { get; set; }
    }
}