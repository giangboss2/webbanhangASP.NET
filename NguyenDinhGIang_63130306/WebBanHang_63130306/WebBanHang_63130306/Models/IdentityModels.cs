using System.Data.Entity;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebBanHang_63130306.Models.EF;

namespace WebBanHang_63130306.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<ThongKe_63130306> ThongKes { get; set; }
        public DbSet<Category_63130306> Categories { get; set; }
        public DbSet<Adv_63130306> Advs { get; set; }
        public DbSet<Posts_63130306> Posts { get; set; }
        public DbSet<News_63130306> News { get; set; }
        public DbSet<SystemSetting_63130306> SystemSettings { get; set; }
        public DbSet<ProductCategory_63130306> ProductCategories { get; set; }
        public DbSet<Product_63130306> Products { get; set; }
        public DbSet<ProductImage_63130306> ProductImages { get; set; }
        public DbSet<Contact_63130306> Contacts { get; set; }
        public DbSet<Order_63130306> Orders { get; set; }
        public DbSet<OrderDetail_63130306> OrderDetails { get; set; }
        public DbSet<Subscribe_63130306> Subscribes { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}