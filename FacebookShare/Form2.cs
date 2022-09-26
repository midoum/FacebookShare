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


namespace FacebookShare
{
    public partial class Form2 : Form
    {
        string link;
        /// VideoCapture vidcap;
        VideoCaptureDevice videocap;
        FilterInfoCollection cameras;

        public Form2()
        {
            

            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            pictureBox2.Visible = false;
            cameras = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo camerainfo in cameras)
            {
                comboBox1.Items.Add(camerainfo.Name);
            }
            
            videocap = new VideoCaptureDevice(cameras[1].MonikerString);
            videocap.VideoResolution = videocap.VideoCapabilities[7];
            for (int i = 0; i<videocap.VideoCapabilities.Length;i++) {
                MessageBox.Show(videocap.VideoCapabilities[i].FrameSize.ToString()); 
            }
            videocap.NewFrame +=ProcessFrame;

            videocap.Start();
            
            try
            {
               // /vidcap = new VideoCapture(1);
                ///vidcap.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, 1080);
                ///vidcap.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, 1940);
                ///vidcap.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, 25);
               
               
                 //MessageBox.Show(vidcap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps).ToString());
                 //  MessageBox.Show(vidcap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth).ToString());
                 // MessageBox.Show(vidcap.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Saturation).ToString());
                
                pictureBox1.Controls.Add(label1);




                


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
            /// Bitmap bmp;
            /// Mat ImageFrame = vidcap.QueryFrame();

            /// if (ImageFrame != null)
            /// {
            ///    try
            ///    {

            ///         bmp = ImageFrame.ToBitmap();
            ///        pictureBox1.Image = bmp;



            ///     }
            ///   catch(Exception ex)
            /// {
            ///    MessageBox.Show(ex.ToString());
            /// }



            //pictureBox1.Image = histeq.ToBitmap();

            /// }
             
            pictureBox1.Image = (Bitmap)arg.Frame.Clone();

        }

        private void button1_Click(object sender, EventArgs e)

        {

            try
            {
                link = DateTime.UtcNow.ToString("MM-dd-yyyy-hh-mm-ss");
                var fb = new FacebookClient("EAAFXZCBvSn5kBABsIyaqF0VoG4K8zMJvGYvraASogFhTLNcoAQetkZC5jqSXDdiaHkpFRRIivFmESjvvvZBxeXnzDABSAO0G7U2XUnfT2hrYEYVupBGwDaalSNkTem7eavtBKBdZBI3iwCiZAwjYcaOOumTmasMryIJemPUquMCqxfe2YJqLLmt5aZBmTZCgfzq5RAyeKbWBgZDZD");
                pictureBox1.Image.Save(@"D:\Temp\"+link+".jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);


              
                pictureBox2.Visible = true;
                pictureBox2.Enabled = true;
                pictureBox1.Controls.Add(pictureBox2);

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
            
        }
       
        public async void Timer(FacebookClient fb,int time)
        {
            for (int profileNumber = 1; profileNumber <= time; profileNumber++)
            {
                label1.Text=profileNumber.ToString();
                await Task.Delay((1000));
            }
            label1.Text = "";
            UploadImage(fb,link);
            pictureBox2.Visible = false;

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
    }
}
