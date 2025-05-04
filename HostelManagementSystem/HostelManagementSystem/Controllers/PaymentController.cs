using HostelManagementSystem.CustomFilter;
using HostelManagementSystem.Models;
using HostelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace HostelManagementSystem.Controllers
{
    public class PaymentController : Controller
    {
        DbHostelManagementSystemEntities _dbPayment = new DbHostelManagementSystemEntities();

        ExceptionHandleAttribute _storedExcepation = new ExceptionHandleAttribute();
        public ActionResult AddPayment(int? roomId)
        {
            try
            {
                 
                int checkHostelId = Convert.ToInt32(Session["StoredHostelId"]);
                int studentId = Convert.ToInt32(Session["GetUserId"]);
                var getStudentBookedOrNot = _dbPayment.RoombookingInfoes.Where(x => x.userId == studentId && x.hostelId == checkHostelId && x.roomId == roomId).FirstOrDefault();
              
                if (getStudentBookedOrNot != null)
                {
                    var getPayment = _dbPayment.Payments.Where(x => x.userId == studentId && x.hostelId == checkHostelId && x.roomId == roomId).ToList().LastOrDefault();
                    DateTime paymentDate = Convert.ToDateTime(getPayment.PaymentDate);
                    DateTime date = DateTime.Now;
                    if (date.Year == paymentDate.Year && date.Month == paymentDate.Month)
                    {
                        TempData["NotAddPayment"] = "you can add payment because you already payed for this month";
                        return RedirectToAction("ListOfbokingRoom");
                    }
                    else
                    {
                        Session["StoredRoomId"] = roomId;
                        var checkRoom = _dbPayment.Rooms.Where(x => x.roomId == roomId).FirstOrDefault();
                        double roomPay = checkRoom.Rent;
                        Session["RoomPay"] = roomPay;
                        return View();
                    }

                }
                else
                {
                    Session["StoredRoomId"] = roomId;
                    var checkRoom = _dbPayment.Rooms.Where(x => x.roomId == roomId).FirstOrDefault();
                    double roomPay = checkRoom.Rent;
                    Session["RoomPay"] = roomPay;
                    return View();
                }
                   
               
            }catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
            
            
        }
        [HttpPost]
        public ActionResult AddPayment(PaymentViewModel addPayment)
        {
            try
            {

                addPayment.paymentsAmount = Convert.ToDouble(Session["RoomPay"]);
                int checkRoomId = Convert.ToInt32(Session["StoredRoomId"]);
                int checkHostelId = Convert.ToInt32(Session["StoredHostelId"]);
                int studentId = Convert.ToInt32( Session["GetUserId"]);
                var getStudentId = _dbPayment.Users.Where(x => x.userId == studentId).FirstOrDefault();
                //var getRoomRent = _dbPayment.Rooms.Where(x => x.roomId == checkRoomId).FirstOrDefault();
                Payment payment = new Payment
                {

                    Amount = addPayment.paymentsAmount,
                    Type = addPayment.paymentType,
                    hostelId = checkHostelId,
                    roomId = checkRoomId,
                    userId = getStudentId.userId,
                    PaymentDate = DateTime.Today,
                };
                //if (getRoomRent.roomRent == addPayment.paymentsAmount)
                //{
                   
                    _dbPayment.Payments.Add(payment);
                    //AddBookingInfo();
                    int checkPayment = _dbPayment.SaveChanges();
                    if (checkPayment > 0)
                    {
                        //var checkRoom = _dbPayment.tbRooms.Where(x => x.roomId == checkRoomId).FirstOrDefault();
                        //int updeteNoOfBooking = checkRoom.noOfBoking;
                        //updeteNoOfBooking += 1;
                        //tbRoom updateRoomAvailability = new tbRoom
                        //{
                        //    noOfBoking = updeteNoOfBooking,

                        ////};
                        //_dbPayment.Entry(updateRoomAvailability).State=EntityState.Modified;
                        //_dbPayment.SaveChanges();
                        var checkRoomAvailability = _dbPayment.Payments.Where(x => x.roomId == checkRoomId && x.hostelId == checkHostelId).ToList();
                        var checkRoom = _dbPayment.Rooms.Where(x => x.roomId == checkRoomId).First();
                         double roomPay = checkRoom.Rent;

                        if (checkRoomAvailability.Count==checkRoom.Size)
                         { 

                            _dbPayment.Entry(checkRoom).CurrentValues.SetValues(checkRoom.IsRoomAvailable = "No");
                            _dbPayment.SaveChanges();
                        }
                       
                        return RedirectToAction("BookingInfo");
                    }
                    else
                    {
                        ViewBag.PaymentNotAdd = "Their are some error in booking room";
                        return View();
                    }
                //}
                //else
                //{
                //    ViewBag.RoomRentNotequal = "Please pay sufficient amount.";
                //    return View();
                //}



            }
            catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }


        }
        public ActionResult BookingInfo()
        {
            try
            {
                int checkRoomId = Convert.ToInt32(Session["StoredRoomId"]);
                int checkHostelId = Convert.ToInt32(Session["StoredHostelId"]);
                int studentId = Convert.ToInt32(Session["GetUserId"]);


                var getStudentId = _dbPayment.Users.Where(x => x.userId == studentId).FirstOrDefault();
                int checkUserId = getStudentId.userId;
                var chekeStudentEnroll=_dbPayment.RoombookingInfoes.Where(x => x.userId == checkUserId).FirstOrDefault();
                if (chekeStudentEnroll == null)
                {
                    var getRoomForUpdateNoBooking = _dbPayment.Rooms.Where(x => x.roomId == checkRoomId).FirstOrDefault();
                    RoombookingInfo bookingInfo = new RoombookingInfo
                    {
                        bookingDate = DateTime.Today,
                        userId = getStudentId.userId,
                        roomId = checkRoomId,
                        hostelId = checkHostelId
                    };
                    _dbPayment.RoombookingInfoes.Add(bookingInfo);
                    _dbPayment.Entry(getRoomForUpdateNoBooking).CurrentValues.SetValues(getRoomForUpdateNoBooking.NoBooking+=1);
                    AddBookingInfo();
                    _dbPayment.SaveChanges();
                    return RedirectToAction("ListOfbokingRoom");
                }
                else
                {
                    return RedirectToAction("ListOfbokingRoom");
                }
                
            }catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
           
        }
        public ActionResult ListOfbokingRoom()
        {
            try
            {
                int checkRoomId = Convert.ToInt32(Session["StoredRoomId"]);
                int checkHostelId = Convert.ToInt32(Session["StoredHostelId"]);
                int studentId = Convert.ToInt32(Session["GetUserId"]);
                var getStudentId = _dbPayment.Users.Where(x => x.userId == studentId).FirstOrDefault();
                int checkUserId = getStudentId.userId;
              
                //      List<tbHostel> listHostel = new List<tbHostel>();
                //      List<tbRoom> listRoom = new List<tbRoom>();
                //List<tbPayment> listPayment = new List<tbPayment>();
                var bookingList = (from hostel in _dbPayment.Hostels
                                   join boookingInfo in _dbPayment.RoombookingInfoes on hostel.hostelId equals boookingInfo.hostelId
                                   where boookingInfo.hostelId == checkHostelId
                                   join rooms in _dbPayment.Rooms on boookingInfo.roomId equals rooms.roomId
                                   where boookingInfo.roomId == checkRoomId
                                   join student in _dbPayment.Users on boookingInfo.userId equals student.userId
                                   where boookingInfo.userId == checkUserId
                                   //join rooms in _dbPayment.tbRooms on payment.roomId equals checkRoomId
                                   //join user in _dbPayment.tbUsers on payment.userId equals checkUserId
                                   select new bookingRoomModel
                                   {
                                       bookingHostel = hostel,
                                       bookingRoom = rooms,
                                       bookingUser = student,
                                       bookingRoombookingInfo = boookingInfo

                                   }).FirstOrDefault();
                //var getRoomMates=_dbPayment.tbPayments.Where(x=>x.roomId==checkRoomId && x.hostelId==checkHostelId).ToList();
                var listgetRoomMates = (from roomMates in _dbPayment.Users
                                        join getBookedRoomsData in _dbPayment.RoombookingInfoes on roomMates.userId equals getBookedRoomsData.userId
    
                                        where getBookedRoomsData.roomId == checkRoomId && getBookedRoomsData.hostelId == checkHostelId && getBookedRoomsData .userId!=checkUserId   select roomMates
                                        ).ToList();

                var getTotalPayment = _dbPayment.Payments.Where(x => x.userId == checkUserId  && x.hostelId == checkHostelId).ToList();
                //ListgetRoomMates.Skip(1).ToList();
                bookingList.listRoomMates = listgetRoomMates;
                bookingList.paymentroom = getTotalPayment;
                return View(bookingList);
            }
            catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }
        public ActionResult LeaveRoom(int? BookingId)
        {
            try
            {
                int leveRoomId = Convert.ToInt32(Session["StoredRoomId"]);
                var LeaveRoom = _dbPayment.RoombookingInfoes.Where(x => x.bookingId == BookingId).FirstOrDefault();
                var getRoomForUpdateNoBooking = _dbPayment.Rooms.Where(x => x.roomId == leveRoomId).FirstOrDefault();
                _dbPayment.Entry(LeaveRoom).State = EntityState.Deleted;
                AddBookingInfo();
                var getRooms = _dbPayment.Rooms.Where(x => x.roomId == leveRoomId).FirstOrDefault();


                _dbPayment.Entry(getRooms).CurrentValues.SetValues(getRooms.IsRoomAvailable = "Yes");
                _dbPayment.Entry(getRoomForUpdateNoBooking).CurrentValues.SetValues(getRoomForUpdateNoBooking.NoBooking -= 1);
                _dbPayment.SaveChanges();
                return RedirectToAction("ViewHostel", "Student");
            }catch (Exception ex)
            {
                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }

        public void AddBookingInfo()
        { 

            int checkRoomId = Convert.ToInt32(Session["StoredRoomId"]);
            int checkHostelId = Convert.ToInt32(Session["StoredHostelId"]);
            int studentId = Convert.ToInt32(Session["GetUserId"]);
            var getStudentId = _dbPayment.Users.Where(x => x.userId == studentId).FirstOrDefault();
            int checkUserId = getStudentId.userId;
            LeaveAndBooked bookingInfo = new LeaveAndBooked
            {
                bookLeave = DateTime.Today,
                userId = getStudentId.userId,
                roomId = checkRoomId,
                hostelId = checkHostelId
            };
            _dbPayment.LeaveAndBookeds.Add(bookingInfo);
        }
    }

    

}
       