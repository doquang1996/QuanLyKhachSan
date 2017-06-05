using QuanLyKhachSan.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan

{
    public partial class FormLogin : Form
    {

        public static readonly QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            txtUserName.Focus();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text == "")
            {
                label3.Text = "Xin nhap ten dang nhap!";
                return;
            }
            if (txtPassword.Text == "")
            {
                label3.Text = "Xin nhap mat khau";
            }
            if (txtUserName != null && txtPassword != null)
            {
                var user = db.users.FirstOrDefault(p => p.username == txtUserName.Text.TrimEnd());
                if (user == null)
                {
                    label3.Text = "Sai ten dang nhap!";
                    return;
                }
                else
                {
                    if (user.pass == txtPassword.Text.TrimEnd())
                    {
                        this.Hide();
                        label3.Text = null;
                        using
                                (MainForm suadv = new MainForm(user))
                        {
                            suadv.ShowDialog();

                        }
                        
                        
                    }
                    else
                    {
                        label3.Text = "Sai mat khau";
                        return;
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
