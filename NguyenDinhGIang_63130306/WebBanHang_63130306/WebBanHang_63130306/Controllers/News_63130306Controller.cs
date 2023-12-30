using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang_63130306.Models;

namespace WebBanHang_63130306.Controllers
{
    public class News_63130306Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News_63130306
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial_News_Home()
        {
            var items = db.News.Take(3).ToList();
            return PartialView(items);
        }
    }
}