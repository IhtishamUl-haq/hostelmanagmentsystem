using HostelManagementSystem.Models;
using HostelManagementSystem.ViewModels;
using System;
using HostelManagementSystem.CustomFilter;
using System.Data.Entity;
using System.Linq;
using System.ServiceModel.Dispatcher;
 using System.Web.Mvc;
using System.Web.Routing;

namespace HostelManagementSystem.Controllers
{
    public class HostelController : Controller
    {
        DbHostelManagementSystemEntities _hostelDb = new DbHostelManagementSystemEntities();
        ExceptionHandleAttribute _storedExcepation=new ExceptionHandleAttribute();
        public ActionResult HostelList()
        {
            try
            {


                if (Session["Email"] != null)
                {
                    string userEmail = Session["Email"].ToString();
                    var getUser = _hostelDb.Users.Where(x => x.EmailAddress == userEmail).FirstOrDefault();
                    Session["GetUserId"] = getUser.userId;
                }

                if (Session["GetUserId"] == null)
                {
                    return RedirectToAction("Login", "Login");
                }
                else
                {


                    int pickTroughId = Convert.ToInt32(Session["GetUserId"]);
                    var pickHostal = _hostelDb.Hostels.Where(x => x.IsCreatedBy == pickTroughId && x.IsDeleted == false).ToList();
                    //TempData["InchargeImage"] = _hostelDb.tbUsers.Select(x=>x.userImage).FirstOrDefault();
                    if (pickHostal.Count > 0)
                    {
                        return View(pickHostal);
                    }
                    else
                    {
                        ViewBag.NoHostel = "You can't add any hostel.";
                        return View();
                    }


                }
                //var data = _hostelDb.tbHostels.Where(x => x.hostelIsCreatedBy == pickTroughEmail).FirstOrDefault();
            }catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(HostelViewModel hostelViewModel)
        {
            try
            {

                if ( Session["GetUserId"]== null)
                {
                    return RedirectToAction("Login", "Login");
                }
               hostelViewModel.IsCreatedBy = Convert.ToInt32( Session["GetUserId"]) ;

                Hostel addHostel = new Hostel
                {
                    OwnerName = hostelViewModel.OwnerName,
                    Name = hostelViewModel.Name,
                    Address = hostelViewModel.Address,
                    PhoneNo = hostelViewModel.PhoneNo,
                    IsCreatedBy = hostelViewModel.IsCreatedBy,
                    IsDeleted = false
                };
                //if (ModelState.ContainsKey("hostelId"))
                //{
                //    ModelState.Remove("hostelId");
                //}

                //if (ModelState.IsValid)
                //{

                _hostelDb.Hostels.Add(addHostel);
                int a = _hostelDb.SaveChanges();
                if (a > 0)
                {
                    TempData["hostelAdd"] = "New hostel is added.";
                    return RedirectToAction("HostelList", "Hostel");
                }
                else
                {
                    TempData["hostelNotCreate"] = "hostel is not created";
                    return RedirectToAction("HostelList", "Hostel");
                }
                //}
                //else
                //{
                //    TempData["hostelNotCreate"] = "hostel is not created";
                //    return RedirectToAction("HostelList", "Hostel");
                //}

            }
            catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }

        }
        public ActionResult EditHostel(int? id)
        {
            try
            {


                var getHostel = _hostelDb.Hostels.Where(x => x.hostelId == id).FirstOrDefault();
                HostelViewModel hostelReturn = new HostelViewModel
                {

                    Name = getHostel.Name,
                    OwnerName = getHostel.Name,
                    Address = getHostel.Address,
                    PhoneNo = getHostel.PhoneNo,
                };
                TempData["StoredHostelId"] = getHostel.hostelId;
                return View(hostelReturn);
            }
            catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }
        [HttpPost]
        public ActionResult EditHostel(HostelViewModel editHostel)
        {
            try
            {

                editHostel.IsCreatedBy = Convert.ToInt32( Session["GetUserId"]);
                Hostel hostelEditInDatabase = new Hostel
                {
                    hostelId = Convert.ToInt32(TempData["StoredHostelId"]),
                    Name = editHostel.Name,
                    OwnerName = editHostel.OwnerName,
                    Address = editHostel.Address,
                    PhoneNo = editHostel.PhoneNo,
                    IsCreatedBy = editHostel.IsCreatedBy,
                    IsDeleted=false
                    

                };

                _hostelDb.Entry(hostelEditInDatabase).State = EntityState.Modified;
                int a = _hostelDb.SaveChanges();
                if (a > 0)
                {
                    TempData["hostelEdit"]  = "Hostel edit successfully";
                    return RedirectToAction("HostelList");
                }
                else
                {
                    ViewBag.hostelNotEdit = "Their are some issue hostel edit";
                    return RedirectToAction("HostelList");
                }


            }
            catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }


        }

        //public ActionResult DeleteHostel(int? id)
        //{
        //    TempData["DeleteHostl"] = id;
        //    var hostelId = _hostelDb.tbHostels.Where(x => x.hostelId == id).FirstOrDefault();
        //    return View(hostelId);
        //}

        public ActionResult DeleteHostel(int? id)
        {
            try
            {


                /*  int id = id;*/ /*Convert.ToInt32(TempData["DeleteHostl"]);*/
                var deleteHostelData = _hostelDb.Hostels.Where(x => x.hostelId == id).FirstOrDefault();
                var checkedHostelHasRoom = _hostelDb.Rooms.Where(x => x.hostelId == id && x.IsDeleted == false).FirstOrDefault();
                if (checkedHostelHasRoom != null)
                {
                    TempData["HostelHaveRooms"] = "You can't delete hostel because hostel have rooms";
                    return RedirectToAction("HostelList");
                }
                _hostelDb.Entry(deleteHostelData).CurrentValues.SetValues(deleteHostelData.IsDeleted = true);
                _hostelDb.SaveChanges();
                return RedirectToAction("HostelList");
            }catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }

        //public ActionResult HostelDetails(int? id)
        //{
        //    var hostelDetails = _hostelDb.tbHostels.Where(_x => _x.hostelId == id).FirstOrDefault();
        //    return View(hostelDetails);
        //}

      
       
       
        public ActionResult RoomList(int? id)
        {
            try
            {


                Session["checkHostelID"] = id;
                //var checkYesNo= _hostelDb.tbRooms.Where(x=>x.isRoomAvilable==true).ToString();

                var RoomList = _hostelDb.Rooms.Where(x => x.hostelId == id && x.IsDeleted == false).ToList();

                if (RoomList.Count > 0)
                {
                    return View(RoomList);
                }
                else
                {
                    ViewBag.NoRoom = "No room found in hostel.";
                    return View();
                }
            }catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }

        }
        //public ActionResult AddRoom(int? id)
        //{
        //    Session["HostelIdForRoom"] = id;
        //    return View();


        //}
        [HttpPost]
        public ActionResult AddRoom(RoomsViewModel addRoom)
        {
            try
            {
                int hostelId = Convert.ToInt32(Session["checkHostelID"]);
                addRoom.hostelId = hostelId;
                var checkRoomNo = _hostelDb.Rooms.Where(x => x.Number == addRoom.roomNumber && x.hostelId == hostelId).FirstOrDefault();
                if (checkRoomNo != null)
                {
                    
                    TempData["RoomAvailability "]= "The room number already Exist";

                    return RedirectToAction("RoomList", new RouteValueDictionary(new { Controller = "Hostel", Action = "RoomList", id = hostelId }));
                }
                //else
                //{
                    addRoom.isRoomAvailable = "Yes";

                    Room room = new Room
                    {
                        Number = addRoom.roomNumber,
                        Size = addRoom.roomSize,
                        Rent = addRoom.roomRent,
                        IsRoomAvailable = addRoom.isRoomAvailable,
                        hostelId = addRoom.hostelId,
                        NoBooking = 0,
                        IsDeleted = false
                    };
                    //var checRoomNo=_hostelDb.tbRooms.Where(x=>x.roomNumber==room.roomNumber).FirstOrDefault();

                    //room.hostelId = hostelid;
                    _hostelDb.Rooms.Add(room);
                    int a = _hostelDb.SaveChanges();
                    if (a > 0)
                    {
                        TempData["RoomAdd"] = "Room Added";
                        return RedirectToAction("RoomList", new RouteValueDictionary(
                         new { controller = "Hostel", action = "RoomList", Id = hostelId }));
                    }
                    else
                    {

                        return RedirectToAction("Error", "Error");
                    }
                //}
                
               
            }
            catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }


        }
        
        public ActionResult RoomEdit(int? id)
        {
            try
            {
                var roomEdit = _hostelDb.Rooms.Where(x => x.roomId == id).FirstOrDefault();
                TempData["RoomId"] = roomEdit.roomId;
                TempData["GetNoBooking"] = roomEdit.NoBooking;
                //TempData["HostelId"] = roomEdit.hostelId;
                RoomsViewModel getRoomViewModel = new RoomsViewModel
                {
                    roomNumber = roomEdit.Number,
                    roomRent = roomEdit.Rent,
                    roomSize = roomEdit.Size,
                    hostelId = roomEdit.hostelId,
                    
                };
                return View(getRoomViewModel);
            }
           catch(Exception ex)
            {
               
                   _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
        }

         

        [HttpPost]
        public ActionResult RoomEdit(RoomsViewModel editRoom)
        {
            try
            {


                int Id = Convert.ToInt32(Session["checkHostelID"]);
                int checkRoomId = Convert.ToInt32(TempData["RoomId"]);
                editRoom.hostelId = Id;
                editRoom.isRoomAvailable = "Yes";

                int getNoBooking = Convert.ToInt32(TempData["GetNoBooking"]);
                Room room = new Room
                {
                    roomId = checkRoomId,
                    Number = editRoom.roomNumber,
                    Size = editRoom.roomSize,
                    Rent = editRoom.roomRent,
                    IsRoomAvailable = editRoom.isRoomAvailable,
                    hostelId = editRoom.hostelId,
                    NoBooking = getNoBooking,
                    IsDeleted=false

                };

                _hostelDb.Entry(room).State = EntityState.Modified;
                int checkRoomEdit = _hostelDb.SaveChanges();
                if (checkRoomEdit > 0)
                {
                    TempData["RoomEdit"] = "Room Edit successfully";
                    return RedirectToAction("RoomList", new RouteValueDictionary(new { Controller = "Hostel", Action = "RoomList", id = Id }));
                }
                else
                {
                    ViewBag.RoomNotEdit = "Room not Edit";
                    return View();
                }


            }
            catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }

        }

        //public ActionResult DeleteRoom(int? id)
        //{
        //    TempData["deleteRoomId"] = id;
        //    var roomId = _hostelDb.tbRooms.Where(x => x.roomId == id).FirstOrDefault();
        //    return View(roomId);

        //}
        
        public ActionResult DeleteRoom(int? id)
        {
            try
            {
                //int Id = Convert.ToInt32(TempData["deleteRoomId"]);
                var deleteRoom = _hostelDb.Rooms.Where(x => x.roomId == id).FirstOrDefault();

               
                var checkRoomBookOrNot=_hostelDb.RoombookingInfoes.Where(x => x.roomId == id).FirstOrDefault();
                int hostelId = Convert.ToInt32(Session["checkHostelID"]);
                if (checkRoomBookOrNot!=null)
                {
                    TempData["RoomBooked"] = "You can't delete room because this room is booked";
                    return RedirectToAction("RoomList", new RouteValueDictionary(new { Controller = "Hostel", Action = "RoomList", id = hostelId }));
                }
                else
                {
                   
                    _hostelDb.Entry(deleteRoom).CurrentValues.SetValues(deleteRoom.IsDeleted=true);
                    _hostelDb.SaveChanges();

                    return RedirectToAction("RoomList", new RouteValueDictionary(new { Controller = "Hostel", Action = "RoomList", id = hostelId }));
                }
             
            }catch (Exception ex)
            {


                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
            
        }
        //public ActionResult RoomDetails(int id)
        //{
        //    var roomlDetails = _hostelDb.tbRooms.Where(x => x.roomId == id).FirstOrDefault();
        //    return View(roomlDetails);
        //}
    }
}