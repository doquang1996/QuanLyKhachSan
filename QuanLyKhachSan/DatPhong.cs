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
    public partial class DatPhong : Form
    {
        QuanLyKhachSan.Model.QuanLyKhachSanEntities3 db = new Model.QuanLyKhachSanEntities3();
        Model.KhachHang khachhang = new Model.KhachHang();
        Model.MaPhieuDK phieudk = new Model.MaPhieuDK();
        Model.Phong phong = new Model.Phong();
        List<Model.Phong> listphong = new List<Model.Phong>();
        List<Model.DichVu> dichvu = new List<Model.DichVu>();
        Model.DichVu dichvuchon = new Model.DichVu();
        int sophong = 0;
        int tienphong = 0;
        int tiendichvu = 0;
        public DatPhong()
        {
            InitializeComponent();
        }
        private void huydatphong()
        {
            textBox1.Text=null;
            textBox2.Text=null;
            textBox7.Text=null;
            comboBox1.Text = null ;
            textBox5.Text=null;
           textBox6.Text=null;
            dateTimePicker1.Value=DateTime.Now;
            textBox9.Text=null;
            textBox10.Text=null;
            textBox11.Text=null;
            foreach (DataGridViewRow i in dataGridView2.Rows)
                dataGridView2.Rows.Remove(i);

            foreach (DataGridViewRow i in dataGridView4.Rows)
                dataGridView4.Rows.Remove(i);
            tiendichvu = 0;
            tienphong = 0;
            txtTienDV.Text = tiendichvu.ToString();
            txtDatTruoc.Text = null;
            txtSum.Text = null;
            txtTienPhong.Text = tienphong.ToString();
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            khachhang = null;
            phieudk = null;
            
        }
        private void datphong()
        {
            khachhang.TenKH = textBox1.Text;
            khachhang.CMND = textBox2.Text;
            khachhang.SDT = textBox7.Text;
            if (comboBox1.SelectedIndex == 0) khachhang.GioiTinh = false; else khachhang.GioiTinh = true;
            khachhang.QuocTich = textBox5.Text;
            khachhang.Email = textBox6.Text;
            phieudk.NgayDen = dateTimePicker1.Value;
            phieudk.NguoiLon = int.Parse(textBox9.Text);
            phieudk.TreEm = int.Parse(textBox10.Text);
            phieudk.SoPhong = int.Parse(textBox11.Text);
            for (int i = 0; i < dataGridView4.Rows.Count; i++)
            {
                string v = dataGridView4.Rows[i].Cells[0].Value.ToString().TrimEnd();
                dichvuchon = db.DichVus.SingleOrDefault(p => p.TenDV == v);
                dichvu.Add(dichvuchon);
            }
            phieudk.DichVus = dichvu;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                string n = dataGridView2.Rows[i].Cells[0].Value.ToString().TrimEnd();
                phong = db.Phongs.SingleOrDefault(p => p.TenPhong == n);
                phong.TrangThai = true;
                listphong.Add(phong);
            }
            phieudk.Phongs = listphong;
            if (int.TryParse(txtDatTruoc.Text, out int a)) phieudk.TraTruoc = a;
            else MessageBox.Show("Xin nhập số vào ô trả trước!");
            phieudk.ChuThich = textBox12.Text;
            db.KhachHangs.Add(khachhang);
            db.SaveChanges();
            var makh = db.KhachHangs.SingleOrDefault(p => p.TenKH == textBox1.Text);
            phieudk.KhachHang = makh;
            db.MaPhieuDKs.Add(phieudk);
            db.SaveChanges();
            MessageBox.Show("Luu thanh cong!");
            huydatphong();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSum_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow it in dataGridView3.SelectedRows)
            {
                if (kiemtra(it, dataGridView4, 1))
                {
                    int n = dataGridView4.Rows.Add();
                    dataGridView4.Rows[n].Cells[0].Value = it.Cells[1].Value.ToString();
                    dataGridView4.Rows[n].Cells[1].Value = it.Cells[2].Value.ToString();
                    tiendichvu = tiendichvu + int.Parse(it.Cells[2].Value.ToString());
                }

            }
            txtSum.Text = (tienphong + tiendichvu).ToString();
            txtTienDV.Text = tiendichvu.ToString();
        }
        public bool kiemtra(DataGridViewRow a, DataGridView b, int c)
        {
            if (b == null) return true;
            for (int i = 0; i < b.RowCount; i++)
            {
                if (b.Rows[i].Cells[0].Value.ToString() == a.Cells[c].Value.ToString()) return false;
            }
            return true;


        }
        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void btnThemGhiChu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[3];
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }

        private void DatPhong_Load(object sender, EventArgs e)
        {

            var DanhsachPhong = from p in db.Phongs
                                join b in db.GiaPhongs on p.MaPhong equals b.MaPhong
                                join a in db.KieuPhongs on b.MaKP equals a.MaKP
                                join c in db.LoaiPhongs on b.MaLP equals c.MaLP
                                where p.TrangThai == false
                                select new
                                {
                                    TenPhong = p.TenPhong,
                                    Loai = c.TenLP,
                                    Kieu = a.TenKP,
                                    Gia = b.Gia,
                                    MaPhong = b.MaPhong
                                };
            dataGridView1.DataSource = DanhsachPhong.ToList();
            dataGridView1.Columns[0].HeaderText = "Tên phòng";

            dataGridView1.Columns[1].HeaderText = "Loại phòng";

            dataGridView1.Columns[2].HeaderText = "Kiểu phòng";

            dataGridView1.Columns[3].HeaderText = "Giá phòng";
            dataGridView1.AutoResizeColumns();
            var dichvu = from b in db.DichVus select new { b.MaDV, b.TenDV, b.GiaDV };
            var listdichvu = dichvu.ToList();
            dataGridView3.DataSource = listdichvu;
            dataGridView3.Columns[0].HeaderText = "Mã dịch vụ";

            dataGridView3.Columns[1].HeaderText = "Tên dịch vụ";


            dataGridView3.Columns[2].HeaderText = "Giá dịch vụ";
            dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow it in dataGridView1.SelectedRows)
            {

                if (kiemtra(it, dataGridView2, 0))
                {
                    int n = dataGridView2.Rows.Add();
                    dataGridView2.Rows[n].Cells[0].Value = it.Cells[0].Value.ToString();
                    dataGridView2.Rows[n].Cells[1].Value = it.Cells[3].Value.ToString();
                    sophong++;
                    tienphong = tienphong + int.Parse(it.Cells[3].Value.ToString());
                }

            }
            textBox11.Text = sophong.ToString();
            txtTienPhong.Text = tienphong.ToString();

            txtSum.Text = (tienphong + tiendichvu).ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridView4.SelectedRows)
            {
                tiendichvu = tiendichvu - int.Parse(i.Cells[1].Value.ToString());
                dataGridView4.Rows.Remove(i);
            }
            txtSum.Text = (tienphong + tiendichvu).ToString();
            txtTienDV.Text = tiendichvu.ToString();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow i in dataGridView2.SelectedRows)
            {

                sophong--;
                tienphong = tienphong - int.Parse(i.Cells[1].Value.ToString());
                dataGridView2.Rows.Remove(i);
            }
            textBox11.Text = sophong.ToString();
            txtTienPhong.Text = tienphong.ToString();

            txtSum.Text = (tienphong + tiendichvu).ToString();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            datphong();
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            datphong();
        }
       
        private void button9_Click(object sender, EventArgs e)
        {
            huydatphong();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            huydatphong();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            huydatphong();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            huydatphong();
        }
    }
}
