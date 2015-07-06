using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edstart.DTO
{
    public class ParentDashboardFilter
    {
        public string StudentName { get; set; }
        public int Term { get; set; }
        public eBorroweStatus? LoanStatus { get; set; }
    }
}