using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    public class LicenceState
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string LicenceStateCode { get; set; }
        public string Description { get; set; }
    }
}