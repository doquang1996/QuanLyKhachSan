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
            webBrowser1.Navigate(@"C:\Users\Do Quang\documents\visual studio 2017\Projects\QuanLyKhachSan\QuanLyKhachSan\ht.html");
        }

        private void GioiThieu_Load(object sender, EventArgs e)
        {

        }
    }
}
