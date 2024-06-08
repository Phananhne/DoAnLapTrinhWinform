namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [StringLength(20)]
        public string MaHD { get; set; }

        public DateTime NgayBan { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNV { get; set; }

        public int SoLuong { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual ChiTietHoaDon ChiTietHoaDon { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
