using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("name=EdstartData")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        //public DbSet<Borrower> Borrowers { get; set; }

        /* main model */
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Parent> Parents { get; set; }       
        public DbSet<School> Schools { get; set; }
        public DbSet<Investor> Investors { get; set; }
        //public DbSet<Loan> Loans { get; set; }
        /* data model */
        public DbSet<Term> Terms { get; set; }
        public DbSet<LicenceState> LicenceStates { get; set; }
        public DbSet<Trustee> Trustees { get; set; }
        public DbSet<Investment> Investments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<School>()
                .HasMany<Parent>(x => x.Parents)
                .WithRequired(s => s.School)
                .HasForeignKey(x => x.SchoolId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parent>()
                .HasMany<Investment>(x => x.Investments)
                .WithRequired(s => s.Parent)
                .HasForeignKey(x=>x.ParentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Parent>()
                .HasRequired(x => x.Account).WithMany()
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<School>()
                .HasRequired(x => x.Account).WithMany()
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Investor>()
                .HasRequired(x => x.Account).WithMany()
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Investor>()
                .HasRequired(x => x.Trustee).WithMany()
                .HasForeignKey(x => x.TrusteeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Investment>()
                .HasRequired(x => x.Parent).WithMany()
                .HasForeignKey(x => x.ParentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Investment>()
                .HasRequired(x => x.Investor).WithMany()
                .HasForeignKey(x => x.InvestorId)
                .WillCascadeOnDelete(false);
               
        }
        
    }
}