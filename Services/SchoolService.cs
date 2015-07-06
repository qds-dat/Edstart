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

        public Result GetSchoolDashboard(int SchoolId,SchoolDashboardFilter filter)
        {
            Result res = new Result();
            try
            {
                var parents = db.Schools.FirstOrDefault(x => x.ID == SchoolId).Parents.ToList();

                if (parents == null)
                    return res.Fail("Cannot get school information");

                if(filter.ParentName != null){
                    parents = parents.Where(x => (x.FirstName + " " + x.LastName).Contains(filter.ParentName)).ToList();
                }
                if (filter.StudentName != null)
                {
                    parents = parents.Where(x => (x.StudentFirstName + " " + x.StudentLastName).Contains(filter.StudentName)).ToList();
                }
                if (filter.Term > 0)
                {
                    parents = parents.Where(x => x.TermId == filter.Term).ToList();
                }
                if (filter.LoanStatus != null)
                {
                    parents = parents.Where(x => x.Status == filter.LoanStatus).ToList();
                }              
                return res.Success(parents);
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