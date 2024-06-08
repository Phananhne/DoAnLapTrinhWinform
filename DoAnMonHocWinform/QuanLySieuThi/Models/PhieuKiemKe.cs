namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuKiemKe")]
    public partial class PhieuKiemKe
    {
        [Key]
        [StringLength(20)]
        public string MaPKK { get; set; }

        public DateTime NgayKK { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; }

        public int SoLuongTon { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
