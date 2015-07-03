using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class Term
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string KindTerm { get; set; }


    }
}