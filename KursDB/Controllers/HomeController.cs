using KursDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KursDB.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page = 1)
        {
            List<PagingNews_Result> list;
            using(var db = new KursDBEntities())
            {
                int CountItemsOnPage = 2;
                double CountOfNews = db.News.Count();
                list = db.PagingNews(CountItemsOnPage, page).ToList();
                int PagesCount = (int)Math.Ceiling((CountOfNews / CountItemsOnPage));
                ViewBag.PagesCount = PagesCount;
            }
            return View(list);
        }

        // Add news
        public ActionResult AddNews(string Title, string News)
        {
            using (var db = new KursDBEntities())
            {
                db.News.Add(new News { User_ID = (int)Session["User_ID"], Title = Title, Description = News, Date = DateTime.Now });
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Delete news
        public ActionResult Delete_News(int News_ID)
        {
            using (var db = new KursDBEntities())
            {
                db.Delete_News(News_ID);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit_News(int News_ID)
        {
            using (var db = new KursDBEntities())
            {
                News item = db.News.Where<News>(x => x.News_ID == News_ID).FirstOrDefault<News>();
                return View(item);
            }
        }

        public ActionResult EditNews(int idNews, string Title, string Description)
        {
            using (var db = new KursDBEntities())
            {
                db.UpdateNews(idNews, Title, Description);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}