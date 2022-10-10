using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Entities;

namespace WebAPI.Models
{
    public class NhanVienViewModel
    {
        public int MaNV { get; set; }
        public string HoNV { get; set; }
        public string TenNV { get; set; }
        public bool Phai { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}