using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [StringLength(10, ErrorMessage = "Mã sinh viên không được vượt quá 10 ký tự")]
        public string MaSV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ và tên không được vượt quá 100 ký tự")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh không được để trống")]
        public DateTime NgaySinh { get; set; }

        [Required(ErrorMessage = "Giới tính không được để trống")]
        public string GioiTinh { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Điện thoại không được để trống")]
        [RegularExpression(
            @"^0\d{9,10}$",
            ErrorMessage = "Số điện thoại phải bắt đầu bằng 0 và có 10 hoặc 11 số")]
        public string DienThoai { get; set; } = string.Empty;

        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10")]
        public double Diem { get; set; }

        [Required(ErrorMessage = "Trạng thái không được để trống")]
        public string TrangThai { get; set; } = string.Empty;

        // Quan hệ n - 1:
        // Một sinh viên thuộc về một lớp học
        [Required(ErrorMessage = "Sinh viên phải thuộc một lớp")]
        public LopHoc Lop { get; set; } = null!;

        // Kiểm tra các thuộc tính của đối tượng
        public bool IsValid(out List<ValidationResult> errors)
        {
            errors = new List<ValidationResult>();

            ValidationContext context = new ValidationContext(this);

            return Validator.TryValidateObject(
                this,
                context,
                errors,
                validateAllProperties: true
            );
        }
    }
}
