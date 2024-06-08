using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class NHAPHANG : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public NHAPHANG()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void NHAPHANG_Load(object sender, EventArgs e)
        {
            loadNV();
            loadNCC();
            loadHH();
            loadNhapHang();
            With();
        }
        private void With()
        {
            dgvNhapHang.Columns[0].Width = 150;
            dgvNhapHang.Columns[1].Width = 150;
            dgvNhapHang.Columns[2].Width = 150;
            dgvNhapHang.Columns[3].Width = 150;
            dgvNhapHang.Columns[4].Width = 150;
            dgvNhapHang.Columns[5].Width = 150;
            dgvNhapHang.Columns[6].Width = 150;
        }
        private void loadNV()
        {
            var listNV = st.NhanViens.ToList();
            cbTenNV.DataSource = listNV;
            cbTenNV.DisplayMember = "TenNV";
            cbTenNV.ValueMember = "MaNV";
        }
        private void loadNCC()
        {
            var listNCC = st.NhaCungCaps.ToList();
            cbNhaCC.DataSource = listNCC;
            cbNhaCC.DisplayMember = "TenNCC";
            cbNhaCC.ValueMember = "MaNCC";
        }
        private void loadHH()
        {
            var listHH = st.HangHoas.ToList();
            cbHang.DataSource = listHH;
            cbHang.DisplayMember = "TenHang";
            cbHang.ValueMember = "MaHang";
        }
        private void loadNhapHang()
        {
            var listNH = st.PhieuNhaps.Join(
                    st.NhanViens,
                    pn => pn.MaNV,
                    nv => nv.MaNV,
                    (pn, nv) => new
                    {
                        MaPN = pn.MaPN,
                        NgayNhap = pn.NgayNhap,
                        TenNV = nv.TenNV,
                        MaNCC = pn.MaNCC,
                        SoLuong = pn.SoLuong,
                        DonGia = pn.DonGia,
                        MaHang = pn.MaHang
                    }).Join(
                    st.HangHoas,
                    pn => pn.MaHang,
                    hh => hh.MaHang,
                    (pn, hh) => new
                    {
                        MaPN = pn.MaPN,
                        NgayNhap = pn.NgayNhap,
                        TenNV = pn.TenNV,
                        MaNCC = pn.MaNCC,
                        SoLuong = pn.SoLuong,
                        DonGia = pn.DonGia,
                        Tenhang = hh.TenHang
                    }).Join(
                    st.NhaCungCaps,
                    pn => pn.MaNCC,
                    ncc => ncc.MaNCC,
                    (pn, ncc) => new
                    {
                        MaPN = pn.MaPN,
                        NgayNhap = pn.NgayNhap,
                        TenNV = pn.TenNV,
                        TenNCC = ncc.TenNCC,
                        SoLuong = pn.SoLuong,
                        DonGia = pn.DonGia,
                        TenHang = pn.Tenhang
                    }).ToList();
            dgvNhapHang.DataSource = listNH;
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvNhapHang.Rows.Count; i++)
            {
                if (dgvNhapHang.Rows[i].Cells[0].Value.ToString() == MaPN)
                {
                    return i;
                }
            }
            return -1;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDonGia.Text == "" || txtMaNhap.Text == "" || txtSoLuong.Text == "" || cbHang.Text == "" || cbNhaCC.Text == "" || cbTenNV.Text == "" || dtpNgayNhap.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaNhap.Text) == -1)
                {
                    string mapn = txtMaNhap.Text;
                    DateTime ngaynhap = DateTime.Parse(dtpNgayNhap.Text);
                    ngaynhap.ToString("yyyyMMdd");
                    string tenncc = cbNhaCC.Text;
                    NhaCungCap rowNCC = st.NhaCungCaps.Where(k => k.TenNCC == tenncc).FirstOrDefault();
                    string mancc = rowNCC.MaNCC;
                    int soluong = int.Parse(txtSoLuong.Text);
                    int dongia = int.Parse(txtDonGia.Text);
                    string tenhang = cbHang.Text;
                    HangHoa rowHH = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                    string mahang = rowHH.MaHang;
                    string tennv = cbTenNV.Text;
                    NhanVien rowNV = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                    string manv = rowNV.MaNV;
                    PhieuNhap newPN = new PhieuNhap();
                    newPN.MaPN = mapn;
                    newPN.NgayNhap = ngaynhap;
                    newPN.MaNCC = mancc;
                    newPN.SoLuong = soluong;
                    newPN.DonGia = dongia;
                    newPN.MaHang = mahang;
                    newPN.MaNV = manv;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.PhieuNhaps.Add(newPN);
                        st.SaveChanges();
                        loadNhapHang();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nMã phiếu nhập bị trùng!", "Thông báo");
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
                if (txtDonGia.Text == "" || txtMaNhap.Text == "" || txtSoLuong.Text == "" || cbHang.Text == "" || cbNhaCC.Text == "" || cbTenNV.Text == "" || dtpNgayNhap.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mapn = txtMaNhap.Text;
                PhieuNhap delPN = st.PhieuNhaps.Where(k => k.MaPN == mapn).FirstOrDefault();
                ChiTietPhieuNhap delCTNH = st.ChiTietPhieuNhaps.Where(k => k.MaPN == mapn).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuNhaps.Remove(delPN);
                    st.SaveChanges();
                    loadNhapHang();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDonGia.Text == "" || txtMaNhap.Text == "" || txtSoLuong.Text == "" || cbHang.Text == "" || cbNhaCC.Text == "" || cbTenNV.Text == "" || dtpNgayNhap.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần cập nhật!");
                string mapn = txtMaNhap.Text;
                DateTime ngaynhap = DateTime.Parse(dtpNgayNhap.Text);
                ngaynhap.ToString("yyyyMMdd");
                string tenncc = cbNhaCC.Text;
                NhaCungCap rowNCC = st.NhaCungCaps.Where(k => k.TenNCC == tenncc).FirstOrDefault();
                string mancc = rowNCC.MaNCC;
                int soluong = int.Parse(txtSoLuong.Text);
                int dongia = int.Parse(txtDonGia.Text);
                string tenhang = cbHang.Text;
                HangHoa rowHH = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                string mahang = rowHH.MaHang;
                string tennv = cbTenNV.Text;
                NhanVien rowNV = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                string manv = rowNV.MaNV;
                PhieuNhap uptPN = st.PhieuNhaps.Where(k => k.MaPN == mapn).FirstOrDefault();
                uptPN.NgayNhap = ngaynhap;
                uptPN.MaNCC = mancc;
                uptPN.MaNV = manv;
                uptPN.SoLuong = soluong;
                uptPN.DonGia = dongia;
                uptPN.MaHang = mahang;
                DialogResult dr = MessageBox.Show("Bạn có muốn cập nhật không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuNhaps.AddOrUpdate(uptPN);
                    st.SaveChanges();
                    loadNhapHang();
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
            txtDonGia.Text = "";
            txtMaNhap.Text = "";
            txtSoLuong.Text = "";
            cbHang.Text = "";
            cbNhaCC.Text = "";
            cbTenNV.Text = "";
            dtpNgayNhap.Text = "";
        }

        private void dgvNhapHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtDonGia.Text = dgvNhapHang.Rows[rowid].Cells[5].Value.ToString();
            txtMaNhap.Text = dgvNhapHang.Rows[rowid].Cells[0].Value.ToString();
            cbTenNV.Text = dgvNhapHang.Rows[rowid].Cells[2].Value.ToString();
            cbNhaCC.Text = dgvNhapHang.Rows[rowid].Cells[3].Value.ToString();
            dtpNgayNhap.Text = dgvNhapHang.Rows[rowid].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvNhapHang.Rows[rowid].Cells[4].Value.ToString();
            cbHang.Text = dgvNhapHang.Rows[rowid].Cells[6].Value.ToString();
        }
    }
}
