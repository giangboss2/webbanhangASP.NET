using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebBanHang_63130306.Models;
using WebBanHang_63130306.Models.EF;

namespace WebBanHang_63130306.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class News_63130306Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/News_63130306
        public ActionResult Index(string Searchtext, int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<News_63130306> items = db.News.OrderByDescending(x => x.Id);
            if (!string.IsNullOrEmpty(Searchtext))
            {
                items = items.Where(x => x.Alias.Contains(Searchtext) || x.Title.Contains(Searchtext));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News_63130306 model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 6;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang_63130306.Models.Common.Filter_63130306.FilterChar(model.Title);

                // Thêm một bản ghi mới thay vì cập nhật
                db.News.Add(model);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }



        public ActionResult Edit(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News_63130306 model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang_63130306.Models.Common.Filter_63130306.FilterChar(model.Title);

                // Kiểm tra và cập nhật đối tượng
                var existingEntity = db.News.Find(model.Id);
                if (existingEntity != null)
                {
                    db.Entry(existingEntity).CurrentValues.SetValues(model);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var itemIds = ids.Split(',');
                if (itemIds != null && itemIds.Any())
                {
                    foreach (var itemId in itemIds)
                    {
                        var obj = db.News.Find(Convert.ToInt32(itemId));
                        if (obj != null)
                        {
                            db.News.Remove(obj);
                            db.SaveChanges();
                        }
                    }
                    return Json(new { success = true });
                }
            }
            return Json(new { success = false });
        }
    }
}
