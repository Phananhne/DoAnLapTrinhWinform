using QuanLySieuThi.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class TAIKHOAN : Form
    {
        SieuthiContextDB st = new SieuthiContextDB();
        public TAIKHOAN()
        {
            InitializeComponent();
        }

        private void TAIKHOAN_Load(object sender, EventArgs e)
        {
            LoadCV();
            LoadTK();
            With();
        }
        public void LoadCV()
        {
            var listCV = st.ChucVus.ToList();
            cbChucVu.DataSource = listCV;
            cbChucVu.DisplayMember = "TenCV";
            cbChucVu.ValueMember = "MaCV";
        }
        
        private void LoadTK()
        {
            var listTK = st.Accounts.Join(
                    st.ChucVus,
                    tk => tk.MaCV,
                    cv => cv.MaCV,
                    (tk, cv) => new
                    {

                        User = tk.Username,
                        Pass = tk.Password,
                        ChucVu = cv.TenCV,
                        
                    }).ToList();   
            dgvTaiKhoan.DataSource = listTK;
        }
        private void With()
        {
            dgvTaiKhoan.Columns[0].Width = 150;
            dgvTaiKhoan.Columns[1].Width = 150;
            dgvTaiKhoan.Columns[2].Width = 150;
            
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowid = e.RowIndex;

            txtUser.Text = dgvTaiKhoan.Rows[rowid].Cells[0].Value.ToString();
            txtPass.Text = dgvTaiKhoan.Rows[rowid].Cells[1].Value.ToString();
            cbChucVu.Text = dgvTaiKhoan.Rows[rowid].Cells[2].Value.ToString();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPass.Text = "";
            txtUser.Text = "";
            cbChucVu.Text = "";
  
            
        }
        private int GetSelectedRow(string MaPN)
        {
            for (int i = 0; i < dgvTaiKhoan.Rows.Count; i++)
            {
                if (dgvTaiKhoan.Rows[i].Cells[0].Value.ToString() == MaPN)
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
                if (txtPass.Text == "" || txtUser.Text == "" || cbChucVu.Text == "")
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                if (GetSelectedRow(txtUser.Text) == -1)
                {
                    string User = txtUser.Text;
                    string Pass = txtPass.Text;
                    string chucvu = cbChucVu.Text;
                    
                    ChucVu rowCV = st.ChucVus.Where(k => k.TenCV == chucvu).FirstOrDefault();
                    string macv = rowCV.MaCV;
                    Account newTk = new Account();
                    newTk.Username = User;
                    newTk.Password = Pass;
                    newTk.MaCV = macv;
                    
                    DialogResult dr = MessageBox.Show("Bạn có muốn thêm không?", "Thông báo", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        st.Accounts.Add(newTk);
                        st.SaveChanges();
                        LoadTK();
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                    }
                }
                else
                {
                    MessageBox.Show("Thêm thất bại! \nUsername bị trùng!", "Thông báo");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            
            
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUser.Text == "" || txtPass.Text == "" || cbChucVu.Text == "")
                    throw new Exception("Vui lòng chọn thông tin để xóa!");
                string User = txtUser.Text;
                Account delTk = st.Accounts.Where(k => k.Username == User).FirstOrDefault();
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    st.Accounts.Remove(delTk);
                    st.SaveChanges();
                    LoadTK();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }  
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpt_Click(object sender, EventArgs e)
        {
            /*string User = txtUser.Text;
            string Pass = txtPass.Text;
            string chucvu = cbChucVu.Text;
            ChucVu rowCV = st.ChucVus.Where(k => k.TenCV == chucvu).FirstOrDefault();
            string macv = rowCV.MaCV;
            Account uptTk = st.Accounts.Where(k => k.Username == User).FirstOrDefault();
            uptTk.Username = User;
            uptTk.Password = Pass;
            uptTk.MaCV = macv;
            st.Accounts.AddOrUpdate(uptTk);
            st.SaveChanges();
            LoadTK();
            MessageBox.Show("Cập nhật tài khoản thành công", "Thông báo");*/
        }
    }
}
