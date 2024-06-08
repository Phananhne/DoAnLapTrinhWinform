using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class CTHD : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public CTHD()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void CTHD_Load(object sender, EventArgs e)
        {
            loadHD();
            loadCTHD();
            txtDonGia.Enabled = false;
            txtThanhTien.Enabled = false;
            txtSoLuong.Enabled = false;
            With();
        }
        private void loadHD()
        {
            var listHD = st.HoaDons.ToList();
            cbMaHD.DataSource = listHD;
            cbMaHD.ValueMember = "MaHD";
        }

        private void loadCTHD()
        {
            var listCTHD = st.HoaDons.Join(
                    st.ChiTietHoaDons,
                    hd => hd.MaHD,
                    ct => ct.MaHD,
                    (hd, ct) => new
                    {
                        MaHD = ct.MaHD,
                        SoLuong = hd.SoLuong,
                        MaHang = hd.MaHang
                    }).Join(
                    st.HangHoas,
                    hd => hd.MaHang,
                    hh => hh.MaHang,
                    (hd, hh) => new
                    {
                        MaHD = hd.MaHD,
                        SoLuong = hd.SoLuong,
                        TenHang = hh.TenHang,
                        DonGia = hh.DonGia,
                        ThanhTien = hd.SoLuong * hh.DonGia
                    }).ToList();
            dgvCTHD.DataSource = listCTHD;
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            
            cbMaHD.Text = dgvCTHD.Rows[rowid].Cells[0].Value.ToString();
            txtDonGia.Text = dgvCTHD.Rows[rowid].Cells[2].Value.ToString();
            txtSoLuong.Text = dgvCTHD.Rows[rowid].Cells[1].Value.ToString();
            txtThanhTien.Text = dgvCTHD.Rows[rowid].Cells[3].Value.ToString();
        }private void With()
        {
            dgvCTHD.Columns[0].Width = 150;
            dgvCTHD.Columns[1].Width = 150;
            dgvCTHD.Columns[2].Width = 150;
            dgvCTHD.Columns[3].Width = 150;
            dgvCTHD.Columns[4].Width = 150;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMaHD.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                string mahd = cbMaHD.Text;
                ChiTietHoaDon newCTHD = new ChiTietHoaDon();
                newCTHD.MaHD = mahd;
                DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.ChiTietHoaDons.Add(newCTHD);
                    st.SaveChanges();
                    loadCTHD();
                    MessageBox.Show("Thêm thành công!", "Thông báo");
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
                if (cbMaHD.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa!");
                string mahd = cbMaHD.Text;
                ChiTietHoaDon delCTHD = st.ChiTietHoaDons.Where(k => k.MaHD == mahd).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Ban có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.ChiTietHoaDons.Remove(delCTHD);
                    st.SaveChanges();
                    loadCTHD();
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
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "";
            cbMaHD.Text = "";
        }

        private void dgvCTHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
