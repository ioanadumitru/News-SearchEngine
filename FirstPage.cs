using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class FirstPage : Form
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Profile p = new Profile();
            p.FormClosed += (s, args) => this.Show();
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            datesearch d = new datesearch();
            d.Visible = true;
            d.FormClosed += (s, args) => this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 f = new Form1();
            f.Visible = true;
            f.FormClosed += (s, args) => this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            AuthorSearchcs a = new AuthorSearchcs();
            a.Visible = true;
            a.FormClosed += (s, args) => this.Show();
        }
    }
}
