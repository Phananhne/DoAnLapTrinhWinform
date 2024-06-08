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
    public partial class HANGHOA : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public HANGHOA()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void HANGHOA_Load(object sender, EventArgs e)
        {
            loadHangHoa();
            With();
        }
        private void With()
        {
            dgvHangHoa.Columns[0].Width = 150;
            dgvHangHoa.Columns[1].Width = 150;
            dgvHangHoa.Columns[2].Width = 150;
            dgvHangHoa.Columns[3].Width = 150;
            dgvHangHoa.Columns[4].Width = 150;
            dgvHangHoa.Columns[5].Width = 150;
        }
        private void loadHangHoa()
        {
            var listHH = st.HangHoas.Select(k => new
            {
                MaHang = k.MaHang,
                TenHang = k.TenHang,
                DonGia = k.DonGia,
                DonViTinh = k.DonViTinh,
                HSD = k.HSD,
                NoiSanXuat = k.NoiSX
            }).ToList();
            dgvHangHoa.DataSource = listHH;
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvHangHoa.Rows.Count; i++)
            {
                if (dgvHangHoa.Rows[i].Cells[0].Value.ToString() == MaPN)
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
                if (txtDonGia.Text == "" || txtDonViTinh.Text == "" || txtMaHang.Text == "" || txtNoiSX.Text == "" || txtTenHang.Text == "" || dtpHSD.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtMaHang.Text) == -1)
                {
                    string mahang = txtMaHang.Text;
                    string tenhang = txtTenHang.Text;
                    int dongia = int.Parse(txtDonGia.Text);
                    int DVTinh = int.Parse(txtDonViTinh.Text);
                    DateTime hsd = DateTime.Parse(dtpHSD.Text);
                    hsd.ToString("yyyyMMdd");
                    string noisx = txtNoiSX.Text;
                    HangHoa newHH = new HangHoa();
                    newHH.MaHang = mahang;
                    newHH.TenHang = tenhang;
                    newHH.DonGia = dongia;
                    newHH.DonViTinh = DVTinh;
                    newHH.NoiSX = noisx;
                    newHH.HSD = hsd;
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    { 
                        st.HangHoas.Add(newHH);
                        st.SaveChanges();
                        loadHangHoa();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nMã hàng bị trùng!", "Thông báo");
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
                if (txtDonGia.Text == "" || txtDonViTinh.Text == "" || txtMaHang.Text == "" || txtNoiSX.Text == "" || txtTenHang.Text == "" || dtpHSD.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mahang = txtMaHang.Text;
                HangHoa delHH = st.HangHoas.Where(k => k.MaHang == mahang).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn Xóa Không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.HangHoas.Remove(delHH);
                    st.SaveChanges();
                    loadHangHoa();
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
                if (txtDonGia.Text == "" || txtDonViTinh.Text == "" || txtMaHang.Text == "" || txtNoiSX.Text == "" || txtTenHang.Text == "" || dtpHSD.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần sửa!");
                string mahang = txtMaHang.Text;
                string tenhang = txtTenHang.Text;
                int dongia = int.Parse(txtDonGia.Text);
                int DVTinh = int.Parse(txtDonViTinh.Text);
                DateTime hsd = DateTime.Parse(dtpHSD.Text);
                hsd.ToString("yyyyMMdd");
                string noisx = txtNoiSX.Text;
                //
                HangHoa uptHH = st.HangHoas.Where(k => k.MaHang == mahang).FirstOrDefault();
                uptHH.TenHang = tenhang;
                uptHH.DonGia = dongia;
                uptHH.DonViTinh = DVTinh;
                uptHH.HSD = hsd;
                uptHH.NoiSX = noisx;
                DialogResult dr = MessageBox.Show("Bạn có muốn cập nhật không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.HangHoas.AddOrUpdate(uptHH);
                    st.SaveChanges();
                    loadHangHoa();
                    MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDonGia.Text = "";
            txtDonViTinh.Text = "";
            txtMaHang.Text = "";
            txtNoiSX.Text = "";
            txtTenHang.Text = "";
            dtpHSD.Text = "";
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            txtMaHang.Text = dgvHangHoa.Rows[rowid].Cells[0].Value.ToString();
            txtTenHang.Text = dgvHangHoa.Rows[rowid].Cells[1].Value.ToString();
            txtDonGia.Text = dgvHangHoa.Rows[rowid].Cells[2].Value.ToString();
            txtDonViTinh.Text = dgvHangHoa.Rows[rowid].Cells[3].Value.ToString();
            txtNoiSX.Text = dgvHangHoa.Rows[rowid].Cells[5].Value.ToString();
            dtpHSD.Text = dgvHangHoa.Rows[rowid].Cells[4].Value.ToString();
        }
    }
        
}
