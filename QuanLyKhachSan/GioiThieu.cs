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
    public partial class GioiThieu : Form
    {
        public GioiThieu()
        {
            InitializeComponent();
            webBrowser1.Navigate(@"C:\Users\namtv1996\Desktop\GITHUB-PROJECT\Quan Ly Khach San - Đỗ Quảng\QuanLyKhachSan\QuanLyKhachSan\ht.html");
        }

        private void GioiThieu_Load(object sender, EventArgs e)
        {

        }
    }
}
