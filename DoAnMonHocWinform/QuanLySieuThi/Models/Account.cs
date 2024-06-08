namespace QuanLySieuThi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Username { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(30)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string MaCV { get; set; }

        public virtual ChucVu ChucVu { get; set; }
    }
}
