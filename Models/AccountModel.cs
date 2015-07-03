using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Edstart.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        public int ID { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public eRole Role { get; set; }
       
    }
}