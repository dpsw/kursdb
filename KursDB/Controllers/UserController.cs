using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using KursDB.Models;
using System.Web.Security;

namespace KursDB.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Sign_up_show()
        {
            return View();
        }

        public ActionResult Sign_up(string Email, string Name, string Surname, string Mobile, string Password)
        {
            using (var db = new KursDBEntities())
            {
                var User = db.Users.SingleOrDefault(x => x.Email == Email);
                if (User != null)
                {
                    ViewBag.Error = "This E-mail already used! Try another E-mail.";
                    return View("Sign_up_show");
                }
                else
                {
                    var Info = db.User_Info.SingleOrDefault(x => x.Mobile == Mobile);
                    if (Info != null)
                    {
                        ViewBag.Error = "This Mobile number already used! Try another Mobile number.";
                        return View("Sign_up_show");
                    }
                    else
                    {
                        Users user = new Users { Email = Email };
                        user = db.Users.Add(user);
                        db.SaveChanges();
                        User_Info info = new User_Info { User_ID = user.User_ID, Name = Name, Surname = Surname, Mobile = Mobile, Password = Password, Users = user };
                        db.Entry(info).State = EntityState.Modified;
                        db.User_Info.Add(info);
                        db.SaveChanges();
                    }
                }
            }
            return Redirect("/Home/Index/");
        }

        public ActionResult Sign_in_show()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Sign_in(string Email, string Password)
        {
            bool result = false;
            using (var db = new KursDBEntities())
            {
                var User = db.Users.SingleOrDefault(x => x.Email == Email);
                if (User != null)
                {
                    var info = db.User_Info.SingleOrDefault(x => x.User_ID == User.User_ID && x.Password == Password);
                    if (info != null)
                    {
                        result = true;
                        FormsAuthentication.SetAuthCookie(info.User_ID.ToString(), true);
                        Session.Add("User_ID", info.User_ID);
                        Session.Add("Name", info.Name);
                        Session.Add("Surname", info.Surname);
                        if (User.isAdmin)
                        {
                            Session.Add("Admin", User.isAdmin);
                        }
                        db.UpdateEntries(info.User_ID);
                        db.SaveChanges();
                    }
                }

            }

            if (!result)
            {
                ViewBag.Error = "Incorrect email or password!";
            }

            if (result)
            {
                return RedirectToAction("Index", "Home");
            }
            return result ? View("~/Views/Home/Index.cshtml") : View("Sign_in_show");
        }

        public ActionResult Log_out()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Change_Password()
        {
            User_Info user;
            int userId = 0;
            try
            {
                int.TryParse(Session["User_ID"].ToString(), out userId);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
            using (var db = new KursDBEntities())
            {
                user = db.User_Info.Where(x => x.User_ID == userId).FirstOrDefault();
            }
            return View(user);
        }

        public ActionResult ChangePassword(string OldPassword, string NewPassword, string RepeatNewPassword)
        {
            using (var db = new KursDBEntities())
            {
                int ID = Int32.Parse(Session["User_ID"].ToString());
                 var user = db.User_Info.Where(x => x.User_ID == ID).FirstOrDefault();
                if (NewPassword == RepeatNewPassword && user.Password == OldPassword)
                {
                    db.UpdatePassword(NewPassword, (int)Session["User_ID"]);
                    db.SaveChanges();
                    ViewBag.OK = "Password has been changed!";
                }
                else if (OldPassword != null && NewPassword != null && RepeatNewPassword != null)
                {
                    ViewBag.Error = "Incorrect old password or new password is not concide!";
                }
            }
            return View();
        }
    }
}
