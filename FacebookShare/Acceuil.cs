using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FacebookShare
{
    public partial class Acceuil : Form
    {
        public Acceuil()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          Form   Setting = new Settings();
            this.Hide();
            Setting.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form form = new Capture();
            form.Show();
        }
    }
}
