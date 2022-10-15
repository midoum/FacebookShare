using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Facebook;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using Emgu.CV.Structure;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using Newtonsoft.Json.Linq;


namespace FacebookShare
{
    public partial class Capture : Form
    {
        string link;
        /// VideoCapture vidcap;
        VideoCaptureDevice videocap;
        FilterInfoCollection cameras;

        public Capture()
        {
            

            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
         
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
           
            string[] lines = System.IO.File.ReadAllLines(@"Stettings.txt");
            int camindex = Int32.Parse(lines[0]);
            int resindex = Int32.Parse(lines[1]);
            
            videocap = new VideoCaptureDevice(cameras[camindex].MonikerString);
            videocap.VideoResolution = videocap.VideoCapabilities[resindex];
            videocap.NewFrame +=ProcessFrame;

            videocap.Start();
            
            try
            {
                             
                




                


            } catch (NullReferenceException excpt)
            {
                Console.WriteLine(excpt);
            }


        }


        private void Form2_Load(object sender, EventArgs e)
        {



        }
        private void ProcessFrame(object sender, NewFrameEventArgs arg)
        {
            pictureBox1.Image = (Bitmap)arg.Frame.Clone();

        }

        private void button1_Click(object sender, EventArgs e)

        {

            try
            {
                string[] lines = System.IO.File.ReadAllLines(@"Stettings.txt");
                string token = lines[2];
                link = DateTime.UtcNow.ToString("MM-dd-yyyy-hh-mm-ss");
                var fb = new FacebookClient(token);
                pictureBox1.Image.Save(@"D:\Temp\"+link+".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);


              
         

                int time;

                if (radioButton1.Checked)
                {
                    time = 5;
                }
                else if (radioButton2.Checked)
                {
                    time = 10;
                }
                else
                {
                    time = 20;
                }
                Timer(fb, time);

           

            }catch(Exception f)
            {
                MessageBox.Show(f.ToString());
            }



        }
        public static void UploadImage(FacebookClient fb,string link)
        {

            string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
            dynamic result = fb.Batch(
            
                new FacebookBatchParameter(HttpMethod.Post, "/1927328194088299/photos", new Dictionary<string, object> { { "message", date }, { "capture"+date, new FacebookMediaObject { ContentType = "image/jpeg", FileName = "capture " }.SetValue(File.ReadAllBytes(@"D:\Temp\"+link+".jpeg")) } })
            );
            File.Delete(@"D:\Temp\" + link + ".jpeg");
            
        }
       
        public async void Timer(FacebookClient fb,int time)
        {
            label1.Show();
            for (int profileNumber = 1; profileNumber <= time; profileNumber++)
            {
                label1.Text=profileNumber.ToString();
                await Task.Delay((1000));
            }
            label1.Text = "";
            UploadImage(fb,link);
       

        }
        

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            videocap.NewFrame -= new NewFrameEventHandler(ProcessFrame);
            
            videocap.SignalToStop();
            videocap.WaitForStop();

        }
        private void button2_Click(object sender, EventArgs e)
        {
           
           

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            videocap.NewFrame -= new NewFrameEventHandler(ProcessFrame);

            videocap.SignalToStop();
            videocap.WaitForStop();
            this.Hide();
            Form form = new Settings();
            form.Show();
        }
    }
}
