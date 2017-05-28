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
    public partial class Phong : Form
    {
        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        public Phong()
        {
            InitializeComponent();
        }

        private void Phong_Load(object sender, EventArgs e)
        {
            textBox1.Text = textBox2.Text = txtMota.Text = null;
            var loaiphong = db.LoaiPhongs.Select(p => p.TenLP).ToList();
            for(int i=0; i<loaiphong.Count(); i++)
            {
                string l1 = loaiphong.ElementAt(i);
                cmbLoaiPhong.Items.Add(l1);
            }
            var kieuphong = db.KieuPhongs.Select(p => p.TenKP).ToList();
            for(int j=0;j<kieuphong.Count();j++)
            {

                cmbKieuPhong.Items.Add(kieuphong.ElementAt(j));
            }
            var DanhsachPhong = from p in db.Phongs
                                join b in db.GiaPhongs on p.MaPhong equals b.MaPhong
                                join a in db.KieuPhongs on b.MaKP equals a.MaKP
                                join c in db.LoaiPhongs on b.MaLP equals c.MaLP
                                select new
                                {
                                    TenPhong = p.TenPhong,
                                    TrangThai = p.TrangThai,
                                    Loai = c.TenLP,
                                    Kieu = a.TenKP,
                                    Gia = b.Gia,
                                    MaPhong = b.MaPhong
                                };
            dataGridView1.DataSource = DanhsachPhong.ToList();
            dataGridView1.Columns[0].HeaderText = "Tên phòng";

            dataGridView1.Columns[1].HeaderText = "Trang thai";
            dataGridView1.Columns[2].HeaderText = "Loại phòng";

            dataGridView1.Columns[3].HeaderText = "Kiểu phòng";

            dataGridView1.Columns[4].HeaderText = "Giá phòng";
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnXoaPhong_Click(object sender, EventArgs e)
        {
            string maphong = dataGridView1.CurrentRow.Cells[5].Value.ToString().TrimEnd();
            var xoaphong = db.GiaPhongs.SingleOrDefault(p => p.MaPhong == maphong);
            var xoa = db.Phongs.SingleOrDefault(p => p.MaPhong == maphong);
            db.GiaPhongs.Remove(xoaphong);
            db.Phongs.Remove(xoa);
            db.SaveChanges();
            Phong_Load(sender, e);
        }

        private void btnSuaPhong_Click(object sender, EventArgs e)
        {
            string ma = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            using(QuanLyKhachSan.SuaPhong suaphong = new SuaPhong(ma))
                {
                suaphong.ShowDialog();
            }
            Phong_Load(sender,e);
        }

        private void btnThemPhong_Click(object sender, EventArgs e)
        {
            var suaGia = new QuanLyKhachSan.Model.GiaPhong();
            var suaphong = new QuanLyKhachSan.Model.Phong();
            suaphong.TenPhong = textBox1.Text;
            string malp = cmbLoaiPhong.Text.TrimEnd();
            var loaiphong = db.LoaiPhongs.Where(p => p.TenLP == malp).ToList();
            suaGia.MaLP = loaiphong[0].MaLP.TrimEnd();
            string kieu = cmbKieuPhong.Text.TrimEnd();
            var Kieuphong = db.KieuPhongs.Where(p => p.TenKP == kieu).ToList();
            suaGia.MaKP = Kieuphong[0].MaKP.TrimEnd();
            suaGia.Gia = int.Parse(textBox2.Text);
            suaphong.MotaPhong = txtMota.Text;
            db.SaveChanges();
            
            MessageBox.Show("Đã Them thành công phòng!");
            Phong_Load(sender, e);
        }
    }
}
