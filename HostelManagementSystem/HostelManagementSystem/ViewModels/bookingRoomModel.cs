using HostelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class bookingRoomModel
    {
        
        public Hostel bookingHostel { get; set; }
        public Room bookingRoom { get; set; }
        public User bookingUser { get; set; }
        public List<Payment> paymentroom { get; set; }
        public RoombookingInfo bookingRoombookingInfo { get; set; }
       public List<User> listRoomMates { get; set; }
    }
}