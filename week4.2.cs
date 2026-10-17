using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace QuanLySinhVien.BUL
{
    public class SinhVienBUL
    {
        SinhVienDAL svd;
        public SinhVienBUL()
        {
            svd = new SinhVienDAL();
        }
        public SinhVien GetSinhVienByMaSV(string maSV)
        {
            return svd.GetSinhVienByMaSV(maSV);
        }
        public List<SinhVien> GetSinhVienByMaLop(string maLop)
        {
            return svd.GetSinhViensByMaLop(maLop);
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return svd.GetAllSinhVien();
        }
        public void DeleteSinhVien(string maSV)
        {
            try
            {
                svd.DeleteSinhVien(maSV);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa sinh viên: " + ex.Message);
            }
        }
        public void AddSinhVien(SinhVien sv)
        {
            //Kiểm tra sv hợp lệ
            List<ValidationResult> vdr = sv.IsInValid();

            if (vdr.Count>0) //Dữ liệu không hợp lệ
            {
                throw new Exception("Invalid student information");
            }
            else
            {
                //Chuẩn hóa dữ liệu
                sv.HoTen = sv.HoTen.Trim();
                Regex rg = new Regex("s+");
                sv.HoTen = rg.Replace(sv.HoTen, " ");
                svd.AddSinhVien(sv);

            }    
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            //Nhận thông tin sinh viên từ giao diện người dùng và kiểm tra tính hợp lệ
            var validationResults = sv.IsInValid();
            if (validationResults.Count > 0)
            {
                throw new Exception(string.Join("\n", validationResults.Select(vr => vr.ErrorMessage)));
            }
            else
            {
                //CHuẩn hóa dữ liệu
                sv.HoTen = sv.HoTen.Trim();
                Regex regex = new Regex(@"\s+");
                sv.HoTen = regex.Replace(sv.HoTen, " ");
                try
                {
                    svd.UpdateSinhVien(sv);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi cập nhật sinh viên: " + ex.Message);
                }
            }
        }
        public void AddSinhVien(SinhVien sv)
        {

            //Nhận thông tin sinh viên từ giao diện người dùng và kiểm tra tính hợp lệ
            var validationResults = sv.IsInValid();
            if (validationResults.Count > 0)
            {
                throw new Exception(string.Join("\n", validationResults.Select(vr => vr.ErrorMessage)));
            }
            else
            {
//CHuẩn hóa dữ liệu
                sv.HoTen = sv.HoTen.Trim();
                Regex regex = new Regex(@"\s+");
                sv.HoTen = regex.Replace(sv.HoTen, " ");
                try
                {
                    svd.AddSinhVien(sv);
                }
                catch (Exception ex)
                {
                    throw new Exception("Lỗi khi thêm sinh viên: " + ex.Message);
                }
            }
        }
    }
}
