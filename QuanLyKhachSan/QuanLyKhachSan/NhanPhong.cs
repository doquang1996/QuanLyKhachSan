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
    public partial class NhanPhong : Form
    {

        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        IEnumerable< MaPhieuDK> tk ;
        public NhanPhong()
        {
            InitializeComponent();
            
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tkSDT = txtTiemKiemSDT.Text;
            int tkMaPDK;
          var  tk = from b in db.MaPhieuDKs
                 from a in db.PhieuTTs.Where(p => p.MaPDK == b.MaPDK).DefaultIfEmpty()
                 where b.MaPDK != a.MaPDK


                 select 
                 new {
                     b.MaPDK,b.MaKH,b.ChuThich,b.KhachHang.TenKH,b.KhachHang.SDT,b.KhachHang.QuocTich,b.KhachHang.CMND,b.KhachHang.Email,b.NgayDen
                 };


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
                tk = from p in tk where p.TenKH == tkname select p;
            }
            if(tkSDT!="")
            {
                tk= from p in tk where p.SDT == tkSDT select p;
            }
            if(dateTimePicker1.Checked)
            {
                tk = from p in tk where p.NgayDen.Value.Date == ngayden.Date select p;
            }
            dataGridView1.DataSource = tk.ToList();
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
           int madk= int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString().TrimEnd());
            var phieu = db.MaPhieuDKs.SingleOrDefault(p => p.MaPDK == madk);
            phieu.NgayDen = DateTime.Now;
            phieu.ChuThich += "Đã nhận phòng";
            db.SaveChanges();
            MessageBox.Show("Nhậ phòng thành công");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
