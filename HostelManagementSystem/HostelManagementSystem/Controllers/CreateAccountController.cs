using HostelManagementSystem.CustomFilter;
using HostelManagementSystem.Models;
using HostelManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HostelManagementSystem.Controllers
{
    public class CreateAccountController : Controller
    {
        DbHostelManagementSystemEntities _LoginDb = new DbHostelManagementSystemEntities();
        ExceptionHandleAttribute _storedExcepation = new ExceptionHandleAttribute();

        public ActionResult CreateAccount()

        {

            //List<UserRole> roleList = _LoginDb.UserRoles.ToList();

            //ViewBag.UserRole = new SelectList(roleList, "RoleId", "roleName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(UserModel user)
        {
            try
            {





                //for dropdown
                //List<tbuserRole> roleList = _LoginDb.tbuserRoles.ToList();
                //ViewBag.UserRole = new SelectList(roleList, "RoleId", "roleName");

                //for checking unique data
                var getUsers = _LoginDb.Users.ToList();
                var checkEmail = getUsers.Where(x => x.EmailAddress == user.EmailAddress).FirstOrDefault();
                var checkPhone = getUsers.Where(x => x.PhoneNo == user.PhoneNo).FirstOrDefault();
                var checkCNIC = getUsers.Where(x => x.CNIC == user.CNIC).FirstOrDefault();
                //if (ModelState.IsValid==true)
                //{
                if (checkEmail == null && checkPhone == null && checkCNIC == null)
                {

                    //for image upload and save student in database
                    string fileName = Path.GetFileNameWithoutExtension(user.Image.FileName);
                    string Extension = Path.GetExtension(user.Image.FileName).Trim();

                    fileName = fileName + Extension;
                    user.userImage = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    user.Image.SaveAs(fileName);

                    User saveUserDatabase = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        PhoneNo = user.PhoneNo,
                        CNIC = user.CNIC,
                        Address = user.Address,
                        Password = user.Password,
                        Image = user.userImage,
                        RoleId = user.RoleId

                    };

                    //string ConvertExtension = Extension.ToLower();
                    //if (ConvertExtension == ".jpg" || ConvertExtension == ".png" || ConvertExtension == ".jpeg")
                    //{
                    //if (ModelState.IsValid == true)
                    //{
                    _LoginDb.Users.Add(saveUserDatabase);
                    try
                    {

                        int a = _LoginDb.SaveChanges();


                        Session["Email"] = user.EmailAddress;

                        Session["FullName"] = user.FirstName + " " + user.LastName;

                        if (user.RoleId == 1)//this is the student Id
                        {

                            if (a > 0)
                            {
                                TempData["Message"] = "Account Created";
                                return RedirectToAction("ViewHostel", "Student");
                            }
                            else
                            {
                                TempData["Message"] = "Error";
                                return View();
                            }
                        }
                        else
                        {
                            //for image upload and save incharge in database
                            if (a > 0)
                            {
                                TempData["Message"] = "Account Created";
                                return RedirectToAction("HostelList", "Hostel");
                            }
                            else
                            {
                                TempData["Message"] = "Error";
                                return View();
                            }
                        }
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var excpationDatabase in e.EntityValidationErrors)
                        {
                            //ViewBag.ExceptionError = (excpationDatabase.Entry.Entity.GetType().Name, excpationDatabase.Entry.State);
                            foreach (var excpationData in excpationDatabase.ValidationErrors)
                            {
                                ViewBag.exceptionError = excpationData.ErrorMessage;
                            }
                        }
                        return View();

                    }

                    //    }

                    ////}
                    //else
                    //{
                    //    ViewBag.imageExtensions = "Image extension incorrect";
                    //    return View();
                    //}

                }
                else
                {
                    if (checkEmail != null)
                    {
                        ViewBag.checkEmail = "Already Exist";

                    }
                    if (checkPhone != null)
                    {
                        ViewBag.checkPhone = "Already Exist";

                    }
                    if (checkCNIC != null)
                    {

                        ViewBag.checkCNIC = "Already Exist";

                    }

                    return View();
                }



                ////}
                // return View();

            }catch (Exception ex)
            {

                _storedExcepation.OnException(ex);
                return RedirectToAction("Error", "Error");
            }
            }


    }
    
}