using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Entities;
using WebAPI.Models;

using System.Data;
using System.Data.Entity;

namespace WebAPI.Controllers
{
    public class NhanVienController : ApiController
    {
        //Get
        [Route("nhanvien")]
        [HttpGet]
        public IHttpActionResult GetAllNV()
        {
            IList<NhanVienViewModel> nv = null;

            using (var ctx = new QLBanHangDBEntities())
            {
                nv = ctx.NhanViens.Include("NhanVien")
                            .Select(s => new NhanVienViewModel()
                            {
                                MaNV = s.MaNV,
                                HoNV = s.HoNV,
                                TenNV = s.TenNV,
                                Phai = s.Phai,
                                DiaChi = s.DiaChi,
                                DienThoai = s.DienThoai,
                            }).ToList<NhanVienViewModel>();
            }

            if (nv.Count == 0)
            {
                return NotFound();
            }

            return Ok(nv);
        }

        //Get By Id
        [Route("nhanvien/{maNV}")]
        [HttpGet]
        public IHttpActionResult GetByMaNV(int maNV)
        {
            NhanVienViewModel nv = null;

            using (var ctx = new QLBanHangDBEntities())
            {
                nv = ctx.NhanViens.Include("maNV")
                    .Where(s => s.MaNV == maNV)
                    .Select(s => new NhanVienViewModel()
                    {
                        MaNV = s.MaNV,
                        HoNV = s.HoNV,  
                        TenNV = s.TenNV,
                        Phai = s.Phai,
                        DiaChi = s.DiaChi,
                        DienThoai = s.DienThoai,
                    }).FirstOrDefault<NhanVienViewModel>();
            }

            if (nv == null)
            {
                return NotFound();
            }

            return Ok(nv);
        }

        //POST
        [Route("nhanvien")]
        [HttpPost]
        public IHttpActionResult PostNewNV(NhanVienViewModel nv)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            using (var ctx = new QLBanHangDBEntities())
            {
                ctx.NhanViens.Add(new NhanVien()
                {
                    HoNV = nv.HoNV,
                    TenNV = nv.TenNV,
                    Phai = nv.Phai,
                    DiaChi = nv.DiaChi,
                    DienThoai = nv.DienThoai,
                });

                ctx.SaveChanges();
            }

            return Ok();
        }



        //Put
        [Route("nhanvien")]
        [HttpPut]
        public IHttpActionResult Put(NhanVienViewModel nv)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new QLBanHangDBEntities())
            {
                var existingNV = ctx.NhanViens.Where(s => s.MaNV == nv.MaNV)
                                                        .FirstOrDefault<NhanVien>();

                if (existingNV != null)
                {
                    existingNV.MaNV = nv.MaNV;
                    existingNV.HoNV = nv.HoNV;
                    existingNV.TenNV = nv.TenNV;
                    existingNV.Phai = nv.Phai;
                    existingNV.DiaChi = nv.DiaChi;
                    existingNV.DienThoai = nv.DienThoai;

                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }



        //Delete
        [Route("nhanvien/{maNV}")]
        [HttpDelete]
        public IHttpActionResult Put(int maNV)
        {
            if (maNV <= 0)
                return BadRequest("Not a valid student manV");

            using (var ctx = new QLBanHangDBEntities())
            {
                var nv = ctx.NhanViens
                    .Where(s => s.MaNV == maNV)
                    .FirstOrDefault();

                ctx.Entry(nv).State = EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }


    }
}
