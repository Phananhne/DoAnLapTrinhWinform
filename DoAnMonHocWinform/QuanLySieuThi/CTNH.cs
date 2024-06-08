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
    public partial class CTNH : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public CTNH()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void CTNH_Load(object sender, EventArgs e)
        {
            loadPN();
            loadCTPN();
            With();
        }
        private void loadPN()
        {
            var listPN = st.PhieuNhaps.ToList();
            cbPhieuNhap.DataSource = listPN;
            cbPhieuNhap.ValueMember = "MaPN";
        }
        private void loadCTPN()
        {
            var listCTPN = st.ChiTietPhieuNhaps.Join(
                    st.PhieuNhaps,
                    ct => ct.MaPN,
                    pn => pn.MaPN,
                    (ct, pn) => new
                    {
                        MaPN = ct.MaPN,
                        ChietKhau = ct.ChietKhau,
                        NgayCapNhat = ct.NgayCapNhat,
                        SoLuong = pn.SoLuong,
                        DonGia = pn.DonGia,
                        ThanhTien = (pn.SoLuong * pn.DonGia) - ((pn.SoLuong * pn.DonGia) * ct.ChietKhau) / 100
                    }).ToList();
            dgvCTNH.DataSource = listCTPN;
        }

        private void dgvCTNH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;
            cbPhieuNhap.Text = dgvCTNH.Rows[rowid].Cells[0].Value.ToString();
            txtChietKhau.Text = dgvCTNH.Rows[rowid].Cells[1].Value.ToString();
            dtpNCN.Text = dgvCTNH.Rows[rowid].Cells[2].Value.ToString();
        }
        private void With()
        {
            dgvCTNH.Columns[0].Width = 150;
            dgvCTNH.Columns[1].Width = 150;
            dgvCTNH.Columns[2].Width = 150;
            dgvCTNH.Columns[3].Width = 150;
            dgvCTNH.Columns[4].Width = 150;
            dgvCTNH.Columns[5].Width = 150;

        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChietKhau.Text == "" || cbPhieuNhap.Text == "" || dtpNCN.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                string mapn = cbPhieuNhap.Text;
                float chietkhau = float.Parse(txtChietKhau.Text);
                DateTime ngaycapnhat = DateTime.Parse(dtpNCN.Text);
                ngaycapnhat.ToString("yyyyMMdd");
                ChiTietPhieuNhap newct = new ChiTietPhieuNhap();
                newct.MaPN = mapn;
                newct.ChietKhau = chietkhau;
                newct.NgayCapNhat = ngaycapnhat;
                DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.ChiTietPhieuNhaps.Add(newct);
                    st.SaveChanges();
                    loadCTPN();
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
                if (txtChietKhau.Text == "" || cbPhieuNhap.Text == "" || dtpNCN.Text == "")
                    throw new Exception("Vui lòng chọn thông tin cần xóa");
                string manh = cbPhieuNhap.Text;
                ChiTietPhieuNhap delCTNH = st.ChiTietPhieuNhaps.Where(k => k.MaPN == manh).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.ChiTietPhieuNhaps.Remove(delCTNH);
                    st.SaveChanges();
                    loadCTPN();
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
            cbPhieuNhap.Text = "";
            txtChietKhau.Text = "";
            dtpNCN.Text = "";
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
