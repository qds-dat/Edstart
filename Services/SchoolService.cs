using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edstart.DTO;
namespace Edstart.Services
{
    public class SchoolService
    {
        private DatabaseContext db = null;
        public SchoolService()
        {
            db = new DatabaseContext();
        }

        public SchoolService(DatabaseContext context)
        {
            db = context;
        }

        public Result GetSchoolInformationByEmail(string email)
        {
            Result res = new Result();
            try
            {
                var users = db.Accounts;
                var schools = db.Schools;
                var schoolInfor = (from user in users
                                   join school in schools
                                   on user.ID equals school.UserId
                                   where user.Email.Equals(email)
                                   select school).FirstOrDefault();

                if (schoolInfor == null)
                    return res.Fail("Cannot get school information");

                return res.Success(schoolInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

        public Result GetSchoolInformationBySchoolId(int SchoolId)
        {
            Result res = new Result();
            try
            {
                var schoolInfor = db.Schools.FirstOrDefault(x => x.ID == SchoolId);

                if (schoolInfor == null)
                    return res.Fail("Cannot get school information");

                return res.Success(schoolInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

        public Result GetParentBySchoolId(int SchoolId)
        {
            Result res = new Result();
            try
            {
                var schoolInfor = db.Schools.FirstOrDefault(x => x.ID == SchoolId);

                if (schoolInfor == null)
                    return res.Fail("Cannot get school information");

                return res.Success(schoolInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

    }
}