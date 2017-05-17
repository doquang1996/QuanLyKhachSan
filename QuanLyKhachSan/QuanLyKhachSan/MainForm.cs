using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using QuanLyKhachSan.Model;

namespace QuanLyKhachSan
{
    public partial class MainForm : Form
    {
        Image closeImg, closeImgAct;

        public MainForm()
        {
            InitializeComponent();
        }

        private int KTfrmTonTai(Form frm)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                if (tabControl1.TabPages[i].Text == frm.Text.Trim()) return i;

            }
            return -1;
        }
        private void addTabPage(Form frm)
        {
            int p = KTfrmTonTai(frm);
            if (p >= 0)
            {
                if (tabControl1.TabPages[p] == tabControl1.SelectedTab)
                    MessageBox.Show("Tab dang duoc mo");
                else
                    tabControl1.SelectedTab = tabControl1.TabPages[p];

            }
            else
            {
                TabPage newtab = new TabPage(frm.Text.Trim());
                tabControl1.TabPages.Add(newtab);
                frm.TopLevel = false;
                frm.Parent = newtab;
                tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabCount - 1];
                frm.Show();
                frm.Dock = DockStyle.Fill;
            }

        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle rect = tabControl1.GetTabRect(e.Index);

            Rectangle imagerect = new Rectangle(rect.Right - closeImg.Width, rect.Top + (rect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height);
            Font f;
            Brush br = Brushes.Black;
            StringFormat strF = new StringFormat(StringFormat.GenericDefault);

            if (tabControl1.SelectedTab == tabControl1.TabPages[e.Index])
            {
                e.Graphics.DrawImage(closeImgAct, imagerect);
                f = new Font("Time New Roman", 9, FontStyle.Bold);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, f, br, rect, strF);

            }
            else
            {
                e.Graphics.DrawImage(closeImg, imagerect);
                f = new Font("Time New Roman", 10, FontStyle.Regular);
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, f, br, rect, strF);

            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                Rectangle rect = tabControl1.GetTabRect(i);
                Rectangle imagerect = new Rectangle(rect.Right - closeImg.Width, rect.Top + (rect.Height - closeImg.Height) / 2, closeImg.Width, closeImg.Height);
                if (imagerect.Contains(e.Location))
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            addTabPage(new Phong());
        }

        private void btnDichVu_Click(object sender, EventArgs e)
        {
            addTabPage(new DichVu());
        }

        private void btnDatPhong_Click(object sender, EventArgs e)
        {
            addTabPage(new DatPhong());
        }

        private void btnNhanPhong_Click(object sender, EventArgs e)
        {
            addTabPage(new NhanPhong());
        }

        private void btnTraPhong_Click(object sender, EventArgs e)
        {
            addTabPage(new TraPhong());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addTabPage(new TimKiem());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addTabPage(new GioiThieu());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Size mysize = new System.Drawing.Size(20, 20);
            Bitmap bt = new Bitmap(Properties.Resources._1490869516_close, mysize);
            Bitmap bt2 = new Bitmap(Properties.Resources._1490869512_f_cross_256, mysize);
            closeImg = bt;
            closeImgAct = bt2;
            tabControl1.Padding = new Point(30);
            QuanLyKhachSanEntities3 db = new QuanLyKhachSanEntities3();
            var tenkhachsan = db.ThongTinKS.Select(p => p.TenKS).ToList() ;
            
            label1.Text +=" "+ tenkhachsan[0].ToString();
        }

        //private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        //{

        //}
    }
}
