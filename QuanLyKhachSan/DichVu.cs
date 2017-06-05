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
    public partial class DichVu : Form
    {

        QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
        public DichVu()
        {
            InitializeComponent();
            DichVu_Load();
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            var dichvu = from b in db.DichVus select new {b.MaDV, b.TenDV,b.GiaDV};
            dataGridView1.DataSource = dichvu.ToList();
            dataGridView1.Columns[0].HeaderText = "Mã dịch vụ";

            dataGridView1.Columns[1].HeaderText = "Tên dịch vụ";


            dataGridView1.Columns[2].HeaderText = "Giá dịch vụ";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void DichVu_Load()
        {
            var dichvu = from b in db.DichVus select new { b.MaDV, b.TenDV, b.GiaDV };
            dataGridView1.DataSource = dichvu.ToList();
            dataGridView1.Columns[0].HeaderText = "Mã dịch vụ";

            dataGridView1.Columns[1].HeaderText = "Tên dịch vụ";


            dataGridView1.Columns[2].HeaderText = "Giá dịch vụ";
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void btnThemDV_Click(object sender, EventArgs e)
        {
            int GiaDV;
            if (!int.TryParse(textBox2.Text, out GiaDV)) { MessageBox.Show("Giá dịch vụ là số!"); return; }
            string TenDV = textBox1.Text;
            if(TenDV=="" ) { MessageBox.Show("Nhap lai ten dich vu!"); return; }
            Model.DichVu dv = new Model.DichVu();
            dv.TenDV = TenDV;
            dv.GiaDV = GiaDV;
            db.DichVus.Add(dv);
            db.SaveChanges();
            DichVu_Load(sender,e);
        }

        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            Model.DichVu dv = db.DichVus.SingleOrDefault(p => p.MaDV == id);
            
            db.DichVus.Remove(dv);
            db.SaveChanges();
            DichVu_Load(sender,e);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnSuaDV_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            using
               (QuanLyKhachSan.SuaDV suadv = new SuaDV(id))
            {
                suadv.ShowDialog();
                
            }
            DichVu_Load();
        }
    }
}
