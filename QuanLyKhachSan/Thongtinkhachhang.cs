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
    public partial class Thongtinkhachhang : Form
    {
        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        int id1;
        public Thongtinkhachhang(int id)
        {
            id1 = id;
            InitializeComponent();
        }

        private void Thongtinkhachhang_Load(object sender, EventArgs e)
        {
            QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
            var khachhang = db.KhachHangs.FirstOrDefault(p => p.MaKH == id1);
            if (khachhang != null)
            {
                textBox1.Text = khachhang.TenKH;
                textBox2.Text = khachhang.CMND;
                textBox3.Text = khachhang.SDT;
                textBox4.Text = khachhang.QuocTich;
                if (khachhang.GioiTinh == true) comboBox1.SelectedIndex = 1;
                if (khachhang.GioiTinh == false) comboBox1.SelectedIndex = 0;
                if (khachhang.GioiTinh == null) comboBox1.SelectedValue = null;
                textBox6.Text = khachhang.Email;
            }
            var sudung = from k in db.MaPhieuDKs
                         where k.MaKH == id1
                         select new
                         {
                             k.MaPDK,
                             k.MaKH,
                             k.KhachHang.TenKH,
                             k.NgayDen,
                             k.NgayDi,
                             k.SoPhong,

                         };
            dataGridView1.DataSource = sudung.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var khachhang = db.KhachHangs.FirstOrDefault(p => p.MaKH == id1);
            if (khachhang != null)
            {
                if (textBox1.Text != "") khachhang.TenKH = textBox1.Text.TrimEnd();
                if (textBox2.Text != "") khachhang.CMND = textBox2.Text.TrimEnd();
                if (textBox3.Text != "") khachhang.SDT = textBox3.Text.TrimEnd();
                if (textBox4.Text != "") khachhang.QuocTich = textBox4.Text.TrimEnd();
                if (comboBox1.SelectedIndex == 1) khachhang.GioiTinh = true;
                if (comboBox1.SelectedIndex == 0) khachhang.GioiTinh = false;
                if (comboBox1.SelectedValue == null) khachhang.GioiTinh = null;
                if (textBox6.Text != "") khachhang.Email = textBox6.Text.TrimEnd();
                db.SaveChanges();
                MessageBox.Show("Sua thong tin thanh cong");
                Thongtinkhachhang_Load(sender, e);
            }
        }
    }
}
