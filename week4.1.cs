using QuanLySinhVien.BUL;
using QuanLySinhVien.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Views
{
    public partial class frmQuanLySV : Form
    {
        SinhVienBUL svb;
        LopBus LopBus;
        List<SinhVien> sinhViens;


        List<LopHoc> lopHocs;

        void TogAdd(bool isAdding)
        {
            butThem.Enabled = isAdding;
            butXoa.Enabled = !isAdding;
            butSua.Enabled = !isAdding;
        }
        public frmQuanLySV()
        {
            svb = new SinhVienBUL();
            LopBus = new LopBus();

            lopHocs = LopBus.GetAllLopHoc();

            InitializeComponent();
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMaSV_Enter(object sender, EventArgs e)
        {
            txtMaSV.SelectAll();
        }

        private void frmQuanLySV_Load(object sender, EventArgs e)
        {
            cboLop.DataSource = lopHocs;
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
        }

        private void txtMaSV_Leave(object sender, EventArgs e)
        {
            //Tìm sinh viên
            var sv = svb.GetSinhVienByMaSV(txtMaSV.Text);

            if (sv == null)
            {
                txtHoTen.Text = "";
                txtEmail.Text = "";
                txtDienThoai.Text = "";
                rdoNam.Checked = true;
                TogAdd(true);

            }
            else
            {
                txtHoTen.Text = sv.HoTen;
                txtEmail.Text = sv.Email;
                txtDienThoai.Text = sv.SoDienThoai;
                if (sv.GioiTinh == "Nam")
                    rdoNam.Checked = true;
                else
                    rdoNu.Checked = true;
                cboLop.SelectedValue = sv.MaLop;
                numDiem.Value = (decimal)sv.Diem;
                TogAdd(false);
            }
            //Kiểm tra xem sinh viên có tồn tại không
            //Quyết định xử lý
        }

        private void butDong_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?",
                "Thông báo", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtMaSV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                dtpNgaySinh.Focus();
            }
        }
        private void ClearErrorProviders()
        {
            errPMaSV.Clear();
            erpHoten.Clear();
            erpEmail.Clear();
            erpDienThoai.Clear();
