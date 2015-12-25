using KursDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursDB.Controllers
{
    public class RoomsController : Controller
    {

        public ActionResult Show_all_rooms(int? Price_from, int? Price_to, int? CountPeople_from, int? CountPeople_to, int? Area_from, int? Area_to)
        {
            List<RoomsWithServices> outList = GetRooms(Price_from, Price_to, CountPeople_from, CountPeople_to, Area_from, Area_to);
            if ((!(Price_from == null || Price_to == null || CountPeople_from == null || CountPeople_to == null || Area_from == null || Area_to == null)))
            {
                ViewBag.Price_from = Price_from;
                ViewBag.Price_to = Price_to;
                ViewBag.CoP_from = CountPeople_from;
                ViewBag.CoP_to = CountPeople_to;
                ViewBag.Area_from = Area_from;
                ViewBag.Area_to  = Area_to;
            }
            else
            {
                using (var db = new KursDBEntities())
                {
                    var price_to = db.ToR.Max(x => x.Price);
                    var price_from = db.ToR.Min(x => x.Price);
                    var CoP_from = db.ToR.Min(x => x.CoP);
                    var CoP_to = db.ToR.Max(x => x.CoP);
                    var area_to = db.ToR.Max(x => x.Area);
                    var area_from = db.ToR.Min(x => x.Area);

                    ViewBag.Price_from = price_from;
                    ViewBag.Price_to = price_to;
                    ViewBag.CoP_from = CoP_from;
                    ViewBag.CoP_to = CoP_to;
                    ViewBag.Area_from = area_from;
                    ViewBag.Area_to = area_to;
                }
            }
            return View(outList);
        }

        public ActionResult AddRoom(int Number, string ToR)
        {
            using (var db = new KursDBEntities())
            {
                db.Add_Room(Number, ToR);
                db.SaveChanges();
            }
            return RedirectToAction("Show_all_rooms");
        }

        public ActionResult Delete_Room(int Room_ID)
        {
            using (var db = new KursDBEntities())
            {
                db.Delete_Room(Room_ID);
                db.SaveChanges();
            }
            return RedirectToAction("Show_all_rooms");
        }

        private static List<RoomsWithServices> GetRooms(int? Price_from = null, int? Price_to = null, int? CountPeople_from = null, int? CountPeople_to = null, int? Area_from = null, int? Area_to = null)
        {
            List<RoomsWithServices> outList = new List<RoomsWithServices>();
            using (var db = new KursDBEntities())
            {
                var rooms = db.Rooms_View.Where(x => true).ToList();
                if (!(Price_from == null || Price_to == null || CountPeople_from == null || CountPeople_to == null || Area_from == null || Area_to == null))
                {
                    rooms = rooms.Where(x => x.Area >= Area_from && x.Area <= Area_to && x.Price >= Price_from && x.Price <= Price_to && x.CoP >= CountPeople_from && x.CoP <= CountPeople_to).ToList();
                }
                foreach (var item in rooms)
                {
                    var sevices = db.ServiceShow.Where(x => x.ToR_ID == item.ToR_ID).ToList();
                    outList.Add(new RoomsWithServices
                    {
                        Title = item.Title,
                        Area = item.Area,
                        Number = item.Number,
                        CoP = item.CoP,
                        Room_ID = item.Room_ID,
                        Price = item.Price,
                        services = sevices
                    });
                }
                foreach (var item in outList)
                {
                    item.ToDoList();
                }
            }
            return outList;
        }

    

        // Reservation
        public ActionResult Reservation(String DateFrom, String DateTo, int Room_ID)
        {
            bool good = true;
            DateTime Date_To = new DateTime(), Date_From = new DateTime();
            try
            {
                String[] from = DateFrom.Split('-'), to = DateTo.Split('-');

                Date_From = new DateTime(Int32.Parse(from[2]), Int32.Parse(from[1]), Int32.Parse(from[0]));
                Date_To = new DateTime(Int32.Parse(to[2]), Int32.Parse(to[1]), Int32.Parse(to[0]));


                Date_To.AddDays(Int32.Parse(to[0]));
                Date_To.AddMonths(Int32.Parse(to[1]));
                Date_To.AddYears(Int32.Parse(to[2]));
            }
            catch (Exception)
            {
                good = false;
            }
            

            if (good)
            {
                using (var db = new KursDBEntities())
                {
                    try
                    {
                        //Check
                        var check = db.Reservation.Where(x => x.Room_ID == Room_ID && 
                            (
                            DateTime.Compare(Date_From, x.Date_From) >= 0 && DateTime.Compare(Date_From, x.Date_To) <= 0 
                                ||
                            DateTime.Compare(Date_To, x.Date_From) >= 0 && DateTime.Compare(Date_To, x.Date_To) <= 0)
                            ).ToList();
                        if (check.Count == 0)
                        {
                            //Reservationing
                            var res = new Reservation { User_ID = (int)Session["User_ID"], Room_ID = Room_ID, Date_From = DateTime.Parse(DateFrom), Date_To = DateTime.Parse(DateTo), Date = DateTime.Now.Date };
                            db.Reservation.Add(res);
                            db.SaveChanges();
                            var room = db.Rooms.Single(x => x.Room_ID == Room_ID);
                            ViewBag.OK = String.Format("Success. You reserved a room {0} from {1} to {2}.", room.Number, res.Date_From.ToShortDateString(), res.Date_To.Date.ToShortDateString());
                        }
                        else
                        {
                            ViewBag.Error = "Sorry. This room has already booked on the selected date! Try again.";
                        }

                    }
                    catch (Exception e)
                    {

                        String error = e.InnerException.InnerException.Message;
                        var mas = error.Split(new Char[] { '"' });
                        if (mas[1] == "CK_Reservation_1")
                        {
                            ViewBag.Error = "Date of reservation should not be earlier than today.";
                        }
                        else if (mas[1] == "CK_Reservation")
                        {
                            ViewBag.Error = "Date To must be more than Date From";
                        }
                        else
                        {
                            ViewBag.Error = error;
                        }
                    }

                }    
            }
            else
            {
                ViewBag.Error = "Enter all options correct.";
            }

            List<RoomsWithServices> outList = GetRooms();
            return View("Show_all_rooms", outList);
        }

        // Orders
        public ActionResult Orders()
        {
            List<Reserve_View> list;
            List<OrderModel> view = new List<OrderModel>();
            using(var db = new KursDBEntities())
            {
                try
                {
                    DateTime date = DateTime.Now;
                    int user = (int)Session["User_ID"];
                    list = db.Reserve_View.Where(x => x.User_ID == user).ToList();
                    list.Where(x => DateTime.Compare(x.Date_To, date) >= 0);
                    foreach (var item in list)
                    {
                        view.Add(new OrderModel { Number = item.Number, Date = item.Date.ToShortDateString(), Date_From = item.Date_From.ToShortDateString(), Date_To = item.Date_To.ToShortDateString() });
                    }
                }
                catch(Exception e)
                { }
                
            }
            return View(view);
        }

        public ActionResult Edit_Room(int Room_ID)
        {
            RoomsWithTypes outList = GetRoom(Room_ID);
            return View(outList);
        }

        private static RoomsWithTypes GetRoom(int Room_ID)
        {
            RoomsWithTypes outList = new RoomsWithTypes();
            using (var db = new KursDBEntities())
            {
                var Rooms = db.Room_View.Where(x => x.Room_ID == Room_ID);
                foreach (var item in Rooms)
                {
                    var types = db.ToRs_View.Where(x => x.ToR_ID != 8).ToList();
                    outList.Number = item.Number;
                    outList.Room_ID = item.Room_ID;
                    outList.AllTypes = types;
                }
          }
            return outList;
        }

        public ActionResult EditRoom(int Number, string typeTitle, int idRoom)
        {
            using (var db = new KursDBEntities())
            {
                db.UpdateRoom(idRoom, Number, typeTitle);
                db.SaveChanges();
            }
            return RedirectToAction("Show_all_rooms");
        }
    }
}
