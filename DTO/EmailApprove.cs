using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Edstart.DTO
{
    public class EmailApprove
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public Guid Code { get; set; }
    }
}