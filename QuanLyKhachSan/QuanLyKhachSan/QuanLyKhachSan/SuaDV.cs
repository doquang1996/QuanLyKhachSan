using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyKhachSan.Model;
namespace QuanLyKhachSan
{
    public partial class SuaDV : Form
    {

        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        int id1;
        public SuaDV(int id)
        {
            InitializeComponent();
            id1 = id;
        }

        private void SuaDV_Load(object sender, EventArgs e)
        {
            var dv = db.DichVus.Single(p => p.MaDV == id1);
            textBox6.Text = dv.TenDV.ToString();
            textBox5.Text = dv.GiaDV.ToString();
            textBox1.Text = dv.TenDV.ToString();
            textBox2.Text = dv.GiaDV.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var dv = db.DichVus.Single(p => p.MaDV == id1);
            dv.TenDV = textBox1.Text;
            dv.GiaDV = int.Parse(textBox2.Text);
            db.SaveChanges();
            this.Hide();
            MessageBox.Show("Đã sửa dịch vụ!");
        }
    }
}
