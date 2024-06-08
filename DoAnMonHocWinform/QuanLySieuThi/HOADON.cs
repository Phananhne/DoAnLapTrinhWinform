using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class HOADON : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public HOADON()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void HOADON_Load(object sender, EventArgs e)
        {
            loadHH();
            loadNV();
            loadHoaDon();
            With();
        }
        private void loadHH()
        {
            var listHH = st.HangHoas.ToList();
            cbTenHang.DataSource = listHH;
            cbTenHang.DisplayMember = "TenHang";
            cbTenHang.ValueMember = "MaHang";
        }
        private void loadNV()
        {
            var listNV = st.NhanViens.ToList();
            cbTenNV.DataSource = listNV;
            cbTenNV.DisplayMember = "TenNV";
            cbTenNV.ValueMember = "MaNV";
        }
        private void loadHoaDon()
        {
            var listHD = st.HoaDons.Join(
                    st.NhanViens,
                    hd => hd.MaNV,
                    nv => nv.MaNV,
                    (hd, nv) => new
                    {
                        MaHD = hd.MaHD,
                        NgayBan = hd.NgayBan,
                        TenNV = nv.TenNV,
                        SoLuong = hd.SoLuong,
                        MaHang = hd.MaHang
                    }).Join(
                        st.HangHoas,
                        hd => hd.MaHang,
                        hh => hh.MaHang,
                        (hd, hh) => new
                        {
                            MaHD = hd.MaHD,
                            NgayBan = hd.NgayBan,
                            TenNV = hd.TenNV,
                            SoLuong = hd.SoLuong,
                            TenHang = hh.TenHang
                        }
                    ).ToList();
            dgvHoaDon.DataSource = listHD;
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvHoaDon.Rows.Count; i++)
            {
                if (dgvHoaDon.Rows[i].Cells[0].Value.ToString() == MaPN)
                {
                    return i;
                }
            }
            return -1;
        }
        private void With()
        {
            dgvHoaDon.Columns[0].Width = 150;
            dgvHoaDon.Columns[1].Width = 150;
            dgvHoaDon.Columns[2].Width = 150;
            dgvHoaDon.Columns[3].Width = 150;
            dgvHoaDon.Columns[4].Width = 150;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayBan.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaHD.Text) == -1)
                {
                    string mahd = txtMaHD.Text;
                    DateTime ngayban = DateTime.Parse(dtpNgayBan.Text);
                    ngayban.ToString("yyyyMMdd");
                    int soluong = int.Parse(txtSoLuong.Text);
                    string tennv = cbTenNV.Text;
                    NhanVien rowNV = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                    string manv = rowNV.MaNV;
                    string tenhang = cbTenHang.Text;
                    HangHoa rowhh = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                    string mahang = rowhh.MaHang;
                    HoaDon newHD = new HoaDon();
                    newHD.MaHD = mahd;
                    newHD.NgayBan = ngayban;
                    newHD.SoLuong = soluong;
                    newHD.MaNV = manv;
                    newHD.MaHang = mahang;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.HoaDons.Add(newHD);
                        st.SaveChanges();
                        loadHoaDon();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nMã hóa đơn bị trùng!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayBan.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mahd = txtMaHD.Text;
                HoaDon delHD = st.HoaDons.Where(k => k.MaHD == mahd).FirstOrDefault();
                ChiTietHoaDon delCTHD = st.ChiTietHoaDons.Where(k => k.MaHD == mahd).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.HoaDons.Remove(delHD);
                    st.SaveChanges();
                    loadHoaDon();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaHD.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayBan.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần sửa!");
                string mahd = txtMaHD.Text;
                DateTime ngayban = DateTime.Parse(dtpNgayBan.Text);
                ngayban.ToString("yyyyMMdd");
                int soluong = int.Parse(txtSoLuong.Text);
                string tennv = cbTenNV.Text;
                NhanVien rowNV = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                string manv = rowNV.MaNV;
                //
                HoaDon uptHD = st.HoaDons.Where(k => k.MaHD == mahd).FirstOrDefault();
                uptHD.MaNV = manv;
                uptHD.NgayBan = ngayban;
                uptHD.SoLuong = soluong;
                DialogResult dr = MessageBox.Show("Bạn có muốn sửa thông tin?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.HoaDons.AddOrUpdate(uptHD);
                    st.SaveChanges();
                    loadHoaDon();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMaHD.Text = "";
            txtSoLuong.Text = "";
            cbTenHang.Text = "";
            cbTenNV.Text = "";
            dtpNgayBan.Text = "";
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtMaHD.Text = dgvHoaDon.Rows[rowid].Cells[0].Value.ToString();
            cbTenNV.Text = dgvHoaDon.Rows[rowid].Cells[2].Value.ToString();
            cbTenHang.Text = dgvHoaDon.Rows[rowid].Cells[4].Value.ToString();
            dtpNgayBan.Text = dgvHoaDon.Rows[rowid].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvHoaDon.Rows[rowid].Cells[3].Value.ToString();
        }
    }
}
