using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang_63130306.Models;
using WebBanHang_63130306.Models.EF;

namespace WebBanHang_63130306.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult Partial_Subcrice()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Subscribe(Subscribe_63130306 req)
        {
            if (ModelState.IsValid)
            {
                db.Subscribes.Add(new Subscribe_63130306 { Email = req.Email, CreatedDate = DateTime.Now });
                db.SaveChanges();
                return Json(new { Success = true });
            }
            return View("Partial_Subcrice", req);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Refresh()
        {
            var item = new ThongKeModel_63130306();

            ViewBag.Visitors_online = HttpContext.Application["visitors_online"];
            // Kiểm tra xem các thuộc tính có tồn tại không trước khi sử dụng
            item.HomNay = HttpContext.Application["HomNay"]?.ToString() ?? "";
            item.HomQua = HttpContext.Application["HomQua"]?.ToString() ?? "";
            item.TuanNay = HttpContext.Application["TuanNay"]?.ToString() ?? "";
            item.TuanTruoc = HttpContext.Application["TuanTruoc"]?.ToString() ?? "";
            item.ThangNay = HttpContext.Application["ThangNay"]?.ToString() ?? "";
            item.ThangTruoc = HttpContext.Application["ThangTruoc"]?.ToString() ?? "";
            item.TatCa = HttpContext.Application["TatCa"]?.ToString() ?? "";

            return PartialView(item);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}