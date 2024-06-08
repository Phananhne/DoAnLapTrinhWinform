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
    public partial class NHACUNGCAP : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public NHACUNGCAP()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void NHACUNGCAP_Load(object sender, EventArgs e)
        {
            loadNCC();
            With();
        }
        private void With()
        {
            dgvNhaCungCap.Columns[0].Width = 150;
            dgvNhaCungCap.Columns[1].Width = 150;
            dgvNhaCungCap.Columns[2].Width = 150;
            dgvNhaCungCap.Columns[3].Width = 150;
            dgvNhaCungCap.Columns[4].Width = 150;
        }
        private void loadNCC()
        {
            var listNCC = st.NhaCungCaps.Select(k => new
            {
                MaNCC = k.MaNCC,
                TenNCC = k.TenNCC,
                DiaChi = k.DiaChi,
                DienThoai = k.DienThoai,
                Email = k.Email
            }).ToList();
            dgvNhaCungCap.DataSource = listNCC;
        }

        private void dgvNhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtMaNCC.Text = dgvNhaCungCap.Rows[rowid].Cells[0].Value.ToString();
            txtTenNCC.Text = dgvNhaCungCap.Rows[rowid].Cells[1].Value.ToString();
            txtDiaChi.Text = dgvNhaCungCap.Rows[rowid].Cells[2].Value.ToString();
            txtSDT.Text = dgvNhaCungCap.Rows[rowid].Cells[3].Value.ToString();
            txtEmail.Text = dgvNhaCungCap.Rows[rowid].Cells[4].Value.ToString();
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvNhaCungCap.Rows.Count; i++)
            {
                if (dgvNhaCungCap.Rows[i].Cells[0].Value.ToString() == MaPN)
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
                if (txtDiaChi.Text == "" || txtEmail.Text == "" || txtMaNCC.Text == "" || txtSDT.Text == "" || txtTenNCC.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaNCC.Text) == -1)
                {
                    string mancc = txtMaNCC.Text;
                    string tenncc = txtTenNCC.Text;
                    string diachi = txtDiaChi.Text;
                    string dienthoai = txtSDT.Text;
                    string email = txtEmail.Text;
                    NhaCungCap newNCC = new NhaCungCap();
                    newNCC.MaNCC = mancc;
                    newNCC.TenNCC = tenncc;
                    newNCC.DiaChi = diachi;
                    newNCC.DienThoai = dienthoai;
                    newNCC.Email = email;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.NhaCungCaps.Add(newNCC);
                        st.SaveChanges();
                        loadNCC();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nMã nhà cung cấp bị trùng!", "Thông báo");
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
                if (txtDiaChi.Text == "" || txtEmail.Text == "" || txtMaNCC.Text == "" || txtSDT.Text == "" || txtTenNCC.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mancc = txtMaNCC.Text;
                NhaCungCap delNCC = st.NhaCungCaps.Where(k => k.MaNCC == mancc).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.NhaCungCaps.Remove(delNCC);
                    st.SaveChanges();
                    loadNCC();
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
                if (txtDiaChi.Text == "" || txtEmail.Text == "" || txtMaNCC.Text == "" || txtSDT.Text == "" || txtTenNCC.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần sủa!");
                string mancc = txtMaNCC.Text;
                string tenncc = txtTenNCC.Text;
                string diachi = txtDiaChi.Text;
                string dienthoai = txtSDT.Text;
                string email = txtEmail.Text;
                NhaCungCap uptNCC = st.NhaCungCaps.Where(k => k.MaNCC == mancc).FirstOrDefault();
                uptNCC.TenNCC = tenncc;
                uptNCC.DiaChi = diachi;
                uptNCC.DienThoai = dienthoai;
                uptNCC.Email = email;
                DialogResult dr = MessageBox.Show("Bạn có mún sửa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.NhaCungCaps.AddOrUpdate(uptNCC);
                    st.SaveChanges();
                    loadNCC();
                    MessageBox.Show("Cập nhật thành công", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDiaChi.Text = "";
            txtTenNCC.Text = "";
            txtMaNCC.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
        }
    }
}
