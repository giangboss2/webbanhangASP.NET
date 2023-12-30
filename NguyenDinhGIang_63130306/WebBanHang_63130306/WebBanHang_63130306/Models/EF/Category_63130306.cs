using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace WebBanHang_63130306.Models.EF
{
    [Table("tb_Category")]
    public class Category_63130306 : CommonAbstract_63130306
    {
        public Category_63130306()
        {
            this.News = new HashSet<News_63130306>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không để trống")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        //[StringLength(150)]
        //public string TypeCode { get; set; }
        //public string Link { get; set; }
        public string Description { get; set; }

        [StringLength(150)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(150)]
        public string SeoKeywords { get; set; }
        public bool IsActive { get; set; }
        public int Position { get; set; }
        public ICollection<News_63130306> News { get; set; }
        public ICollection<Posts_63130306> Posts { get; set; }
    }
}