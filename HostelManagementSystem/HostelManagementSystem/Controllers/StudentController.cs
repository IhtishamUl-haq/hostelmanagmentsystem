using HostelManagementSystem.CustomFilter;
using HostelManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DbHostelManagementSystemEntities _StudentDb=new DbHostelManagementSystemEntities();
        ExceptionHandleAttribute _storedExcepation = new ExceptionHandleAttribute();
        public ActionResult ViewHostel()
        {
            try
            {


                var hostelList = _StudentDb.Hostels.Where(x => x.IsDeleted == false).ToList();
                //var studentImage=_StudentDb.tbUsers.Select(x=>x.userImage).FirstOrDefault();
                //TempData["StudentImage"]=studentImage;
                if (hostelList.Count > 0)
                {
                    return View(hostelList);
                }
                else
                {
                    ViewBag.Hostel = "No Hostel avilable";
                    return View();
                }
            }catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
            
        }
        public ActionResult ViewRooms(int? id)
        {
            try
            {


                var rooms = _StudentDb.Rooms.Where(x => x.hostelId == id && x.IsRoomAvailable == "Yes" && x.IsDeleted == false).ToList();
                Session["StoredHostelId"] = id;
                //int studentId = Convert.ToInt32(Session["GetUserId"]);
                //int roomid = Convert.ToInt32(Session["StoredRoomId"]);

                //var getNumberOfBooking=_StudentDb.tbRoombookingInfoes.Where(x=>x.hostelId==id && x.roomId==roomid).ToList();
                if (rooms.Count() > 0)
                {
                    return View(rooms);
                }
                else
                {
                    ViewBag.Rooms = "No Rooms in hostel";
                    return View();
                }
            }catch(Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
             
        }
    }
}