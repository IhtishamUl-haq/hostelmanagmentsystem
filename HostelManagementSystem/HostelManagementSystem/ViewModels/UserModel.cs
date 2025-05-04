using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class UserModel
    {
        
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required")]
        public string LastName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        public string EmailAddress { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Required")]
        [Phone]
        
        public string PhoneNo { get; set; }
        [Display(Name = "CNIC")]
        [Required(ErrorMessage = "Required")]
        public string CNIC { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Required")]
        public string Address { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Required")]
        public string Password { get; set; }
        [Display(Name = "Image")]
        [Required(ErrorMessage = "Required")]
        public string userImage { get; set; }

        [Required(ErrorMessage = "Required")]
        public Nullable<int> RoleId { get; set; }
        public HttpPostedFileBase Image { get; set; }

    }
}