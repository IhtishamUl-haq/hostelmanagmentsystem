using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class HostelViewModel
    {
        public int hostelId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        public string OwnerName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Phone]
        public string PhoneNo { get; set; }
        [Required]
        public int IsCreatedBy { get; set; }
    }
}