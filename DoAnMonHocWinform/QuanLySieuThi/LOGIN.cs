using QuanLySieuThi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLySieuThi
{
    public partial class LOGIN : Form
    {
      //  THONGTIN TT = new THONGTIN();
        public LOGIN()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string tendn = txtUsername.Text;
                string matkhau = txtPass.Text;

                //kết nối csdl
                string str = "Data Source=PHANANH22\\SQLEXPRESS;Initial Catalog=QuanLySieuThi;Integrated Security=True";
                SqlConnection conn = new SqlConnection(str);
                conn.Open();

                //truy vấn dữ liệu
                SqlCommand cmd = new SqlCommand("SELECT Password ,TenNV, ChucVu.TenCV  FROM NhanVien,Account,ChucVu where NhanVien.MaCV=ChucVu.MaCV and ChucVu.MaCV=Account.MaCV and Username = '" + tendn + "'", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                //kết nối csdl
                if (reader.HasRows)//kiểm tra có kết quả trả về
                {
                    reader.Read();
                    if (reader.GetString(0) == matkhau)
                    {
                        string tennv = reader.GetString(1);
                        string chucvu = reader.GetString(2);
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo!", MessageBoxButtons.OK);
                        CHUCNANG f = new CHUCNANG(tennv, chucvu);
                        f.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show("Mật khẩu sai!", "Thông Báo", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Người dùng không hợp lệ!", "Thông Báo!", MessageBoxButtons.OK);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Thông Báo!");
            }
            /* try
             {

                 using (ContextSieuThiDB st = new ContextSieuThiDB())
                 {
                     //string machucvu = st.Database.SqlQuery<string>("Select MaCV from ChucVu ").FirstOrDefault();
                     //string tenchucvu = st.Database.SqlQuery<string>("Select TenCV from ChucVu ").FirstOrDefault();




                     var data = st.Accounts.FirstOrDefault(k => k.Username == txtUsername.Text && k.Password == txtPass.Text);
                     if (data != null)
                     {

                         MessageBox.Show("Đăng nhập thành công" + "\nXin chào User : " + txtUsername.Text, "Thông báo");
                         this.Hide();
                         CHUCNANG CN = new CHUCNANG();
                         CN.ShowDialog();
                         TT.Hide();
                         Hide();
                     }
                     else
                     {
                         MessageBox.Show("Đăng nhập thất bại" + "\nKiểm tra lại Username và Password", "Thông báo");
                         this.Hide();
                         LOGIN login = new LOGIN();
                         login.ShowDialog();

                     }

                 }

             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Thông báo");
             }*/

        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void Login()
        {
            
        }
        private void txtUsername_Enter(object sender, EventArgs e)
        {
            if(txtUsername.Text == "Username")
            {
                txtUsername.Text = "";
                txtUsername.ForeColor = Color.Black;
            }
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            if(txtUsername.Text == "" )
            {
                txtUsername.Text = "Username";
                txtUsername.ForeColor = Color.Black;
            }         
        }
        private void txtPass_Enter(object sender, EventArgs e)
        {
            if (txtPass.Text == "Password")
            {
                txtPass.Text = "";
                txtPass.ForeColor = Color.Black;
            }
        }
        private void txtPass_Leave(object sender, EventArgs e)
        {
            if (txtPass.Text == "")
            {
                txtPass.Text = "Password";
                txtPass.ForeColor = Color.Black;
            }
        }

        private void LOGIN_Load(object sender, EventArgs e)
        {
            
        }

        private void cbxhienthimk_CheckedChanged(object sender, EventArgs e)
        {
            if(cbxhienthimk.Checked)
            {
                txtPass.PasswordChar = '\0';
            }
            else
            {
                txtPass.PasswordChar = '*';
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
