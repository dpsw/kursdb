using KursDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net.Mail;
using System.Net;

namespace KursDB.Controllers
{
    public class AdminController : Controller
    {
        // Show all services
        public ActionResult Services_show()
        {
            List<Services_View> list;
            using (var db = new KursDBEntities())
            {
                list = db.Services_View.Where(x => x.Title != "").ToList();
            }
            return View(list);
        }

        public ActionResult ChangeAccess(int id)
        {
            using(var db = new KursDBEntities())
            {
                var user = db.Users.Single(x => x.User_ID == id);
                user.isAdmin = !user.isAdmin;
                db.SaveChanges();
            }
            return RedirectToAction("Show_All_Users");
        }

        // Add new service
        public ActionResult AddService(string Title)
        {
            using (var db = new KursDBEntities())
            {
                db.Service.Add(new Service { Title = Title });
                db.SaveChanges();
            }
            return RedirectToAction("Services_show");
        }

        // Delete service
        public ActionResult Delete_Service(int Service_ID)
        {
            using (var db = new KursDBEntities())
            {
                db.Delete_Service(Service_ID);
                db.SaveChanges();
            }
            return RedirectToAction("Services_show");
        }

        // Show all types of rooms
        public ActionResult Type_of_rooms_show()
        {
            List<ToRWithServices> outList = GetToR();
            return View(outList);
        }

        private static List<ToRWithServices> GetToR()
        {
            List<ToRWithServices> outList = new List<ToRWithServices>();
            using (var db = new KursDBEntities())
            {
                var ToRs = db.ToR_View.Where(x => true);
                foreach (var item in ToRs)
                {
                    if (item.ToR_ID != 8)
                    {
                        var sevices = db.ServiceShow.Where(x => x.ToR_ID == item.ToR_ID).ToList();
                        outList.Add(new ToRWithServices
                        {
                            ToR_ID = item.ToR_ID,
                            Title = item.Title,
                            Area = item.Area,
                            CoP = item.CoP,
                            Price = item.Price,
                            services = sevices
                        });
                    }
                }

                var services = db.Services_View.Where(x => true).ToList();
                foreach (var item in outList)
                {
                    var tempService = services.ToList();
                    item.ToDoList(tempService);
                }
                

            }
            return outList;
        }

        // Add new type of room
        public ActionResult AddToR(string Title, int Price, int Area, int CoP)
        {
            using (var db = new KursDBEntities())
            {
                db.ToR.Add(new ToR { Title = Title, Price = Price, Area = Area, CoP = CoP });
                db.SaveChanges();
            }
            return RedirectToAction("Type_of_rooms_show");
        }

        // Delete type of room
        public ActionResult Delete_ToR(int ToR_ID)
        {
            using (var db = new KursDBEntities())
            {
                db.Delete_ToR(ToR_ID);
                db.SaveChanges();
            }
            return RedirectToAction("Type_of_rooms_show");
        }

        public ActionResult AddServiceToToR(string serviceTitle, int idToR)
        {
            using (var db = new KursDBEntities())
            {
                var service = db.Services_View.Single(x => x.Title == serviceTitle);
                db.AddServiceToToR(service.Service_ID, idToR);
                db.SaveChanges();

            }
            return RedirectToAction("Type_of_rooms_show");
        }

        public ActionResult DeleteServiceFromToR(string serviceTitle, int idToR)
        {
            using (var db = new KursDBEntities())
            {
                var service = db.Services_View.Single(x => x.Title == serviceTitle);
                db.DeleteServiceFromTOR(service.Service_ID, idToR);
                db.SaveChanges();

            }
            return RedirectToAction("Type_of_rooms_show");
        }

        public ActionResult Show_All_Users()
        {
            List<Users_View> list;
            using (var db = new KursDBEntities())
            {
                list = db.Users_View.Where(x => true).ToList();
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult Edit_Service(int? Service_ID)
        {
            Service service;
            using (var db = new KursDBEntities())
            {
                service = db.Service.Where(x => x.Service_ID == Service_ID).FirstOrDefault();
            }
            return View(service);
        }

        public ActionResult EditService(int idService, string Title)
        {
            using (var db = new KursDBEntities())
            {
                db.UpdateService(idService, Title);
                db.SaveChanges();
            }
            return RedirectToAction("Services_show");
        }

        [HttpGet]
        public ActionResult Edit_TOR(int? ToR_ID)
        {
            ToR tor;
            using (var db = new KursDBEntities())
            {
                tor = db.ToR.Where(x => x.ToR_ID == ToR_ID).FirstOrDefault();
            }
            return View(tor);
        }

        public ActionResult EditTOR(int idToR, string Title, int Price, int CoP, int Area)
        {
            using (var db = new KursDBEntities())
            {
                db.UpdateTOR(idToR, Title, Price, CoP, Area);
                db.SaveChanges();
            }
            return RedirectToAction("Type_of_rooms_show");
        }

        public ActionResult Reserve_Out()
        {
            return View();
        }

        public ActionResult ReserveOut(string DateFrom, string DateTo)
        {
            using (var db = new KursDBEntities())
            {
                db.AllReservation(DateTime.Parse(DateFrom), DateTime.Parse(DateTo));
                db.SaveChanges();

                try
                {
                    int id = Int32.Parse(Session["User_ID"].ToString());
                    var user = db.Users.Single(x => x.User_ID == id);

                    string resultFile = "D:\\resultOperation.txt", outputInf = "D:\\infAboutOpertation.txt";
                    var cmd = System.Diagnostics.Process.Start("cmd.exe", "/C bcp KursDB.dbo.AllReserveTable out " + resultFile + " -T -c >> " + outputInf);// вывод в файл
                    cmd.WaitForExit(1000 * 60 * 2);//Ждем завершения экспорта в файлы
                    SendMail("smtp.gmail.com", "pivan4243@gmail.com", "1234google1234", /*user.Email*/ "pivan4243@gmail.com", "KursDB", "В приложенных файлах находится запрашиваемая вами информация о статистике гостиницы", resultFile + ";" + outputInf);//Отправка результата тому, кто запрашивал экспер(статистику)
                }
                catch(Exception e)
                {
                    ViewBag.Error = "Failed to send.";
                }

                db.DeleteReserveTable(1);
                db.SaveChanges();
            }
            ViewBag.OK = "File saved.";
            return View("Reserve_Out");
        }

        public static void SendMail(string smtpServer, string from, string password, string mailto,
            string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                {
                    String[] atts = attachFile.Split(';');
                    foreach (var file in atts)
                    {
                        mail.Attachments.Add(new Attachment(file));
                    }
                }

                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

    }
}
