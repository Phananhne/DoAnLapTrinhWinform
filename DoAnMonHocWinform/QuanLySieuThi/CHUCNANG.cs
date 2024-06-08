using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace QuanLySieuThi
{
    public partial class CHUCNANG : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        private string tencv_nhan;
        private string machucvu_nhan;
        public CHUCNANG(string macv, string tencv) : this()
        {

            tencv_nhan = tencv;
            machucvu_nhan = macv;
            txtHTUser.Text = macv;
            txtHTCV.Text = tencv;
            if (tencv_nhan.Equals("NhanVien"))
            {
                btnNhanVien.Enabled = false;
                btnNCC.Enabled = false;
                btnTaiKhoan.Enabled = false;
            }

            
            
        }
        public CHUCNANG()
        {
            InitializeComponent();
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
                NHAPHANG NH = new NHAPHANG();
                
                NH.ShowDialog();          
        }

        private void btnXuatHang_Click(object sender, EventArgs e)
        {
            XUATHANG XH = new XUATHANG();
           
            XH.ShowDialog();
        }

        private void btnCTNH_Click(object sender, EventArgs e)
        {
            
            CTNH CTNH = new CTNH();
           
            CTNH.ShowDialog();
        }

        private void btnHangHoa_Click(object sender, EventArgs e)
        {
            HANGHOA HH = new HANGHOA();
           
            HH.ShowDialog();
            
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            HOADON HD = new HOADON();
           
            HD.ShowDialog();          
        }

        private void btnCTHD_Click(object sender, EventArgs e)
        {
            CTHD CTHD = new CTHD();
          
            CTHD.ShowDialog();
        }

        private void btnKiemKe_Click(object sender, EventArgs e)
        {
            KIEMKE KK = new KIEMKE();
            
            KK.ShowDialog();
        }

        private void btnNCC_Click(object sender, EventArgs e)
        {
            NHACUNGCAP NCC = new NHACUNGCAP();
           
            NCC.ShowDialog();
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            NHANVIEN NV = new NHANVIEN();
           
            NV.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                LOGIN LG = new LOGIN();
                Close();
                LG.Show();
                
            }
            
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            
            TAIKHOAN TK = new TAIKHOAN();
            
            TK.ShowDialog();
        }
        private void load()
        {
            
        }


        private void CHUCNANG_Load(object sender, EventArgs e)
        {
            
        }
      
        private void txtHTUser_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtHTCV_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
