//Downloade from
//Visual C# Kicks - http://www.vcskicks.com/
using System;
using System.Drawing;
using System.Windows.Forms;
//Needed
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
//Extra

namespace AnimatedForm
{
    public partial class animatedFrm : Form
    {
        public animatedFrm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }



        private bool firstRun = true;
        // State info
        private int myScreen = 0;

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (firstRun)
            {
                firstRun = false;
                MainLoop(); //start the animation first time window appears
            }
        }

        private void MainLoop()
        {
            //FPS set up
            double FPS = 30.0;
            long ticks1 = 0;
            long ticks2 = 0;
            double interval = (double)Stopwatch.Frequency / FPS;
            //

            //this.BackgroundImage = AnimatedForm.Properties.Resources.blockanimation as Bitmap;
            this.BackgroundImage = AnimatedForm.Properties.Resources.te9 as Bitmap;
            //this.BackgroundImage = global::AnimatedForm.Properties.Resources.te9;

            this.Region = BmpToRegion.Convert(this.BackgroundImage);

            GifImage gif = new GifImage(this.BackgroundImage);
            gif.ReverseAtEnd = true;

            while (!this.IsDisposed)
            {
                Application.DoEvents();

                ticks2 = Stopwatch.GetTimestamp();

                if (ticks2 >= ticks1 + interval)
                {
                    ticks1 = Stopwatch.GetTimestamp();

                    //Actions

                    // RTR 3.25.2018
                    // State-driven
                    switch (this.myScreen)
                    {

                        case 0:
                            this.BackgroundImage = gif.GetNextFrame();
                            this.Region = BmpToRegion.Convert(this.BackgroundImage);
                            break;
                        case 1:
                            //maybe
                            this.BackgroundImage = AnimatedForm.Properties.Resources.ff6womg38tlmfdkdythf as Bitmap;
                            this.Invalidate(); //refreshes the form
                            this.Region = BmpToRegion.Convert(this.BackgroundImage);
                            break;
                        case 2:
                            this.BackgroundImage = AnimatedForm.Properties.Resources.br_honeybee as Bitmap;
                            this.Invalidate(); //refreshes the form
                            this.Region = BmpToRegion.Convert(this.BackgroundImage);
                            break;
                    }
                    
                    //
                    this.Invalidate(); //refreshes the form
                }
                Thread.Sleep(1); //frees up the cpu
            }
        }

        internal class GifImage
        {
            private Image gifImage;
            private FrameDimension dimension;
            private int frameCount;
            private int currentFrame;
            private bool reverse;
            private int step = 1;

            public GifImage(string path)
            {
                gifImage = Image.FromFile(path); //initialize
                dimension = new FrameDimension(gifImage.FrameDimensionsList[0]); //gets the GUID
                frameCount = gifImage.GetFrameCount(dimension); //total frames in the animation
            }

            public GifImage(Image image)
            {
                gifImage = image; //initialize
                dimension = new FrameDimension(gifImage.FrameDimensionsList[0]); //gets the GUID
                frameCount = gifImage.GetFrameCount(dimension); //total frames in the animation
            }

            public bool ReverseAtEnd //whether the gif should play backwards when it reaches the end
            {
                get { return reverse; }
                set { reverse = value; }
            }

            public Image GetNextFrame()
            {
                currentFrame += step;

                if (currentFrame >= frameCount || currentFrame < 1) //if the animation reaches a boundary...
                {
                    if (reverse)
                    {
                        step *= -1; //...reverse the count
                        currentFrame += step; //apply it
                    }
                    else
                        currentFrame = 0; //...or start over
                }

                return GetFrame(currentFrame);
            }

            public Image GetFrame(int index)
            {
                gifImage.SelectActiveFrame(dimension, index); //find the frame
                return (Image)gifImage.Clone(); //return a copy of it
            }
        } //-----Article on this ----- http://www.vcskicks.com/csharp_animated_gif.html

        private void visualCKicksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // RTR 2.18.2018
            //hide
            //Hide();
            //System.Diagnostics.Process.Start("http://www.vcskicks.com/");
            //
            //=>
            // Run graphics code (e.g., in new Window)
            //

            //temp
            myScreen++;
            if (myScreen > 2)
            {
                myScreen = 0;
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        Point lastPoint;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void animatedFrm_Load(object sender, EventArgs e)
        {

        }
    }
}