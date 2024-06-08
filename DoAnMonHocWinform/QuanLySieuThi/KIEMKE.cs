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
    public partial class KIEMKE : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public KIEMKE()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void KIEMKE_Load(object sender, EventArgs e)
        {
            loadHH();
            loadNV();
            loadKK();
            With();
        }
        private void With()
        {
            dgvKiemKe.Columns[0].Width = 150;
            dgvKiemKe.Columns[1].Width = 150;
            dgvKiemKe.Columns[2].Width = 150;
            dgvKiemKe.Columns[3].Width = 150;
            dgvKiemKe.Columns[4].Width = 150;
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
        private void loadKK()
        {
            var listKK = st.PhieuKiemKes.Join(
                    st.NhanViens,
                    kk => kk.MaNV,
                    nv => nv.MaNV,
                    (kk, nv) => new
                    {
                        MaPKK = kk.MaPKK,
                        NgayKK = kk.NgayKK,
                        TenNV = nv.TenNV,
                        SoLuongTon = kk.SoLuongTon,
                        MaHang = kk.MaHang
                    }).Join(
                    st.HangHoas,
                    kk => kk.MaHang,
                    hh => hh.MaHang,
                    (kk, hh) => new
                    {
                        MaPKK = kk.MaPKK,
                        NgayKK = kk.NgayKK,
                        TenNV = kk.TenNV,
                        SoLuongTon = kk.SoLuongTon,
                        TenHang = hh.TenHang
                    }
                ).ToList();
            dgvKiemKe.DataSource = listKK;
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvKiemKe.Rows.Count; i++)
            {
                if (dgvKiemKe.Rows[i].Cells[0].Value.ToString() == MaPN)
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
                if (txtMaPKK.Text == "" || txtSoLuongTon.Text == "" || cbTenNV.Text == "" || cbTenHang.Text == "" || dtpNgayKK.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaPKK.Text) == -1)
                {
                    string mapkk = txtMaPKK.Text;
                    int soluongton = int.Parse(txtSoLuongTon.Text);
                    string tennv = cbTenNV.Text;
                    NhanVien rownv = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                    string manv = rownv.MaNV;
                    string tenhang = cbTenHang.Text;
                    HangHoa rowhh = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                    string mahang = rowhh.MaHang;
                    DateTime ngaykk = DateTime.Parse(dtpNgayKK.Text);
                    ngaykk.ToString("yyyyMMdd");
                    PhieuKiemKe newpkk = new PhieuKiemKe();
                    newpkk.MaPKK = mapkk;
                    newpkk.SoLuongTon = soluongton;
                    newpkk.MaNV = manv;
                    newpkk.MaHang = mahang;
                    newpkk.NgayKK = ngaykk;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.PhieuKiemKes.Add(newpkk);
                        st.SaveChanges();
                        loadKK();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại \nMã phiếu kiểm kê bị trùng!", "Thông báo");
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
                if (txtMaPKK.Text == "" || txtSoLuongTon.Text == "" || cbTenNV.Text == "" || cbTenHang.Text == "" || dtpNgayKK.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mapkk = txtMaPKK.Text;
                PhieuKiemKe delPkk = st.PhieuKiemKes.Where(k => k.MaPKK == mapkk).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuKiemKes.Remove(delPkk);
                    st.SaveChanges();
                    loadKK();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMaPKK.Text = "";
            txtSoLuongTon.Text = "";
            dtpNgayKK.Text = "";
            cbTenHang.Text = "";
            cbTenNV.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMaPKK.Text == "" || txtSoLuongTon.Text == "" || cbTenNV.Text == "" || cbTenHang.Text == "" || dtpNgayKK.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần sửa!");
                string mapkk = txtMaPKK.Text;
                int soluongton = int.Parse(txtSoLuongTon.Text);
                string tennv = cbTenNV.Text;
                NhanVien rownv = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                string manv = rownv.MaNV;
                string tenhang = cbTenHang.Text;
                HangHoa rowhh = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                string mahang = rowhh.MaHang;
                DateTime ngaykk = DateTime.Parse(dtpNgayKK.Text);
                ngaykk.ToString("yyyyMMdd");
                //
                PhieuKiemKe uptPkk = st.PhieuKiemKes.Where(k => k.MaPKK == mapkk).FirstOrDefault();
                uptPkk.SoLuongTon = soluongton;
                uptPkk.MaNV = manv;
                uptPkk.MaHang = mahang;
                uptPkk.NgayKK = ngaykk;
                DialogResult dr = MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuKiemKes.AddOrUpdate(uptPkk);
                    st.SaveChanges();
                    loadKK();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKiemKe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtMaPKK.Text = dgvKiemKe.Rows[rowid].Cells[0].Value.ToString();
            cbTenNV.Text = dgvKiemKe.Rows[rowid].Cells[2].Value.ToString();
            cbTenHang.Text = dgvKiemKe.Rows[rowid].Cells[4].Value.ToString();
            dtpNgayKK.Text = dgvKiemKe.Rows[rowid].Cells[1].Value.ToString();
            txtSoLuongTon.Text = dgvKiemKe.Rows[rowid].Cells[3].Value.ToString();
        }
    }
}
