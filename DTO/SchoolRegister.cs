using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Edstart.DTO
{
    public class SchoolRegister
    {
        CommonService cs = new CommonService();
        public SchoolRegister() {
            school = new School();
           
        }

        public SchoolRegister(bool state = true)
        {            
            school = cs.DummyData<School>();
            school.Account = new Account()
            {
                Email = cs.GenRandomEmail(),
                Password = "123456"
            };
        }

        public School school { get; set; }
        public HttpPostedFileBase file {get;set;}
    }
}