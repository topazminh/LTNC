//Sửa lớp SinhVienDAO

using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        private readonly DataHelper dataHelper = new DataHelper();

        // Chuyển một dòng dữ liệu thành đối tượng SinhVien
        private SinhVien MapSinhVien(DataRow dr)
        {
            return new SinhVien
            {
                MaSV = Convert.ToString(dr["MaSV"]) ?? "",
                HoTen = Convert.ToString(dr["HoTen"]) ?? "",
                GioiTinh = Convert.ToString(dr["GioiTinh"]) ?? "",
                NgaySinh = dr["NgaySinh"] == DBNull.Value
                    ? ""
                    : Convert.ToDateTime(dr["NgaySinh"])
                        .ToString("yyyy-MM-dd"),
                SoDienThoai = Convert.ToString(dr["SoDienThoai"]) ?? "",
                MaLop = Convert.ToString(dr["MaLop"]) ?? "",
                Diem = dr["Diem"] == DBNull.Value
                    ? 0
                    : Convert.ToDouble(dr["Diem"]),
                Email = Convert.ToString(dr["Email"]) ?? ""
            };
        }

        // Lấy toàn bộ sinh viên
        public List<SinhVien> GetAllSinhVien()
        {
            try
            {
                DataTable dt = dataHelper.GetDataTable(
                    "SELECT * FROM SinhVien");

                List<SinhVien> danhSach = new List<SinhVien>();

                foreach (DataRow dr in dt.Rows)
                {
                    danhSach.Add(MapSinhVien(dr));
                }

                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể lấy danh sách sinh viên.", ex);
            }
        }

        // Tìm sinh viên theo mã sinh viên
        public SinhVien GetSinhVienByMaSV(string maSV)
        {
            try
            {
                string sql = "SELECT * FROM SinhVien WHERE MaSV = @MaSV";

                DataTable dt = dataHelper.GetDataTable(
                    sql,
                    new SqlParameter("@MaSV", maSV));

                if (dt.Rows.Count == 0)
                    return null;

                return MapSinhVien(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw new Exception("Không thể tìm sinh viên.", ex);
            }
        }

        // Lấy danh sách sinh viên theo mã lớp
        public List<SinhVien> GetSinhViensByMaLop(string maLop)
        {
            try
            {
                string sql =
                    "SELECT * FROM SinhVien WHERE MaLop = @MaLop";

                DataTable dt = dataHelper.GetDataTable(
                    sql,
                    new SqlParameter("@MaLop", maLop));

                List<SinhVien> danhSach = new List<SinhVien>();

                foreach (DataRow dr in dt.Rows)
                {
                    danhSach.Add(MapSinhVien(dr));
                }

                return danhSach;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Không thể lấy danh sách sinh viên theo lớp.", ex);
            }
        }

        // Thêm sinh viên
        public void AddSinhVien(SinhVien sv)
        {
            try
            {
                string sql = @"
                    INSERT INTO SinhVien
                    (MaSV, MaLop, HoTen, GioiTinh, NgaySinh,
                     Diem, SoDienThoai, Email)
                    VALUES
                    (@MaSV, @MaLop, @HoTen, @GioiTinh, @NgaySinh,
                     @Diem, @SoDienThoai, @Email)";

                int result = dataHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@MaSV", sv.MaSV),
                    new SqlParameter("@MaLop", sv.MaLop),
                    new SqlParameter("@HoTen", sv.HoTen),
                    new SqlParameter("@GioiTinh", sv.GioiTinh),
                    new SqlParameter("@NgaySinh",
                        DateTime.Parse(sv.NgaySinh)),
                    new SqlParameter("@Diem", sv.Diem),
                    new SqlParameter("@SoDienThoai", sv.SoDienThoai),
                    new SqlParameter("@Email", sv.Email));

                if (result == 0)
                    throw new Exception("Không thêm được sinh viên.");
            }
            catch (Exception ex)
            {
                throw new Exception("Thêm sinh viên thất bại.", ex);
            }
        }

        // Cập nhật sinh viên
        public void UpdateSinhVien(SinhVien sv)
        {
            try
            {
                string sql = @"
                    UPDATE SinhVien
                    SET MaLop = @MaLop,
                        HoTen = @HoTen,
                        GioiTinh = @GioiTinh,
                        NgaySinh = @NgaySinh,
                        Diem = @Diem,
                        SoDienThoai = @SoDienThoai,
                        Email = @Email
                    WHERE MaSV = @MaSV";

                int result = dataHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@MaSV", sv.MaSV),
                    new SqlParameter("@MaLop", sv.MaLop),
                    new SqlParameter("@HoTen", sv.HoTen),
                    new SqlParameter("@GioiTinh", sv.GioiTinh),
                    new SqlParameter("@NgaySinh",
                        DateTime.Parse(sv.NgaySinh)),
                    new SqlParameter("@Diem", sv.Diem),
                    new SqlParameter("@SoDienThoai", sv.SoDienThoai),
                    new SqlParameter("@Email", sv.Email));

                if (result == 0)
                    throw new Exception(
                        "Không tìm thấy sinh viên cần cập nhật.");
            }
            catch (Exception ex)
            {
                throw new Exception("Cập nhật sinh viên thất bại.", ex);
            }
        }

        // Xóa sinh viên khỏi cơ sở dữ liệu
        public void DeleteSinhVien(string maSV)
        {
            try
            {
                string sql =
                    "DELETE FROM SinhVien WHERE MaSV = @MaSV";

                int result = dataHelper.ExecuteNonQuery(
                    sql,
                    new SqlParameter("@MaSV", maSV));

                if (result == 0)
                    throw new Exception(
                        "Không tìm thấy sinh viên cần xóa.");
            }
            catch (Exception ex)
            {
                throw new Exception("Xóa sinh viên thất bại.", ex);
            }
        }
    }
}
