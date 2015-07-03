using Edstart.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    [Table("Parent")]
    public class Parent
    {
        // constructor
        public Parent() {
            EmailCode = Guid.NewGuid();
            Status = eBorroweStatus.Pending;
            CreateDate = DateTime.Now;
            Rate = (decimal)Config.Rate;
            FundingDate = DateTime.Now;
        }

        /*  Parent Info ----------*/
        public int ID { get; set; }

        [ForeignKey("Account")]
        public int UserId { get; set; }
        public virtual Account Account { get; set; }
        public eTitle Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [MaxLength(50)]
        public string LicenceNumber { get; set; }

        [ForeignKey("LicenceState")]
        public int LicenceStateId { get; set; }
        public virtual LicenceState LicenceState { get; set; }

        [Required]
        [MaxLength(50)]
        public string PassportNumber { get; set; }

        public DateTime CreateDate { get; set; }

        public string Certificate { get; set; }
        /*  Student Info ----------*/

        [Required]
        [MaxLength(50)]
        public string StudentFirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string StudentLastName { get; set; }

        public eGender Gender { get; set; }
        public DateTime? StudentDateOfBirth { get; set; }
        /*  School Info ----------*/

        [Required]
        [ForeignKey("School")]
        public int SchoolId { get; set; }
        public virtual School School { get; set; }

        public string FirstYearInSchool { get; set; } // First year of enrolment in non-government school
        public string PreviousSchool { get; set; }


        [Required]
        [ForeignKey("Term")]
        public int TermId { get; set; }
        public virtual Term Term { get; set; }

        /*  Contact Info ----------*/
        [Required]
        [MaxLength(50)]
        public string HomePhone { get; set; }

        [Required]
        [MaxLength(50)]
        public string MobilePhone { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }
        /*  Residence -------------*/
        public eResidence Residence { get; set; }
        public int YearLive { get; set; } //How long have you lived here (years)
        public int MonthLive { get; set; } //How long have you lived here (months)
        /*  Kin -------------*/
        [MaxLength(50)]
        public string PreviousAddress { get; set; }

        [MaxLength(50)]
        public string KinName { get; set; }

        [MaxLength(50)]
        public string KinContactNumber { get; set; }

        [MaxLength(50)]
        public string Relationship { get; set; }
        /*  Job -------------*/
        [MaxLength(50)]
        public string Occupation { get; set; } // missing data

        [MaxLength(50)]
        public string EmployerName { get; set; }

        [MaxLength(50)]
        public string EmployerPhoneNumber { get; set; }
        public eEmploymentStatus EmploymentStatus { get; set; }

        public int YearWithEmployer { get; set; }
        public int MonthWithEmployer { get; set; }

        /* Asset and Income */

        public decimal AnnualEmploymentIncome { get; set; } // Annual employment income (before tax)
        public ePaymentPeriod PaymentPeriod { get; set; }
        public decimal OtherAnnualBusinessIncome { get; set; } // Other annual business income (before tax)
        public decimal AnnualBenefit { get; set; } // Annual benefits (before tax)
        public decimal AnnualRental_InvestmentIncome { get; set; } // Annual rental / investment income (before tax)
        public int NumberOfDependents { get; set; }
        public eMartialStatus MartialStatus { get; set; }
        public decimal RentCost { get; set; } // Rent costs (per month)
        public decimal HomeLoanCost { get; set; } // Home loan costs (per month)
        public decimal InsuranceCost { get; set; } // Insurance costs (per month)
        public decimal Personal_FamilyCost { get; set; } //Personal / family costs (per month)
        public decimal FoodBeverageCost { get; set; }
        public decimal TransportCost { get; set; } //Transport costs (per month)
        public decimal TotalCreditCardLimit { get; set; } // Total credit card limits (combined)
        public decimal TotalPersonalLoan_OverdraftLimit { get; set; } //Total personal loan / overdraft limits (combined)
        public decimal PersonalLoanRepayment { get; set; } // Personal loan repayments (per month)
        public decimal TotalAssets { get; set; } // Total value of assets
        public decimal TotalLiabilities { get; set; } // Total value of assets
        /* EmailCode and Status */
        public Guid EmailCode { get; set; }
        public eBorroweStatus Status { get; set; }
        /* Demo Loan */
        public virtual ICollection<Investment> Investments { get; set; }

        [Required]
        public decimal LoanAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal LoanWithRate { get; set; }
        public DateTime FundingDate { get; set; }


        /* Method */
        public Result Register(DatabaseContext db, HttpPostedFileBase file)
        {
            Result res = new Result();

            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // check email exist
                    if (db.Accounts.Any(x => x.Email == this.Account.Email))
                        return res.Fail("This Email has already exist");

                    // if have file's Certificate  , save file
                    if (file != null)
                    {
                        string extension = Path.GetExtension(file.FileName);
                        string fileName = "";
                        string path = "";
                        bool fileInvalid = true;
                        // check if file's exist . If not : save , else : generate another file name
                        do
                        {
                            // generate file's name by guid
                            fileName = Config.CertificateUrl + Guid.NewGuid().ToString() + extension;
                            path = HttpContext.Current.Server.MapPath("~");
                            path = path + fileName;
                            fileInvalid = File.Exists(path + fileName);
                        } while (fileInvalid);

                        file.SaveAs(path);
                        this.Certificate = fileName;
                    }

                    // create account login
                    this.Account.Role = eRole.Parent;
                    db.Accounts.Add(this.Account);
                    db.SaveChanges();
                    if (this.Account.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create account fail");
                    }

                    // create borrower information 
                    this.LoanWithRate = this.LoanAmount + (this.LoanAmount * 3 / 100);
                    this.UserId = Account.ID;
                    db.Parents.Add(this);
                    db.SaveChanges();

                    if (this.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create Borrower information fail");
                    }
                    // transaction commit 
                    transaction.Commit();
                    // send email approve for borrower

                    Email_Service es = new Email_Service();
                    es.BorrowerRegister(this.Account.Email, this.EmailCode.ToString());
                    es.Notification_Register(this.Account.Email, this.Account.Role);

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