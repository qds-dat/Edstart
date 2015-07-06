using Edstart.DTO;
using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace Edstart.Services
{
    public class ParentService
    {
        private DatabaseContext db = null;
        Result res = new Result();
        public ParentService() {
            db = new DatabaseContext();
        }
        public ParentService(DatabaseContext context)
        {
            db = context;
        }
        
        public Result GetParentInformationByEmail(string email)
        {

            try{
                var parentInfor = (from user in db.Accounts
                                     join borrower in db.Parents.Include(x=>x.Term)
                                     on user.ID equals borrower.UserId
                                     where user.Email.Equals(email)
                                     select borrower).FirstOrDefault();

                if (parentInfor == null)
                    return res.Fail("Cannot get borrower information");

                return res.Success(parentInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }

        }

        public Result GetParentInformationByParentId(int id)
        {
            try
            {
                var parentInfor = db.Parents.Where(x => x.ID == id).FirstOrDefault();

                if (parentInfor == null)
                    return res.Fail("Borrower'Id invalid");

                return res.Success(parentInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }

        }

        public Result ApproveEmail(EmailApprove model)
        {
            try
            {
                var result = GetParentInformationByEmail(model.Email);
                if (!result.State)
                    res.Fail("Cannot get parent information");

                var parent = (Parent)result.RetVal;

                if (parent.Status != eBorroweStatus.AwaitingApplication)
                    //&& parent.Status != eBorroweStatus.Applied)
                    res.Fail("Your email has been applied");

                if (parent.EmailCode != model.Code)
                    res.Fail("Your email code invalid");

                parent.Status = eBorroweStatus.Applied;
                parent.FundingDate = DateTime.Now;
                db.Entry<Parent>(parent).State = EntityState.Modified;
                db.SaveChanges();

                return res.Success(null);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }
        public Parent GetParentDashboard(int id,ParentDashboardFilter filter)
        {
            try
            {
                var parentInfor = db.Parents.Where(x => x.ID == id).FirstOrDefault();



                return parentInfor;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        
    }
}