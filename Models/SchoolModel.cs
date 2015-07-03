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
    [Table("School")]
    public class School
    {
        public School()
        {

        }

        [Key]
        public int ID { get; set; }

        [ForeignKey("Account")]
        public int UserId { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        public eTitle Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(20)]
        public string OfficePhone { get; set; }

        [Required]
        [MaxLength(20)]
        public string MobilePhone { get; set; }

        [Required]
        [MaxLength(100)]
        public string SchoolName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }

        [Required]
        public int CurrentStudent { get; set; }
     
        public eGrade LowestLevelOffred { get; set; }        

        public eGrade HighestLevelOffred { get; set; }

        [Required]
        public decimal HighestAnnualFee { get; set; } // Current annual fee for highest year-level (including compulsory activity contribution fees)

        [Required]
        public eTitle PrincipalTitle { get; set; }

        [Required]
        public string PrincipalFirstName { get; set; }

        [Required]
        public string PrincipalLastName { get; set; }

        [Required]
        public int BSBNumber { get; set; }

        [Required]
        public int AccountNumber { get; set; }

        [Required]
        public string AccountName { get; set; }

        public string Certificate { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

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
                    this.Account.Role = eRole.School;
                    db.Accounts.Add(this.Account);
                    db.SaveChanges();
                    if (this.Account.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create account fail");
                    }

                    // create school information  
                    this.UserId = Account.ID;
                    db.Schools.Add(this);
                    db.SaveChanges();

                    if (this.ID == 0)
                    {
                        transaction.Rollback();
                        return res.Fail("Create School information fail");
                    }
                    // transaction commit 
                    transaction.Commit();
                    // send email congratulations for school
                    Email_Service es = new Email_Service();
                    es.SchoolRegister(this.Account.Email);
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