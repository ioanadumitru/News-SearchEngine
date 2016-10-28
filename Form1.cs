using DevAcademyQuest;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DataBaseConnectionManager c = DataBaseConnectionManager.getInstance();
            string key = textBox1.Text;
            List<Article> la = new List<Article>();
            la = c.SearchArticlesByTitle(key);

            int ty = 100;
            int ly = 120;
            int l2y = 160;

            foreach (Article a in la)
            {
                RichTextBox t = new RichTextBox();
                Label l = new Label();
                Label l2 = new Label();
                l.Text = a.Title;
                l2.Text = a.Author;
                t.Text = a.Text;
                t.Size = new Size(1000, 90);
                t.Location = new Point(200, ty);
                l.Location = new Point(40, ly);
                l2.Location = new Point(40, l2y);
                Controls.Add(t);
                Controls.Add(l);
                Controls.Add(l2);
                ty += 100;
                ly += 100;
                l2y += 100;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
