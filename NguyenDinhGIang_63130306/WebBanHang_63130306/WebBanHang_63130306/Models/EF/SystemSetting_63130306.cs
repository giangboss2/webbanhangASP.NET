﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang_63130306.Models.EF
{
    [Table("tb_SystemSetting")]
    public class SystemSetting_63130306
    {
        [Key]
        [StringLength(50)]
        public string SettingKey { get; set; }
        [StringLength(4000)]
        public string SettingValue { get; set; }
        [StringLength(4000)]
        public string SettingDescription { get; set; }
    }
}
