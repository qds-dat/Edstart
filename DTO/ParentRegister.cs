using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edstart.Models;
using Edstart.Services;
namespace Edstart.DTO
{
    public class ParentRegister 
    {
        CommonService cs = new CommonService();
        public ParentRegister() {
            parent = new Parent();
        }

        public ParentRegister(bool state = true)
        {            
            parent = cs.DummyData<Parent>();
            parent.Account = new Account()
            {
                Email = cs.GenRandomEmail(),
                Password = "123456"
            };

        }

        public Parent parent { get; set; }
        public HttpPostedFileBase file {get;set;}
    }
}