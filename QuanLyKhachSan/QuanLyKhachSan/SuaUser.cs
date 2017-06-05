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
    public partial class SuaUser : Form
    {
        private string us;
        public SuaUser( string username)
        {
            us = username;
            InitializeComponent();
        }

        private void SuaUser_Load(object sender, EventArgs e)
        {
            using (QuanLyKhachSan.Model.QuanLyKhachSanEntities3 db = new Model.QuanLyKhachSanEntities3())
            {
                var user = db.users.FirstOrDefault(p => p.username == us);
                textBox1.Text = user.username;
                textBox2.Text = user.pass;
                textBox3.Text = user.role;
                textBox6.Text = user.username;
                textBox5.Text = user.pass;
                if (user.role.TrimEnd() == "user") { comboBox1.SelectedIndex = 0; }
                else comboBox1.SelectedIndex = 1;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox5.Text!=""&& textBox6.Text != "" && comboBox1.Text != "")
            {
                using (QuanLyKhachSan.Model.QuanLyKhachSanEntities3 db = new Model.QuanLyKhachSanEntities3())
                {
                    var user = db.users.FirstOrDefault(p => p.username == us);
                    user.username = textBox6.Text;
                    user.pass = textBox5.Text;
                    user.role = comboBox1.Text.TrimEnd();
                    db.SaveChanges();
                    MessageBox.Show("Sửa thông tin thành công!");
                    SuaUser_Load(sender,e);
                }
            }
        }
    }
}
