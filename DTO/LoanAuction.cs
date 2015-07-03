using Edstart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edstart.DTO
{
    public class LoanAuction
    {
        public LoanAuction()
        {

        }

        public LoanAuction(Parent parent,int investorId)
        {
            InvestorId = investorId;
            /*Borrower information*/
            ParentId = parent.ID;
            BorrowerFullName = parent.FirstName + " " + parent.LastName;
            StudentName = parent.StudentFirstName + " " + parent.StudentLastName;
            ParentCertificate = parent.Certificate;
            /*School information*/
            SchoolName = parent.School.SchoolName;
            SchoolCertificate = parent.School.Certificate;
            /*Loan information*/
            LoanRate = parent.Rate;
            LoanWithRate = parent.LoanWithRate;
            TotalBid = parent.Investments.Count;
            TotalBidAmount = parent.Investments.Count == 0 ? 0 : parent.Investments.Sum(x => x.BidAmount);
            Term = parent.Term.KindTerm;
            DateStart = parent.FundingDate;
            BidRate = parent.Investments.Any(x => x.InvestorId == InvestorId) ? parent.Investments.FirstOrDefault(x => x.InvestorId == InvestorId).BidRate : 0;
            BidAmount = parent.Investments.Any(x => x.InvestorId == InvestorId) ? parent.Investments.FirstOrDefault(x => x.InvestorId == InvestorId).BidAmount : 0;
        }

        /*Borrower information*/
        public int ParentId { get; set; }
        public int InvestorId { get; set; }
        public string BorrowerFullName { get; set; }
        public string StudentName { get; set; }
        public string ParentCertificate { get; set; }
        /*School information*/
        public string SchoolName { get; set; }
        public string SchoolCertificate { get; set; }
        /*Loan information*/
        public decimal LoanRate { get; set; }

        public decimal LoanWithRate { get; set; }
        public string Term { get; set; }

        public DateTime DateStart { get; set; }
        public int DayLeft
        {
            get
            {
                return DateStart.AddDays(60).Subtract(DateTime.Now).Days;
            }
        }
        public int TotalBid { get; set; }
        public decimal TotalBidAmount { get; set; }
        public string PercentFunded
        {
            get
            {
                return ((TotalBidAmount * 100) / LoanWithRate).ToString("F");
            }
        }
        public string AmountLeft
        {
            get
            {
                return (LoanWithRate - TotalBidAmount).ToString("F");
            }
        }
        public decimal BidRate { get; set; }
        public decimal BidAmount { get; set; }
        // public Int64 LoanId { get; set; }
    }
}