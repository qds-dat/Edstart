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
    [Table("Investor")]
    public class Investor
    {
        /* Investor information */
        [Key]
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

        /* Contact information */
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        [MaxLength(20)]
        public string HomePhone { get; set; }

        [Required]
        [MaxLength(20)]
        public string MobilePhone { get; set; }
        /* Personal Identification  */
        [Required]
        [MaxLength(50)]
        public string LicenceNumber { get; set; }

        [ForeignKey("LicenceState")]
        public int LicenceStateId { get; set; }
        public virtual LicenceState LicenceState { get; set; }

        [Required]
        [MaxLength(50)]
        public string PassportNumber { get; set; }

        public eApplyingAs ApplyingAs { get; set; }

        [Required]
        [MaxLength(50)]
        public string TaxFileNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string BSBNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ACN { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrustName { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrustABN { get; set; }

        [ForeignKey("Trustee")]
        public int TrusteeId { get; set; }
        public virtual Trustee Trustee { get; set; }

        [Required]
        [MaxLength(50)]
        public string SuperannuationFundName { get; set; }

        [Required]
        [MaxLength(50)]
        public string SuperannuationFundABN { get; set; }

        public string Certificate { get; set; }

        public Result Register(DatabaseContext db,HttpPostedFileBase file)
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
                    this.Account.Role = eRole.Investor;
                    db.Accounts.Add(this.Account);
                    db.SaveChanges();
                    if (this.Account.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create account fail");
                    }
                    // create trustee
                    db.Trustees.Add(this.Trustee);
                    db.SaveChanges();
                    if (this.Account.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create trustee fail");
                    }
                    // create investor information  
                    this.UserId = Account.ID;
                    this.TrusteeId = Trustee.ID;
                    db.Investors.Add(this);
                    db.SaveChanges();

                    if (this.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create Investor information fail");
                    }
                    transaction.Commit();
                    // send email congratulations for investor
                    Email_Service es = new Email_Service();
                    es.InvestorRegister(this.Account.Email);
                    es.Notification_Register(this.Account.Email,this.Account.Role);

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