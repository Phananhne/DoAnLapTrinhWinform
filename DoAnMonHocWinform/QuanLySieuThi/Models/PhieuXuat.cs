namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuXuat")]
    public partial class PhieuXuat
    {
        [Key]
        [StringLength(20)]
        public string MaPX { get; set; }

        public DateTime NgayXuat { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; }

        public int SoLuong { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
