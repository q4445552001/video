using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection CaptureDevice; // list of webcam
        private VideoCaptureDevice FinalFrame = null;
        Bitmap image, imageToProcess, imgGrayscale, imgOtsuThreshold;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, NewFrameEventArgs eventArgs)
        {
            {
                Bitmap img = (Bitmap)eventArgs.Frame.Clone();
                pictureBox1.Image = img;
                image = img;
            }

            /*{
            Bitmap imgExtractChannelR, resultImage, imgOtsuThreshold, imgInvert, imgcolorfilter, imgSubtract,img2;
                img2 = (Bitmap)eventArgs.Frame.Clone();
                ExtractChannel filter = new ExtractChannel(RGB.R);
                imgExtractChannelR = filter.Apply(img2);
            }
            {
                Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                imgGrayscale = filter.Apply(img2);
            }
            {
                Subtract filter = new Subtract(imgGrayscale);
                resultImage = filter.Apply(imgExtractChannelR);
            }
            {
                OtsuThreshold filter = new OtsuThreshold();
                imgOtsuThreshold = filter.Apply(resultImage);
            }
            {
                Invert filter = new Invert();
                imgInvert = filter.Apply(imgOtsuThreshold);
            }
            {
                GrayscaleToRGB colorfilter = new GrayscaleToRGB();
                imgcolorfilter = colorfilter.Apply(imgInvert);
                Subtract filtersub = new Subtract(imgcolorfilter);
                imgSubtract = filtersub.Apply(img2);
            }
            pictureBox2.Image = imgSubtract;
            pictureBox3.Image = imgExtractChannelR;
            pictureBox4.Image = imgGrayscale;
            pictureBox5.Image = resultImage;
            pictureBox6.Image = imgOtsuThreshold;
            pictureBox7.Image = imgInvert;
          {
                Bitmap img2 = (Bitmap)eventArgs.Frame.Clone();
                Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
                imgGrayscale = filter.Apply(img2);
            }
            {
                OtsuThreshold filter = new OtsuThreshold();
                imgOtsuThreshold = filter.Apply(imgGrayscale);
                pictureBox2.Image = imgOtsuThreshold;
            }*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CaptureDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            FinalFrame = new VideoCaptureDevice(CaptureDevice[0].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(Form1_Load);
            FinalFrame.Start();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            FinalFrame.SignalToStop();
            timer1.Enabled = false;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            timer1.Enabled = true;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {

            timer1.Interval = 100;
            int stop1 = 0, stop2 = 0, stop3 = 0, stop4 = 0, stop5 = 0, stop6 = 0, x1 = 0, y1 = 0, x2 = 0, y2 = 0, x3 = 0, y3 = 0, x4 = 0, y4 = 0;
            imageToProcess = image;

            for (int j = 0; j < image.Height; j++)
            {
                for (int i = 0; i < image.Width; i++)
                {
                    Color pixelcolor = image.GetPixel(i, j);

                    if (pixelcolor.R < 50)
                    {
                        textBox1.Text = Convert.ToString(i);
                        textBox2.Text = Convert.ToString(j);
                        y1 = j;
                        x1 = i;
                        j = image.Height;
                        i = image.Width;
                    }
                }
            }

            for (int j = image.Height - 10; j > stop1; j--)
            {
                for (int i = image.Width - 10; i > stop2; i--)
                {
                    Color pixelcolor = image.GetPixel(i - 1, j - 1);

                    if (pixelcolor.R < 50)
                    {
                        textBox3.Text = Convert.ToString(i);
                        textBox4.Text = Convert.ToString(j);
                        x2 = i;
                        y2 = j;
                        stop1 = image.Height;
                        stop2 = image.Width;
                    }
                }
            }

            for (int j = stop3; j < image.Width; j++)
            {
                for (int i = image.Height - 10; i > stop4; i = i - 1)
                {
                    Color pixelColor = image.GetPixel(j, i);

                    if (pixelColor.R < 36)
                    {
                        x3 = j;
                        y3 = i;
                        stop3 = image.Height;
                        stop4 = image.Width;
                    }
                }
            }

            for (int j = image.Width - 10; j > stop5; j = j - 1)
            {
                for (int i = image.Height - 10; i > stop6; i = i - 1)
                {
                    Color pixelColor = image.GetPixel(j, i);

                    if (pixelColor.R < 40)
                    {
                        x4 = j;
                        y4 = i;
                        stop5 = image.Height;
                        stop6 = image.Width;
                    }
                }
            }
            {
                int sum = y2 - y1;
                textBox5.Text = Convert.ToString(sum);
            } textBox1.Text = Convert.ToString(x1);
            textBox2.Text = Convert.ToString(y1);
            textBox3.Text = Convert.ToString(x2);
            textBox4.Text = Convert.ToString(y2);
            Bitmap imgone = new Bitmap(image);
            using (Graphics g = Graphics.FromImage(imgone))
            {
                g.DrawLine(new Pen(Color.Green), x3, y1, x4, y1);
                g.DrawLine(new Pen(Color.Green), x4, y2, x3, y2);
                g.DrawLine(new Pen(Color.Blue), x1, y1, x1, y2);
            }
            pictureBox2.Image = imgone;
        }
    }
}
