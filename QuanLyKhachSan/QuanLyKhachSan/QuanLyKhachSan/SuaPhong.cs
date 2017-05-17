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
    public partial class SuaPhong : Form
    {
        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        string id1;
        public SuaPhong(string id)
        {
            id1 = id;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var suaGia = db.GiaPhongs.SingleOrDefault(p => p.MaPhong == id1);
            var suaphong = db.Phongs.SingleOrDefault(p => p.MaPhong == id1);
            suaphong.TenPhong = textBox3.Text;
            string malp = comboBox2.Text.TrimEnd();
            var loaiphong = db.LoaiPhongs.Where(p => p.TenLP == malp).ToList();
            suaGia.MaLP = loaiphong[0].MaLP.TrimEnd();
            string kieu = comboBox1.Text.TrimEnd();
            var Kieuphong = db.KieuPhongs.Where(p => p.TenKP == kieu).ToList();
            suaGia.MaKP = Kieuphong[0].MaKP.TrimEnd();
            suaGia.Gia = int.Parse(textBox7.Text);
            suaphong.MotaPhong = textBox2.Text;
            db.SaveChanges();
            this.Hide();
            MessageBox.Show("Đã sửa thành công phòng!");

        }
        private void SuaPhong_Load(object sender, EventArgs e)
        {
            var Phong = from p in db.Phongs
                        join b in db.GiaPhongs on p.MaPhong equals b.MaPhong
                        join a in db.KieuPhongs on b.MaKP equals a.MaKP
                        join c in db.LoaiPhongs on b.MaLP equals c.MaLP
                        where p.MaPhong == id1
                        select new
                        {
                            TenPhong = p.TenPhong,
                            Loai = c.TenLP,
                            Kieu = a.TenKP,
                            Gia = b.Gia,
                            Mota = p.MotaPhong
                        };

            var p1 = Phong.ToList();
            textBox1.Text= textBox3.Text = p1[0].TenPhong;
            textBox4.Text =comboBox2.Text= p1[0].Loai;
            textBox5.Text =comboBox1.Text= p1[0].Kieu;
            txtMota.Text = textBox2.Text= p1[0].Mota;
            textBox6.Text =textBox7.Text= p1[0].Gia.ToString();
            var loaiphong = db.LoaiPhongs.Select(p => p.TenLP).ToList();
            for (int i = 0; i < loaiphong.Count(); i++)
            {
                string l1 = loaiphong.ElementAt(i);
                comboBox2.Items.Add(l1);
                
            }
            var kieuphong = db.KieuPhongs.Select(p => p.TenKP).ToList();
            for (int j = 0; j < kieuphong.Count(); j++)
            {

                comboBox1.Items.Add(kieuphong.ElementAt(j));
            }
        }
    }
}
