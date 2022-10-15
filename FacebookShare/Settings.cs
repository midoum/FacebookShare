using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FacebookShare
{
    public partial class Settings : Form

    {
        VideoCaptureDevice videocap;
        FilterInfoCollection cameras;
        public Settings()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form2_FormClosed);
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo cam in cameras)
            {
                comboBox1.Items.Add(cam.Name);

            }
            
            comboBox1.SelectedIndexChanged += new EventHandler(ComboBox1_ChangedHandler);
            comboBox1.SelectedIndex = 0;
            string[] lines = System.IO.File.ReadAllLines(@"Stettings.txt");
            int camindex = Int32.Parse(lines[0]);
            int resindex = Int32.Parse(lines[1]);
            string token = lines[2];
            comboBox1.SelectedIndex = camindex;
            comboBox2.SelectedIndex = resindex;
            textBox1.Text = token;
              
            



        }
        private void ComboBox1_ChangedHandler(object sender, System.EventArgs e)
        {
             
            
            int cindex = comboBox1.SelectedIndex;
            videocap = new VideoCaptureDevice(cameras[cindex].MonikerString);
          
                comboBox2.Items.Clear();
            
            
            for (int i = 0; i < videocap.VideoCapabilities.Length; i++)
            {
                comboBox2.Items.Add(videocap.VideoCapabilities[i].FrameSize.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter str = new StreamWriter(@"Stettings.txt");
     
             str.WriteLine((comboBox1.SelectedIndex).ToString());
            str.WriteLine((comboBox2.SelectedIndex).ToString());
            str.WriteLine(textBox1.Text);
            str.Close();
           

        }
        private void Form2_FormClosed(Object sender , EventArgs e)
        {
            this.Hide();
            Form form = new Acceuil();
            form.Show();
        }
    }
}
