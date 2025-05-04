using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class PaymentViewModel
    {
        //public int paymentsId { get; set; }
        [Required]
        public Nullable<double> paymentsAmount { get; set; }
        [Required]
        public string paymentType { get; set; }
        //public Nullable<System.DateTime> PaymentDate { get; set; }
        //[Required]
        //public Nullable<int> userId { get; set; }
        //[Required]
        //public Nullable<int> roomId { get; set; }
        //[Required]
        //public Nullable<int> hostelId { get; set; }
    }
}