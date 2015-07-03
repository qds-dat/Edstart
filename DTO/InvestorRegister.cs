using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edstart.DTO
{
    public class InvestorRegister
    {
        CommonService cs = new CommonService();
        public InvestorRegister() {
            investor = new Investor() {};
        }

        public InvestorRegister(bool state = true)
        {
            investor = cs.DummyData<Investor>();
            investor.Account = new Account()
            {
                Email = cs.GenRandomEmail(),
                Password = "123456"
            };
        }

        public Investor investor { get; set; }
        public HttpPostedFileBase file {get;set;}
    }
}