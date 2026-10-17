//LopHocDAO

using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopHocDAO
    {
        private readonly DataHelper dataHelper = new DataHelper();

        public List<LopHoc> GetAllLopHoc()
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaLop, TenLop FROM LopHoc");

                List<LopHoc> danhSach = new List<LopHoc>();

                foreach (DataRow dr in dt.Rows)
                {
                    LopHoc lop = new LopHoc
                    {
                        MaLop = Convert.ToString(dr["MaLop"]) ?? "",
                        TenLop = Convert.ToString(dr["TenLop"]) ?? ""
                    };

                    danhSach.Add(lop);
                }

                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể lấy danh sách lớp học.", ex);
            }
        }

        public LopHoc GetLopHocByMaLop(string maLop)
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));

                if (dt.Rows.Count == 0)
                    return null;

                return new LopHoc
                {
                    MaLop = Convert.ToString(dt.Rows[0]["MaLop"]) ?? "",
                    TenLop = Convert.ToString(dt.Rows[0]["TenLop"]) ?? ""
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể tìm lớp học.", ex);
            }
        }

        public void AddLopHoc(LopHoc lop)
        {
            try
            {
                int result = dataHelper.ExecuteNonQuery(
                    "INSERT INTO LopHoc (MaLop, TenLop) " +
                    "VALUES (@MaLop, @TenLop)",
                    new SqlParameter("@MaLop", lop.MaLop),
                    new SqlParameter("@TenLop", lop.TenLop));

                if (result == 0)
                    throw new Exception("Không thêm được lớp học.");
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm lớp học thất bại.", ex);
            }
        }

        public void UpdateLopHoc(LopHoc lop)
        {
            try
            {
                int result = dataHelper.ExecuteNonQuery(
                    "UPDATE LopHoc SET TenLop = @TenLop " +
                    "WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", lop.MaLop),
                    new SqlParameter("@TenLop", lop.TenLop));

                if (result == 0)
                    throw new Exception(
                        "Không tìm thấy lớp học cần cập nhật.");
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật lớp học thất bại.", ex);
            }
        }

        public void DeleteLopHoc(string maLop)
        {
            try
            {
                int result = dataHelper.ExecuteNonQuery(
                    "DELETE FROM LopHoc WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));

                if (result == 0)
                    throw new Exception("Không tìm thấy lớp học cần xóa.");
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa lớp học thất bại.", ex);
            }
        }
    }
}
