using Edstart.Framework;
using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edstart.Controllers
{
    [Authorize(Roles = "Investor")]
    public class AuctionController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private InvestorService investorService;
        private InvestmentService investmentService;
        public AuctionController()
        {
            investorService = new InvestorService(db);
            investmentService = new InvestmentService(db);
        }
        // GET: Auction
        public ActionResult Index()
        {
            string Email = HttpContext.User.Identity.Name;
            var result = investorService.GetInvestorInformationByEmail(Email);
            if (!result.State)
            {
                return RedirectToAction("Login", "Home");
            }
            Investor investor = (Investor)result.RetVal;
            var retVal = investmentService.AuctionData(investor.ID);
            return View(retVal);
        }

        // GET: Borrowers
        [HttpPost]
        public JsonResult BidUpsert(Investment investment)
        {
            investment.InvestorId = SessionFactory.InvestorId;

            var UpsertResult = investment.Upsert(db);
            if (!UpsertResult.State)
                return Json(new { status = false, message = UpsertResult.Message });

            if (investment.Status == eInvestmentStatus.Success)
                return Json(new { status = true, data = "null" });

            var InvesmentDataResult = investmentService.AuctionDataByParentId(investment.ParentId, SessionFactory.InvestorId);
            return Json(new { status = true, data = InvesmentDataResult });
        }


    }
}
