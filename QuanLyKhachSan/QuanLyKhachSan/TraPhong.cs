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
using iTextSharp.text.pdf;
using iTextSharp.text;
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
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tkSDT = txtTiemKiemSDT.Text;
            int tkMaPDK;
            tk = from b in db.MaPhieuDKs
                  from a in db.PhieuTTs.Where(p=>p.MaPDK==b.MaPDK ).DefaultIfEmpty()
                   where b.MaPDK != a.MaPDK

                 
                 select b;

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
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            tienphong = 0;
            tiendichvu = 0;
            songay = 0;

            int madk = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd());
            var phieu = db.MaPhieuDKs.SingleOrDefault(p => p.MaPDK == madk);
            txtDatTruoc.Text = phieu.TraTruoc.ToString();
            var ngay = DateTime.Now.Date - phieu.NgayDen.Value.Date;
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
            var khach = db.KhachHangs.SingleOrDefault(a => a.MaKH == phieu.MaKH);
            phieu.NgayDi = DateTime.Now;
            var phieutt = new QuanLyKhachSan.Model.PhieuTT();
            phieutt.MaPDK = phieu.MaPDK;
            phieutt.NgayTT = DateTime.Now;
            phieutt.SoNgay = DateTime.Now.Day - phieu.NgayDen.Value.Day;
            phieutt.TongTien = int.Parse(textBox1.Text);
            db.PhieuTTs.Add(phieutt);
            var khachsan = db.ThongTinKS.ToList();
            db.SaveChanges();
            //
            Document doc = new Document(iTextSharp.text.PageSize.A6, 20, 20, 10, 10);

            PdfWriter pdf = PdfWriter.GetInstance(doc, new System.IO.FileStream(phieu.MaPDK.ToString() + ".pdf", System.IO.FileMode.Create));
            doc.Open();
            //
            BaseFont font = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font head = new iTextSharp.text.Font(font, 16, 1, BaseColor.GRAY);
            Paragraph heading = new Paragraph();
            heading.Alignment = Element.ALIGN_JUSTIFIED;
            heading.Add(new Chunk("Hoa don", head));
            doc.Add(heading);
            //
            BaseFont day = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font dayfont = new iTextSharp.text.Font(font, 8, 1, BaseColor.GRAY);
            Paragraph date = new Paragraph();
            date.Alignment = Element.ALIGN_RIGHT;
            date.Add(new Chunk("Ngay thanh toan" + DateTime.Now.ToShortDateString(), dayfont));
            doc.Add(date);
            //
            Paragraph pa = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100F, BaseColor.BLACK, Element.ALIGN_LEFT, Element.TITLE)));
            doc.Add(pa);
            Paragraph khachhang = new Paragraph("Khach hang: " + khach.TenKH);
            doc.Add(khachhang);
            string gt;
            if (khach.GioiTinh == true) gt = "nam";
            else gt = "nu";
            Paragraph gioitinh = new Paragraph("Gioi tinh: " + gt);
            doc.Add(gioitinh);
            Paragraph lienlac = new Paragraph("SDT: " + khach.SDT);
            doc.Add(lienlac);
            Paragraph cmd = new Paragraph("So CMND(Ho chieu): " + khach.CMND);
            doc.Add(cmd);
            Paragraph email = new Paragraph("Email: " + khach.Email);
            doc.Add(email);

            Paragraph thanhtoan = new Paragraph("Tien phong: " + txtTienPhong.Text + "\n Tien dich vu:" + txtTienDV.Text + "\t So ngay:" + textBox4.Text + "\n Tong tien: " + txtSum.Text + "\n Tien dat truoc: " + txtDatTruoc.Text + "/n So tien thanh toan: "
                + textBox2.Text + "/t So tien tra lai: " + textBox3.Text);
            doc.Add(thanhtoan);
            doc.Close();
            button2_Click(sender, e);
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
