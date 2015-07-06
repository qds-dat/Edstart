using Edstart.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class Investment
    {
        public Investment()
        {
            LastBidDate = DateTime.Now;
        }

        public Int64 ID { get; set; }

        [ForeignKey("Investor")]
        public int InvestorId { get; set; }
        public virtual Investor Investor { get; set; }

        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public decimal BidRate { get; set; }
        public decimal BidAmount { get; set; }

        public DateTime LastBidDate { get; set; }

        public eInvestmentStatus Status { get; set; }


        private Email_Service es = new Email_Service();
        public Result Upsert(DatabaseContext db)
        {
            Result res = new Result();
            ParentService parentService = new ParentService(db);
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Get parent by parent's id
                    var resultParent = parentService.GetParentInformationByParentId(ParentId);
                    if (!resultParent.State)
                        return res.Fail(resultParent.Message);

                    Parent parent = (Parent)resultParent.RetVal;
                    // check loan complete
                    if (parent.Status == eBorroweStatus.Funded || parent.Status == eBorroweStatus.Fulfilled)
                        return res.Fail("This investment complete");

                    // get investor in investments for parent
                    var investment = parent.Investments.Where(x => x.InvestorId == InvestorId).FirstOrDefault(); 
                    // total loan amount
                    var loanWithRate = parent.LoanWithRate;
                    // calculator amount left
                    var amountLeft = loanWithRate 
                        - parent.Investments.Sum(x => x.BidAmount)
                        + (investment == null ? 0 : investment.BidAmount);

                    if (this.BidAmount > amountLeft)
                        return res.Fail("Bid amount so large");

                    if (investment == null)
                    {
                        Random ran = new Random();
                        this.BidRate = (decimal)ran.Next(500, 700) / 100;
                        this.LastBidDate = DateTime.Now;
                        this.Status = this.BidAmount == amountLeft ? eInvestmentStatus.Success : eInvestmentStatus.Funding;
                        db.Investments.Add(this);
                        db.SaveChanges();
                    }
                    else
                    {
                        if (this.BidAmount == 0)
                        {
                            db.Entry<Investment>(investment).State = EntityState.Deleted;
                            db.SaveChanges();
                        }
                        else
                        {
                            investment.LastBidDate = DateTime.Now;
                            investment.BidAmount = this.BidAmount;
                            investment.Status = this.BidAmount == amountLeft ? eInvestmentStatus.Success : eInvestmentStatus.Funding;
                            db.Entry<Investment>(investment).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        
                    }
                    /*Update borrower status*/
                    if (this.BidAmount == amountLeft)
                    {                       
                        parent.Status = eBorroweStatus.Fulfilled; // funded
                        parent.Investments.ToList().ForEach(a => a.Status = eInvestmentStatus.Success);

                        var listEmail = db.Investments.Where(x=>x.ParentId == parent.ID).Select(b => b.Investor.Account.Email).ToList();

                        es.Notification_InvesmentSuccess(parent.Account.Email, listEmail);
                    }
                    
                    db.SaveChanges();
                    transaction.Commit();
                    return res.Success(this);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return res.Fail(ex.Message);
                }
            }

        }
    }
}