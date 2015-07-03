using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public enum eRole
    {
        Admin,
        School,
        Investor,
        Parent
    }
    public enum eBorroweStatus
    {
        Pending,
        Approve,
        Funding,
        Complete
    }

    public enum eGender
    {
        Male,
        Female
    }

    public enum eTerm
    {
        Year3,
        Year4,
        Year5,
    }

    public enum eTitle
    {
        Mr,
        Mrs,
        Ms,
        Miss,
        Dr
    }

    public enum eLicenceState
    {
        QLD,
        NSW,
        ACT,
        VIC,
        TAS,
        SA,
        WA,
        NT
    }

    public enum eResidence
    {
        
        Renting,
        Mortgage,
        Living,
        WithParents,
        FreeHold,
        Boarding
    }

    public enum eEmploymentStatus
    {
        Full_time,
        Part_time,
        Casual,
        Self_employed,
        Retired,
        Student,
        Houseperson,
        Currently_unemployed
    }

    public enum ePaymentPeriod
    {
        Monthly,
        Fortnightly,
        Weedly
    }
    
    public enum eMartialStatus
    {
        Single,
        Defacto,
        Married
    }

    public enum eApplyingAs
    {
        Individual,
        Company,
        Trust,
        Superannuatio_Fund
    }

    public enum eGrade
    {
        Kindergarten,
        Perschool,
        Prep,
        Grade_1,
        Grade_2,
        Grade_3,
        Grade_4,
        Grade_5,
        Grade_6,
        Grade_7,
        Grade_8,
        Grade_9,
        Grade_10,
        Grade_11,
        Grade_12,
    }
    public enum eInvestmentStatus
    {
        Funding,
        False,
        Success
    }


    /* ------------------------------------------ */
    public enum LookupGroup
    {
        BorrowerStatusType = 0
    }

    public enum eLookup
    {
        Pending = LookupGroup.BorrowerStatusType,
        Approve,
        Funding,
        Complete
    }
}