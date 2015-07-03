using Edstart.DTO;
using Edstart.Framework;
using Edstart.Models;
using Edstart.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Edstart.Controllers
{
    

    [Authorize(Roles = "Investor")]
    public class InvestorController : CustomInvestorController
    {
        private DatabaseContext db = new DatabaseContext();
        private AccountService accountService;
        private InvestorService investorService;
        private InvestmentService investmentService;

        public InvestorController() {
            accountService = new AccountService(db);
            investorService = new InvestorService(db);
            investmentService = new InvestmentService(db);
        }

        [AllowAnonymous]
        // GET: Borrowers/Register
        public ActionResult Register()
        {
            return View(new InvestorRegister());
        }

        [AllowAnonymous]
        [HttpPost]
        // POST: Borrowers/Register
        public ActionResult Register(InvestorRegister model,bool sample = false)
        {
            if(sample)
                return View(new InvestorRegister(true)); 

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var result = model.investor.Register(db, model.file);
                if (result.State)
                {
                    var result1 = accountService.Login(model.investor.Account);
                    if (!result1.State)
                        return RedirectToAction("Login", "Home");

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = result.Message;
                    return View(model);
                }
            }
        }

        // GET: Borrowers
        public ActionResult Index()
        {

            var result = investorService.GetInvestorInformationByInvestorId(SessionFactory.InvestorId);             
            Investor investor = (Investor)result.RetVal; 
            return View(investor);
        }

        public ActionResult Dashboard()
        {
            var model = investmentService.GetInvestmentsByInvestorId(SessionFactory.InvestorId);
            return View(model);
        }

        public ActionResult Fee()
        {
            var model = investmentService.GetFeeByInvestorId(SessionFactory.InvestorId);
            return View(model);
        }

    }
}