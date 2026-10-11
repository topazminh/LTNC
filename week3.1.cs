using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using QuanLySinhVien.Models;

namespace QuanLySinhVien
{
    public partial class FrmSinhVien : Form
    {
        // =====================================================
        // 1. DANH SÁCH DỮ LIỆU
        // =====================================================

        private List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        private List<LopHoc> danhSachLopHoc = new List<LopHoc>();


        // =====================================================
        // 2. KHỞI TẠO FORM
        // =====================================================

        public FrmSinhVien()
        {
            InitializeComponent();

            this.Load += FrmSinhVien_Load;
        }


        // =====================================================
        // 3. FORM LOAD
        // =====================================================

        private void FrmSinhVien_Load(object sender, EventArgs e)
        {
            TaoDuLieuLopHoc();
            TaoDuLieuSinhVien();

            LoadComboBox();
            LoadDataGridView();

            // Trạng thái nút khi mở form
            btnThem.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Đưa con trỏ vào mã sinh viên
            txtMaSV.Focus();
        }


        // =====================================================
        // 4. TẠO DỮ LIỆU LỚP HỌC
        // =====================================================

        private void TaoDuLieuLopHoc()
        {
            danhSachLopHoc = new List<LopHoc>
            {
                new LopHoc
                {
                    MaLop = "CNTT01",
                    TenLop = "Công nghệ thông tin 1"
                },

                new LopHoc
                {
                    MaLop = "CNTT02",
                    TenLop = "Công nghệ thông tin 2"
                },

                new LopHoc
                {
                    MaLop = "KTPM01",
                    TenLop = "Kỹ thuật phần mềm 1"
                },

                new LopHoc
                {
                    MaLop = "KTPM02",
                    TenLop = "Kỹ thuật phần mềm 2"
                }
            };
        }


        // =====================================================
        // 5. TẠO DỮ LIỆU SINH VIÊN
        // =====================================================

        private void TaoDuLieuSinhVien()
        {
            danhSachSinhVien = new List<SinhVien>();
        }


        // =====================================================
        // 6. BƯỚC 32
        // NẠP DỮ LIỆU CHO COMBOBOX VÀ DATAGRIDVIEW
        // =====================================================

        private void LoadComboBox()
        {
            // -------------------------------------------------
            // ComboBox Lớp học
            // -------------------------------------------------

            cboLopHoc.DataSource = null;
            cboLopHoc.DataSource = danhSachLopHoc;
            cboLopHoc.DisplayMember = "TenLop";
            cboLopHoc.ValueMember = "MaLop";


            // -------------------------------------------------
            // ComboBox Trạng thái
            // -------------------------------------------------

            cboTrangThai.Items.Clear();

            cboTrangThai.Items.Add("Đang học");
            cboTrangThai.Items.Add("Đã tốt nghiệp");
            cboTrangThai.Items.Add("Bảo lưu");

            cboTrangThai.SelectedIndex = 0;


            // -------------------------------------------------
            // ComboBox lọc lớp
            // LƯU Ý: tên đúng trong Designer là cboLocLop
            // -------------------------------------------------

            cboLopLoc.DataSource = null;

            List<LopHoc> danhSachLocLop =
                new List<LopHoc>();

            danhSachLocLop.Add(
                new LopHoc
                {
                    MaLop = "",
                    TenLop = "Tất cả"
                });

            danhSachLocLop.AddRange(danhSachLopHoc);

            cboLopLoc.DataSource = danhSachLocLop;
            cboLopLoc.DisplayMember = "TenLop";
            cboLopLoc.ValueMember = "MaLop";
        }


        // =====================================================
        // 7. LOAD DATAGRIDVIEW
        // =====================================================

        private void LoadDataGridView()
        {
            dataGridView1.DataSource = null;

            dataGridView1.DataSource = danhSachSinhVien
                .Select(sv => new
                {
                    MaSV = sv.MaSV,
                    HoTen = sv.HoTen,
                    NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy"),
                    GioiTinh = sv.GioiTinh,
                    Email = sv.Email,
                    DienThoai = sv.DienThoai,
                    Diem = sv.Diem,
                    Lop = sv.Lop != null ? sv.Lop.TenLop : "",
                    TrangThai = sv.TrangThai
                })
                .ToList();
        }
    }
}
