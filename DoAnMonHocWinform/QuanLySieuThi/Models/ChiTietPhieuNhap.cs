namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietPhieuNhap")]
    public partial class ChiTietPhieuNhap
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaPN { get; set; }

        [Key]
        [Column(Order = 1)]
        public double ChietKhau { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime NgayCapNhat { get; set; }

        public virtual PhieuNhap PhieuNhap { get; set; }
    }
}
