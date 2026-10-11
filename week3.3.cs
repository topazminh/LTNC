using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLySinhVien.Models
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [StringLength(10, ErrorMessage = "Mã lớp không được vượt quá 10 ký tự")]
        public string MaLop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        [StringLength(100, ErrorMessage = "Tên lớp không được vượt quá 100 ký tự")]
        public string TenLop { get; set; } = string.Empty;

        // Quan hệ 1 - n:
        // Một lớp học có nhiều sinh viên
        public List<SinhVien> DanhSachSinhVien { get; set; }
            = new List<SinhVien>();

        // Kiểm tra tính hợp lệ của đối tượng
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
