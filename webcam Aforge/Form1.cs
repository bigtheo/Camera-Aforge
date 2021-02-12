using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;

namespace webcam_Aforge
{
    public partial class Form1 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCapture;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach(FilterInfo filter in filterInfoCollection)
            {
                cbxWebCam.Items.Add(filter.Name);
                cbxWebCam.SelectedIndex = 0;
                videoCapture = new VideoCaptureDevice();
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            videoCapture = new VideoCaptureDevice(filterInfoCollection[cbxWebCam.SelectedIndex].MonikerString);
            videoCapture.NewFrame += VideoCapture_NewFrame;
            videoCapture.Start();
        }

        private void VideoCapture_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pbxImage.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoCapture = new VideoCaptureDevice();
            videoCapture.SignalToStop();
            videoCapture.Stop();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
            videoCapture.SignalToStop();
            videoCapture.Stop();
            pictureBox1.Image = pbxImage.Image;
        }
    }
}
