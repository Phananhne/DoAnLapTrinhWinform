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
    public partial class XUATHANG : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public XUATHANG()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void XUATHANG_Load(object sender, EventArgs e)
        {
            loadHH();
            loadNV();
            loadXuatHang();
            With();
        }
        private void With()
        {
            dgvXuatHang.Columns[0].Width = 150;
            dgvXuatHang.Columns[1].Width = 150;
            dgvXuatHang.Columns[2].Width = 150;
            dgvXuatHang.Columns[3].Width = 150;
            dgvXuatHang.Columns[4].Width = 150;
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
        private void loadXuatHang()
        {
            var listXH = st.PhieuXuats.Join(
                    st.NhanViens,
                    px => px.MaNV,
                    nv => nv.MaNV,
                    (px, nv) => new
                    {
                        MaPX = px.MaPX,
                        NgayXuat = px.NgayXuat,
                        TenNV = nv.TenNV,
                        MaHang = px.MaHang,
                        SoLuong = px.SoLuong
                    }).Join(
                    st.HangHoas,
                    px => px.MaHang,
                    hh => hh.MaHang,
                    (px, hh) => new
                    {
                        MaPX = px.MaPX,
                        NgayXuat = px.NgayXuat,
                        TenNV = px.TenNV,
                        TenHang = hh.TenHang,
                        SoLuong = px.SoLuong
                    }).ToList();
            dgvXuatHang.DataSource = listXH;
        }
        private int GetSelectedRow(string MaPX)
        {
            for (int i = 0; i < dgvXuatHang.Rows.Count; i++)
            {
                if (dgvXuatHang.Rows[i].Cells[0].Value.ToString() == MaPX)
                {
                    return i;
                }
            }
            return -1;
        }
        private void dgvXuatHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtPhieuXuat.Text = dgvXuatHang.Rows[rowid].Cells[0].Value.ToString();
            cbTenNV.Text = dgvXuatHang.Rows[rowid].Cells[2].Value.ToString();
            cbTenHang.Text = dgvXuatHang.Rows[rowid].Cells[3].Value.ToString();
            dtpNgayXuat.Text = dgvXuatHang.Rows[rowid].Cells[1].Value.ToString();
            txtSoLuong.Text = dgvXuatHang.Rows[rowid].Cells[4].Value.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPhieuXuat.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayXuat.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                    if (GetSelectedRow(txtPhieuXuat.Text) == -1)
                    {
                        string mapx = txtPhieuXuat.Text;
                        int soluong = int.Parse(txtSoLuong.Text);
                        string tennv = cbTenNV.Text;
                        NhanVien rownv = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                        string manv = rownv.MaNV;
                        string tenhang = cbTenHang.Text;
                        HangHoa rowHH = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                        string mahang = rowHH.MaHang;
                        DateTime ngayxuat = DateTime.Parse(dtpNgayXuat.Text);
                        ngayxuat.ToString("yyyyMMdd");
                        PhieuXuat newPX = new PhieuXuat();
                        newPX.MaPX = mapx;
                        newPX.SoLuong = soluong;
                        newPX.MaNV = manv;
                        newPX.MaHang = mahang;
                        newPX.NgayXuat = ngayxuat;
                        DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            st.PhieuXuats.Add(newPX);
                            st.SaveChanges();
                            loadXuatHang();
                            MessageBox.Show("Thêm thành công!", "Thông báo");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại! \nMã phiếu xuất bị trùng!", "Thông báo");
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
                if (txtPhieuXuat.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayXuat.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mapx = txtPhieuXuat.Text;
                PhieuXuat newPX = st.PhieuXuats.Where(k => k.MaPX == mapx).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuXuats.Remove(newPX);
                    st.SaveChanges();
                    loadXuatHang();
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
            txtPhieuXuat.Text = "";
            txtSoLuong.Text = "";
            cbTenHang.Text = "";
            cbTenNV.Text = "";
            dtpNgayXuat.Text = "";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPhieuXuat.Text == "" || txtSoLuong.Text == "" || cbTenHang.Text == "" || cbTenNV.Text == "" || dtpNgayXuat.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần cập nhật!");
                string mapx = txtPhieuXuat.Text;
                int soluong = int.Parse(txtSoLuong.Text);
                string tennv = cbTenNV.Text;
                NhanVien rownv = st.NhanViens.Where(k => k.TenNV == tennv).FirstOrDefault();
                string manv = rownv.MaNV;
                string tenhang = cbTenHang.Text;
                HangHoa rowHH = st.HangHoas.Where(k => k.TenHang == tenhang).FirstOrDefault();
                string mahang = rowHH.MaHang;
                DateTime ngayxuat = DateTime.Parse(dtpNgayXuat.Text);
                ngayxuat.ToString("yyyyMMdd");
                PhieuXuat newPX = st.PhieuXuats.Where(k => k.MaPX == mapx).FirstOrDefault();
                newPX.SoLuong = soluong;
                newPX.MaNV = manv;
                newPX.MaHang = mahang;
                newPX.NgayXuat = ngayxuat;
                DialogResult dr = MessageBox.Show("Bạn có muốn cập nhật không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.PhieuXuats.AddOrUpdate(newPX);
                    st.SaveChanges();
                    loadXuatHang();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
    