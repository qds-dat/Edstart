using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class Trustee
    {
        public int ID { get; set; }

        public string Trustee1 { get; set; }
        public string Trustee2 { get; set; }
        public string Trustee3 { get; set; }
    }
}