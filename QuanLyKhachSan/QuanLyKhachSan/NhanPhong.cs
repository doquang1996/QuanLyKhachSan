﻿using QuanLyKhachSan.Model;
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
            tk = from b in db.MaPhieuDKs
                 where b.PhieuTTs == null
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
            if(tkSDT!="")
            {
                tk= from p in tk where p.KhachHang.SDT == tkSDT select p;
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
            db.SaveChanges();
            MessageBox.Show("Nhan phong thanh cong");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }
    }
}
