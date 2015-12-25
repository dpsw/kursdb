using KursDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursDB.Controllers
{
    public class GuestbookController : Controller
    {
        // Show all notes in guestbook
        public ActionResult Show()
        {
            List<Guestbook_View> list;
            using(var db = new KursDBEntities())
            {
                list = db.Guestbook_View.Where(x => true).ToList();
            }
            return View(list);
        }

        // Add new note
        public ActionResult AddNote(string Comment)
        {
            using (var db = new KursDBEntities())
            {
                db.Guestbook.Add(new Guestbook { User_ID = (int)Session["User_ID"], Comment = Comment, Date = DateTime.Now });
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }

        // Delete note
        public ActionResult Delete_Note(int Note_ID)
        {
            using (var db = new KursDBEntities())
            {
                db.Delete_Comment(Note_ID);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }

        public ActionResult Edit_Note(int Note_ID)
        {
            using (var db = new KursDBEntities())
            {
                Guestbook item = db.Guestbook.Where<Guestbook>(x => x.Note_ID == Note_ID).FirstOrDefault<Guestbook>();
                return View(item);
            }
        }

        public ActionResult EditNote(int idNote, string Comment)
        {
            using (var db = new KursDBEntities())
            {
                db.UpdateNote(idNote, Comment);
                db.SaveChanges();
            }
            return RedirectToAction("Show");
        }
    }
}
