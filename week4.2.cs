erpLopHoc.Clear();
        }

        private void butThem_Click(object sender, EventArgs e)
        {
            ClearErrorProviders();
            SinhVien sv = new SinhVien();
            sv.MaSV = txtMaSV.Text;
            sv.NgaySinh = dtpNgaySinh.Value;
            sv.HoTen = txtHoTen.Text;
            sv.GioiTinh = rdoNam.Checked ? "Nam" : "Nữ";
            sv.Email = txtEmail.Text;
            sv.SoDienThoai = txtDienThoai.Text;
            sv.MaLop = cboLop.SelectedValue.ToString();
            sv.Diem = (double)numDiem.Value;
            List<ValidationResult> errors = sv.IsInValid();
            if (errors.Count == 0)
            {
                svb.AddSinhVien(sv);
            }
            else
            {
                //hiển thị thông báo lỗi tương ứng với từng trường
                foreach (var error in errors)
                {
                    if (error.MemberNames != null && error.MemberNames.Any())
                    {
                        string fieldName = error.MemberNames.First();
                        switch (fieldName)
                        {
                            case "MaSV":
                                errPMaSV.SetError(txtMaSV, error.ErrorMessage);
                                break;
                            case "HoTen":
                                erpHoten.SetError(txtHoTen, error.ErrorMessage);
                                txtHoTen.Focus();
                                break;
                            case "Email":
                                erpEmail.SetError(txtEmail, error.ErrorMessage);
                                txtEmail.Focus();
                                break;
                            case "SoDienThoai":
                                erpDienThoai.SetError(txtDienThoai, error.ErrorMessage);
                                txtDienThoai.Focus();
                                break;
                            default:
                                MessageBox.Show(error.ErrorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show(error.ErrorMessage, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            string malop = cboLop.SelectedValue.ToString();

            sinhViens = svb.GetSinhVienByMaLop(malop);

            dgvSinhVien.DataSource = null;
            dgvSinhVien.DataSource = sinhViens;
        }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {
            errPMaSV.Clear();
        }

        private void butSua_Click(object sender, EventArgs e)
        {
            ClearErrorProviders();
        }
        private void butXoa_Click(object sender, EventArgs e)
        {
            ClearErrorProviders();
        }
    }
}
