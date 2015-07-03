using Edstart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Edstart.Services
{
    public class RazorService
    {
        private DatabaseContext db = null;
        public RazorService()
        {
            db = new DatabaseContext();
        }
        public RazorService(DatabaseContext context)
        {
            db = context;
        }
        public List<SelectListItem> GetSchool_SelectListItem()
        {
            var SLI = db.Schools.ToArray()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.SchoolName
                                }).ToList();

            return SLI;
        }
        private void LoanTermSampleData() {
            List<Term> terms = new List<Term>();
            terms.Add(new Term() { KindTerm = "2 years" });
            terms.Add(new Term() { KindTerm = "3 years" });
            terms.Add(new Term() { KindTerm = "4 years" });
            terms.Add(new Term() { KindTerm = "5 years" });

            foreach(var term in terms){
                db.Terms.Add(term);
            }
            db.SaveChanges();
        }
        public List<SelectListItem> GetTerm_SelectListItem()
        {
            if(db.Terms.ToList().Count == 0){
                LoanTermSampleData();
            }
            var SLI = db.Terms.ToArray()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.KindTerm
                                }).ToList();

            return SLI;
        }

        private void LicenceStateSampleData()
        {
            List<LicenceState> lss = new List<LicenceState>();
            lss.Add(new LicenceState() { LicenceStateCode = "QLD" });
            lss.Add(new LicenceState() { LicenceStateCode = "NSW" });
            lss.Add(new LicenceState() { LicenceStateCode = "ACT" });
            lss.Add(new LicenceState() { LicenceStateCode = "VIC" });
            lss.Add(new LicenceState() { LicenceStateCode = "TAS" });
            lss.Add(new LicenceState() { LicenceStateCode = "SA" });
            lss.Add(new LicenceState() { LicenceStateCode = "WA" });
            lss.Add(new LicenceState() { LicenceStateCode = "NT" });

            foreach (var ls in lss)
            {
                db.LicenceStates.Add(ls);
            }
            db.SaveChanges();
        }

        public List<SelectListItem> GetLicenceState_SelectListItem()
        {
            if (db.LicenceStates.ToList().Count == 0)
            {
                LicenceStateSampleData();
            }
            var SLI = db.LicenceStates.ToArray()
                        .Select(x =>
                                new SelectListItem
                                {
                                    Value = x.ID.ToString(),
                                    Text = x.LicenceStateCode
                                }).ToList();

            return SLI;
        }
    }
}