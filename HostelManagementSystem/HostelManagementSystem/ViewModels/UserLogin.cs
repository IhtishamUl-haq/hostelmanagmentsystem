using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class UserLogin
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        public string EmailAddress { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
    }
}