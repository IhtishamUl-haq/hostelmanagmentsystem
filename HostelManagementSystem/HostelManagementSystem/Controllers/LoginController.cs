using HostelManagementSystem.CustomFilter;
using HostelManagementSystem.Models;
using HostelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        DbHostelManagementSystemEntities _LoginDb = new DbHostelManagementSystemEntities();
        ExceptionHandleAttribute _storedExcepation = new ExceptionHandleAttribute();
        public ActionResult Login()
        {
            //List<tbuserRole> roleList = _LoginDb.tbuserRoles.ToList();

            //ViewBag.UserRoledat = new SelectList(roleList, "RoleId", "roleName");
            return View();

        }
        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            try
            {
                //var checkStudentRole = _LoginDb.tbUsers.Where(x => x.userEmailAddress == userLogin.userEmailAddress).FirstOrDefault();
                var findEmailAndPassword = _LoginDb.Users.Where(x => x.EmailAddress == userLogin.EmailAddress && x.Password == userLogin.Password).FirstOrDefault();
                //tbUser user = new tbUser
                //{
                //    userEmailAddress= userLogin.userEmailAddress,
                //    userPassword= userLogin.userPassword,
                //};
                if (ModelState.IsValid == true)
                {
                    if (findEmailAndPassword != null)
                    {
                        var getBookRoom = _LoginDb.RoombookingInfoes.Where(x => x.userId == findEmailAndPassword.userId).FirstOrDefault();
                        //student login
                        if (findEmailAndPassword.RoleId==1)
                        {
                            if (getBookRoom != null)
                            {
                                Session["StoredRoomId"] = getBookRoom.roomId;
                                Session["StoredHostelId"] = getBookRoom.hostelId;
                               // Session["Email"] = userLogin.userEmailAddress;
                               Session["GetUserId"]= findEmailAndPassword.userId;

                                return RedirectToAction("ListOfbokingRoom", "Payment");
                            }
                            else
                            {
                                //Session["Email"] = userLogin.userEmailAddress;
                                Session["GetUserId"] = findEmailAndPassword.userId;
                                return RedirectToAction("ViewHostel", "Student");
                            }

                        }
                        
                        else
                        {
                            //Session["Email"] = userLogin.userEmailAddress;
                            Session["GetUserId"] = findEmailAndPassword.userId;
                            return RedirectToAction("HostelList", "Hostel");
                        }

                    }
                    else
                    {
                        ViewBag.EmailAndPassword = "Password and Email not exist";
                        return View();
                    }
                }
                return View();
            }catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");

            }
            
        }
        //public ActionResult InchargeLogin()
        //{

        //    return View();

        //}
        //[HttpPost]
        //public ActionResult InchargeLogin(UserLogin userLogin)
        //{

        //    //var getFirstName = _LoginDb.tbUsers.Select(x => x.userFirstName).FirstOrDefault();
        //    //var getLastName = _LoginDb.tbUsers.Select(x => x.userSecondName).FirstOrDefault();
        //    //Session["FullName"] = getFirstName + " " + getLastName;
        //    try
        //    {
        //        var findEmailAndPassword = _LoginDb.tbUsers.Where(x => x.userEmailAddress == userLogin.userEmailAddress && x.userPassword == userLogin.userPassword).FirstOrDefault();

        //        if (ModelState.IsValid == true)
        //        {
        //            if (findEmailAndPassword != null)
        //            {
        //                var checkRoleIncharge = _LoginDb.tbUsers.Where(x => x.userEmailAddress == userLogin.userEmailAddress).FirstOrDefault();
        //                if (checkRoleIncharge.RoleId == 2)
        //                {
        //                    Session["Email"] = userLogin.userEmailAddress;


        //                    return RedirectToAction("HostelList", "Hostel");

        //                }
        //                else
        //                {
        //                    ViewBag.EmailNotIsStudent = "Password and Email not exist";
        //                    return View();
        //                }

        //            }
        //            else
        //            {
        //                ViewBag.EmailAndPassword = "Password and Email not exist";
        //                return View();
        //            }
        //        }
        //        return View();
        //    }catch(Exception ex)
        //    {
        //        return  View(ex);
        //    }
               
        //}
        public ActionResult Logout()
        {
            if (Session["GetUserId"]!= null)
            {
                Session.Abandon();
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Error", "Error");
        }
    }
}