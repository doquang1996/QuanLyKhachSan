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
    public partial class user : Form
    {
        private QuanLyKhachSan.Model.QuanLyKhachSanEntities3 db = new Model.QuanLyKhachSanEntities3();
        Model.user user1 = new Model.user();
        public user(Model.user us)
        {
            user1 = us;
            InitializeComponent();
        }
        private void formLoad()
        {
            Model.QuanLyKhachSanEntities3 db = new Model.QuanLyKhachSanEntities3();
            var listuser = db.users.Where(p=>p.username==p.username);
            dataGridView1.DataSource = listuser.ToList();
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            button4.Enabled = false;
            button3.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Model.user user  = new Model.user();
            if (textBox1.Text != "")
            {
                var kt = db.users.FirstOrDefault(p=>p.username==textBox1.Text.TrimEnd());
                if(kt != null)
                {
                    label5.Text = "Tên đăng nhập đã có người sử dụng";
                    return;
                };
                user.username = textBox1.Text;
                label5.Text = "";
            }
            else
            {
                label5.Text = "Nhập tên đăng nhập";
            }
            string pass1 = textBox2.Text;
            string pass2 = textBox3.Text;
            if (textBox2.Text == "")
            {
                label6.Text = "Nhập mật khẩu";
                return;
            }
            else
            {
                label6.Text = "";
                if (pass1 != pass2)
                {
                    label4.Text = "Xác nhận lại mật khẩu";
                    return;
                }
                else
                {
                    user.pass = pass1;
                    user.role = "user";
                    label4.Text = "";
                }
            }
            db.users.Add(user);
            db.SaveChanges();
            MessageBox.Show("Tạo tài khoản thành công");
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SuaUser su = new SuaUser(dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd());
            su.ShowDialog();
            formLoad();
        }

        private void l(object sender, EventArgs e)
        {
            formLoad();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string username = dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd();
            var us = db.users.FirstOrDefault(p => p.username == username);
            
            if (MessageBox.Show("Xác nhận xóa tài khoản:  " + username, "", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                db.users.Remove(us);
                db.SaveChanges();
                MessageBox.Show("Đã xóa");
                formLoad();
            }
            else return;
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button4.Enabled = true;
            button3.Enabled = true;
        }
    }
}
