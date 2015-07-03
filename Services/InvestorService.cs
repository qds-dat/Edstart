using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Edstart.Services
{
    public class InvestorService
    {
        private DatabaseContext db = null;
        public InvestorService()
        {
            db = new DatabaseContext();
        }

        public InvestorService(DatabaseContext context)
        {
            db = context;
        }
        Result res = new Result();
        public Result GetInvestorInformationByEmail(string email)
        {
           
            try
            {
                var users = db.Accounts;
                var investors = db.Investors;
                var investorInfor = (from user in users
                                   join investor in investors
                                   on user.ID equals investor.UserId
                                   where user.Email.Equals(email)
                                   select investor).FirstOrDefault();

                if (investorInfor == null)
                    return res.Fail("Cannot get school information");

                return res.Success(investorInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

        public Result GetInvestorInformationByInvestorId(int id)
        {
            try
            {
                var investorInfor = (from investor in db.Investors
                                     where investor.ID.Equals(id)
                                     select investor).FirstOrDefault();

                if (investorInfor == null)
                    return res.Fail("Investor'sId invalid");

                return res.Success(investorInfor);
            }
            catch (Exception ex)
            {
                return res.Fail(ex.Message);
            }
        }

    }
}