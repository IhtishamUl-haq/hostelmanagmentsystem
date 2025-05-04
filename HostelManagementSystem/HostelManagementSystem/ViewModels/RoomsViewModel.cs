using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HostelManagementSystem.ViewModels
{
    public class RoomsViewModel
    {
        public int roomId { get; set; }
        [Required(ErrorMessage ="This field is required")]
        public int roomNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> roomSize { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public double roomRent { get; set; }
        [Required(ErrorMessage = "This field is required")]
        
        public string isRoomAvailable { get; set; }
        public Nullable<int> hostelId { get; set; }
    }
}