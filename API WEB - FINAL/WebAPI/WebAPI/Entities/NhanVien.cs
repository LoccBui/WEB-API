//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class NhanVien
    {
        public NhanVien()
        {
            this.HoaDons = new HashSet<HoaDon>();
        }
    
        public int MaNV { get; set; }
        public string HoNV { get; set; }
        public string TenNV { get; set; }
        public bool Phai { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
    
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
