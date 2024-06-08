using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class NHANVIEN : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public NHANVIEN()
        {
            InitializeComponent();
        }

        private void NHANVIEN_Load(object sender, EventArgs e)
        {
            LoadCV();
            loadNV();
            With();
        }
        private void With()
        {
            dgvNhanVien.Columns[0].Width = 150;
            dgvNhanVien.Columns[1].Width = 150;
            dgvNhanVien.Columns[2].Width = 150;
            dgvNhanVien.Columns[3].Width = 150;
            dgvNhanVien.Columns[4].Width = 150;
            dgvNhanVien.Columns[5].Width = 150;
            dgvNhanVien.Columns[6].Width = 150;
        }
        
        

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvNhanVien.Rows.Count; i++)
            {
                if (dgvNhanVien.Rows[i].Cells[0].Value.ToString() == MaPN)
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
                if (txtDiaChi.Text == "" || txtMaNV.Text == "" || txtSDT.Text == "" || txtTenNV.Text == "" || cbChucVu.Text == "" || cbGioiTinh.Text == "" || dtpNgaySinh.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaNV.Text) == -1)
                {
                    string manv = txtMaNV.Text;
                    string tennv = txtTenNV.Text;
                    string gioitinh = cbGioiTinh.Text;
                    DateTime ngaysinh = DateTime.Parse(dtpNgaySinh.Text);
                    ngaysinh.ToString("yyyyMMdd");
                    string diachi = txtDiaChi.Text;
                    string sodt = txtSDT.Text;
                    string chucvu = cbChucVu.Text;
                    ChucVu rowCV = st.ChucVus.Where(k => k.TenCV == chucvu).FirstOrDefault();
                    string macv = rowCV.MaCV;
                    NhanVien newNV = new NhanVien();
                    newNV.MaNV = manv;
                    newNV.TenNV = tennv;
                    newNV.NgaySinh = ngaysinh;
                    newNV.GioiTinh = gioitinh;
                    newNV.DiaChi = diachi;
                    newNV.SoDienThoai = sodt;
                    newNV.MaCV = macv;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.NhanViens.Add(newNV);
                        st.SaveChanges();
                        loadNV();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nMã nhân viên bị trùng!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadCV()
        {
            var listCV = st.ChucVus.ToList();
            cbChucVu.DataSource = listCV;
            cbChucVu.DisplayMember = "TenCV";
            cbChucVu.ValueMember = "MaCV";
        }
        private void loadNV()
        {
            var listNV = st.NhanViens.Join(
                    st.ChucVus,
                    nv => nv.MaCV,
                    cv => cv.MaCV,
                    (nv, cv) => new
                    {
                        MaNV = nv.MaNV
                        ,
                        TenNV = nv.TenNV,
                        GioiTinh = nv.GioiTinh,
                        NgaySinh = nv.NgaySinh,
                        DiaChi = nv.DiaChi,
                        SoDT = nv.SoDienThoai,
                        ChucVu = cv.TenCV
                    }
                ).ToList();
            dgvNhanVien.DataSource = listNV;
        }
       
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDiaChi.Text == "" || txtMaNV.Text == "" || txtSDT.Text == "" || txtTenNV.Text == "" || cbChucVu.Text == "" || cbGioiTinh.Text == "" || dtpNgaySinh.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string manv = txtMaNV.Text;
                NhanVien delNV = st.NhanViens.Where(k => k.MaNV == manv).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.NhanViens.Remove(delNV);
                    st.SaveChanges();
                    loadNV();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtMaNV.Text = dgvNhanVien.Rows[rowid].Cells[0].Value.ToString();
            txtTenNV.Text = dgvNhanVien.Rows[rowid].Cells[1].Value.ToString();
            cbGioiTinh.Text = dgvNhanVien.Rows[rowid].Cells[2].Value.ToString();
            txtDiaChi.Text = dgvNhanVien.Rows[rowid].Cells[4].Value.ToString();
            txtSDT.Text = dgvNhanVien.Rows[rowid].Cells[5].Value.ToString();
            cbChucVu.Text = dgvNhanVien.Rows[rowid].Cells[6].Value.ToString();
            dtpNgaySinh.Text = dgvNhanVien.Rows[rowid].Cells[3].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDiaChi.Text == "" || txtMaNV.Text == "" || txtSDT.Text == "" || txtTenNV.Text == "" || cbChucVu.Text == "" || cbGioiTinh.Text == "" || dtpNgaySinh.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần cập nhật!");
                string manv = txtMaNV.Text;
                string tennv = txtTenNV.Text;
                string gioitinh = cbGioiTinh.Text;
                DateTime ngaysinh = DateTime.Parse(dtpNgaySinh.Text);
                ngaysinh.ToString("yyyyMMdd");
                string diachi = txtDiaChi.Text;
                string sodt = txtSDT.Text;
                string chucvu = cbChucVu.Text;
                ChucVu rowCV = st.ChucVus.Where(k => k.TenCV == chucvu).FirstOrDefault();
                string macv = rowCV.MaCV;
                NhanVien uptNV = st.NhanViens.Where(k => k.MaNV == manv).FirstOrDefault();
                uptNV.TenNV = tennv;
                uptNV.GioiTinh = gioitinh;
                uptNV.NgaySinh = ngaysinh;
                uptNV.DiaChi = diachi;
                uptNV.SoDienThoai = sodt;
                uptNV.MaCV = macv;
                DialogResult dr = MessageBox.Show("Bạn có muốn cập nhật không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.NhanViens.AddOrUpdate(uptNV);
                    st.SaveChanges();
                    loadNV();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void InsertUpdate(int index)
        {

        }
        private void BindGrid(List<NhanVien> listNhanViens)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            dtpNgaySinh.Text = "";
            cbChucVu.Text = "";
            cbGioiTinh.Text = "";
        }
    }
}
