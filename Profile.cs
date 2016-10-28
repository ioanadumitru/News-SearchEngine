using DevAcademyQuest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Profile : Form
    {
        byte[] bytes;

        public Profile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            saveFileDialog1.FileName = openFileDialog1.FileName;

            if (File.Exists(saveFileDialog1.FileName))
            {
                bytes = File.ReadAllBytes(saveFileDialog1.FileName);
                var ms = new MemoryStream(bytes);
                pictureBox1.Size = new Size(400, 400);
                pictureBox1.Image = new Bitmap(new MemoryStream(bytes));
                pictureBox1.Image = Image.FromFile(saveFileDialog1.FileName);
            }

          }

        private void save_Click(object sender, EventArgs e)
        {
            DataBaseConnectionManager c = DataBaseConnectionManager.getInstance();
            string username = userBox.Text;
            string mail = mailBox.Text;
            User user = new User(username, mail, bytes);
            c.AddUser(user);
            this.Close();
        }
    }
}
