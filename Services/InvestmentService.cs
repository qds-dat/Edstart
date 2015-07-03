using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Edstart.DTO;
using Edstart.Models;
using System.Data.Entity;
namespace Edstart.Services
{
    public class InvestmentService
    {
        private DatabaseContext db = null;
        Result res = new Result();
        public InvestmentService()
        {
            db = new DatabaseContext();
        }
        public InvestmentService(DatabaseContext context)
        {
            db = context;
        }

        public ICollection<LoanAuction> AuctionData(int InvestorId)
        {
            var ListParent = db.Parents
                .Where(x =>
                    x.Status != eBorroweStatus.Pending
                    && DateTime.Now < DbFunctions.AddDays(x.FundingDate, 60)
                   ).ToList();
            var RetVal = ListParent.Select(b => new LoanAuction(b, InvestorId)).ToList();

            return RetVal;
        }

        public LoanAuction AuctionDataByParentId(int ParentId, int InvestorId)
        {
            var parent = db.Parents
                .Where(x =>
                    x.ID == ParentId
                    && x.Status != eBorroweStatus.Pending
                    && DateTime.Now < DbFunctions.AddDays(x.FundingDate, 60)
                   ).FirstOrDefault();
            if (parent == null)
                return null;

            return new LoanAuction(parent, InvestorId);
        }

        public IEnumerable<object> GetInvestmentsByInvestorId(int InvestorId)
        {
            var RetVal = (from parent in db.Parents
                          from investment in db.Investments
                          where investment.ParentId == parent.ID && investment.InvestorId == InvestorId
                          select new
                          {
                              Name = parent.FirstName + " " + parent.LastName,
                              LoanAmount = parent.LoanWithRate,
                              Bid = investment.BidAmount,
                              Rate = investment.BidRate,
                              PercentFunding = (parent.Investments.Sum(x=>x.BidAmount) * 100 / parent.LoanWithRate),
                              Status = investment.Status.ToString(),
                          }).ToList();

            return RetVal;
        }

        public IEnumerable<object> GetFeeByInvestorId(int InvestorId)
        {
            var RetVal = (from parent in db.Parents
                          from investment in db.Investments
                          where investment.ParentId == parent.ID
                          && investment.InvestorId == InvestorId
                          && parent.Status == eBorroweStatus.Complete
                          select new
                          {
                              Name = parent.FirstName + " " + parent.LastName,
                              LoanAmount = parent.LoanWithRate,
                              Bid = investment.BidAmount,
                              Rate = investment.BidRate,
                              //InterestFee = (investment.BidAmount 
                              //     + (investment.BidAmount * investment.BidRate/100) ),
                              EdStartRate = Config.EdstartRate,
                              Fee = ((investment.BidAmount
                                   + (investment.BidAmount * investment.BidRate / 100)) - investment.BidAmount)
                                  * (decimal)Config.EdstartRate / 100,
                          }).ToList();

            return RetVal;
        }

        public IEnumerable<Investment> GetInvestorByParentId(int ParentId)
        {
            try
            {
                var investment = db.Investments.Where(x => x.ParentId == ParentId && x.Parent.Status == eBorroweStatus.Complete).ToList();
                return investment;
            }
            catch
            {
                return null;
            }

        }
    }
}