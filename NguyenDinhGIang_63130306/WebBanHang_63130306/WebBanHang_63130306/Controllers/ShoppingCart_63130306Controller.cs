using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang_63130306.Models;
using WebBanHang_63130306.Models.EF;

namespace WebBanHang_63130306.Controllers
{
    public class ShoppingCart_63130306Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart_63130306
        public ActionResult Index()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }


        public ActionResult ShowCount()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Count = 0 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel_63130306 req)
        {
            var code = new { Success = false, Code = -1 };// Khởi tạo một đối tượng phản hồi mặc định với giá trị không thành công
            if (ModelState.IsValid)// Kiểm tra xem dữ liệu gửi lên có hợp lệ không
            {
                ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];// Lấy giỏ hàng từ phiên làm việc
                if (cart != null)// Kiểm tra xem giỏ hàng có tồn tại không  
                {
                    Order_63130306 order = new Order_63130306();// Tạo một đơn hàng mới
                                                                // Điền thông tin khách hàng vào đơn hàng từ dữ liệu nhận được
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    order.Email = req.Email;
                    // Thêm chi tiết từng mặt hàng vào đơn hàng
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail_63130306
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    // Tính tổng số tiền của đơn hàng
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    // Tạo một mã đơn hàng ngẫu nhiên
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    // Thêm đơn hàng vào cơ sở dữ liệu
                    db.Orders.Add(order);
                    db.SaveChanges();
                    // Làm trống giỏ hàng
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess"); // Chuyển hướng đến trang thông báo thành công
                }
            }
            return Json(code);
        }


        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };// Khởi tạo một đối tượng phản hồi mặc định
            var db = new ApplicationDbContext();// Tạo một đối tượng kết nối cơ sở dữ liệu
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);// Tìm kiếm sản phẩm theo ID
            if (checkProduct != null)// Kiểm tra xem sản phẩm có tồn tại không
            {
                ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"]; // Lấy giỏ hàng từ phiên làm việc
                if (cart == null)
                {
                    cart = new ShoppingCart_63130306();
                }
                ShoppingCartItem_63130306 item = new ShoppingCartItem_63130306 // Tạo một mặt hàng mới trong giỏ hàng
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity
                };
                // Lấy hình ảnh mặc định của sản phẩm (nếu có)
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                // Nếu có giá bán, sử dụng giá bán thay vì giá gốc
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                // Cập nhật đối tượng phản hồi thành công
                code = new { Success = true, msg = "Thêm sản phẩm vào giở hàng thành công!", code = 1, Count = cart.Items.Count };
            }
            return Json(code);// Trả về đối tượng phản hồi dưới dạng JSON
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, msg = "", code = 1, Count = cart.Items.Count };
                }
            }
            return Json(code);
        }



        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart_63130306 cart = (ShoppingCart_63130306)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }
    }
}