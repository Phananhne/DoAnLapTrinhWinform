namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PhieuNhap")]
    public partial class PhieuNhap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuNhap()
        {
            ChiTietPhieuNhaps = new HashSet<ChiTietPhieuNhap>();
        }

        [Key]
        [StringLength(20)]
        public string MaPN { get; set; }

        public DateTime NgayNhap { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNCC { get; set; }

        [Required]
        [StringLength(20)]
        public string MaNV { get; set; }

        public int SoLuong { get; set; }

        public int DonGia { get; set; }

        [Required]
        [StringLength(20)]
        public string MaHang { get; set; }

        public virtual HangHoa HangHoa { get; set; }

        public virtual NhaCungCap NhaCungCap { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }
    }
}
