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
    public partial class TraPhong : Form
    {
        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        IEnumerable<MaPhieuDK> tk;
        int songay = 0;
        int tienphong = 0;
        int tiendichvu = 0;
        public TraPhong()
        {
            InitializeComponent();
        }

        private void TraPhong_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button3.Enabled = false;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tkSDT = txtTiemKiemSDT.Text;
            int tkMaPDK;
            tk = from b in db.MaPhieuDKs select b;

            string tkname = txtTiemKiemName.Text;
            string cmnd = txtTimKiemCMND.Text;
            DateTime ngayden = dateTimePicker1.Value;

            if (txtTimKiemMaPDK.Text != "")
            {
                if (!int.TryParse(txtTimKiemMaPDK.Text, out int so))
                {
                    MessageBox.Show("Nhap dung ma phieu dang ki !");
                    return;
                }
                else
                {
                    tkMaPDK = so;
                    tk = from p in tk where p.MaPDK == tkMaPDK select p;
                }
            }
            if (tkname != "")
            {
                tk = from p in tk where p.KhachHang.TenKH == tkname select p;
            }
            if (tkSDT != "")
            {
                tk = from p in tk where p.KhachHang.SDT == tkSDT select p;
            }
            if (dateTimePicker1.Checked)
            {
                tk = from p in tk where p.NgayDen.Value.Date == ngayden.Date select p;
            }
            dataGridView1.DataSource = tk.ToList();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
            button3.Enabled = true;
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            tienphong = 0;
            tiendichvu = 0;
            songay = 0;

            int madk = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd());
            var phieu = db.MaPhieuDKs.SingleOrDefault(p => p.MaPDK == madk);
            txtDatTruoc.Text = phieu.TraTruoc.ToString();
           var  ngay = DateTime.Now.Date - phieu.NgayDen.Value.Date ;
            songay = ngay.Days;
            textBox4.Text = songay.ToString();
            var dichvu = phieu.DichVus;
            foreach (var y in dichvu)
            {
                tiendichvu = tiendichvu + y.GiaDV.Value * songay;
            }
            var phong = phieu.Phongs;

            foreach (var t in phong)
            {
                tienphong = +t.GiaPhong.Gia.Value * songay;
            }
            txtTienDV.Text = tiendichvu.ToString();
            txtTienPhong.Text = tienphong.ToString();
            txtSum.Text = (tienphong + tiendichvu).ToString();
            textBox1.Text = (tiendichvu + tienphong - phieu.TraTruoc).ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                textBox3.Text = (-int.Parse(textBox1.Text)).ToString();
            }
            else if (int.TryParse(textBox2.Text, out int a))
            {
                textBox3.Text = (a - int.Parse(textBox1.Text)).ToString();
            }
            else
            {
                MessageBox.Show("Xin nhap so vao o tien nhan!");
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int madk = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd());
            var phieu = db.MaPhieuDKs.SingleOrDefault(p => p.MaPDK == madk);
            phieu.NgayDi = DateTime.Now;
            var phieutt = new QuanLyKhachSan.Model.PhieuTT();
            phieutt.MaPDK = phieu.MaPDK;
            phieutt.NgayTT = DateTime.Now;
            phieutt.SoNgay = DateTime.Now.Day - phieu.NgayDen.Value.Day;
            phieutt.TongTien = int.Parse(textBox1.Text);
            db.PhieuTTs.Add(phieutt);
            db.SaveChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            tienphong = 0;
            tiendichvu = 0;
            songay = 0;
            tk = null;
            TraPhong_Load(sender, e);
            txtDatTruoc.Clear();
            txtSum.Clear();
            txtTiemKiemName.Clear();
            txtTiemKiemSDT.Clear();
            txtTienDV.Clear();
            txtTienPhong.Clear();
            txtTimKiemCMND.Clear();
            txtTimKiemMaPDK.Clear();
            textBox1.Clear();
        }
    }
}
