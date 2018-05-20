//Downloade from
//Visual C# Kicks - http://www.vcskicks.com/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//Needed
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
//Extra
using Sanford.Multimedia.Midi;
using Sanford.Multimedia.Midi.UI;
//

//
namespace AnimatedForm
{
    public partial class animatedFrm : Form
    {
        //RTR 5.14.2018
        // From L.Sanford
        private bool scrolling = false;
        private bool playing = false;
        private bool closing = false;
        private OutputDevice outDevice;
        private int outDeviceID = 0;
        //private OutputDeviceDialog outDialog = new OutputDeviceDialog();
        //
        //private SequencerDemo.Form1 myForm1;
        //
        //
        MidiHelper1 midiHelper = null;
        //
        private bool firstRun = true;
        // State info
        private int myScreen = 0;


        public animatedFrm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }


        protected override void OnLoad(EventArgs e)
        {

            if (OutputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI output devices available.", "Error!",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                Close();
            }
            else
            {
                try
                {
                    //outDevice = new OutputDevice(outDeviceID);

                    //sequence1.LoadProgressChanged += HandleLoadProgressChanged;
                    //sequence1.LoadCompleted += HandleLoadCompleted;
                    //RTR 5.14.18
                    // load music track
                    //myOpenTrack(this, e);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    Close();
                }
            }

            base.OnLoad(e);
        }


        private void HandleLoadProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            //startButton.Enabled = true;
            //continueButton.Enabled = true;
            //stopButton.Enabled = true;
            //openToolStripMenuItem.Enabled = true;
            //toolStripProgressBar1.Value = 0;

            if (e.Error == null)
            {
                //positionHScrollBar.Value = 0;
                //positionHScrollBar.Maximum = sequence1.GetLength();
            }
            else
            {
                MessageBox.Show(e.Error.Message);
            }
        }

        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (closing)
            {
                return;
            }

            outDevice.Send(e.Message);
            //pianoControl1.Send(e.Message);
        }

        private void HandleChased(object sender, ChasedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        {
            //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
                //pianoControl1.Send(message);
            }
        }

        private void HandlePlayingCompleted(object sender, EventArgs e)
        {
            timer1.Stop();
            playing = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            closing = true;
            //
            //midiHelper.StopAll();
            //
            base.OnClosing(e);
        }

        //protected override void OnClosed(EventArgs e)
        //{
        //    sequence1.Dispose();

        //    if (outDevice != null)
        //    {
        //        outDevice.Dispose();
        //    }

        //    outDialog.Dispose();

        //    base.OnClosed(e);
        //}

        //private void openToolStripMenuItem_Click(object sender, EventArgs e)
        private void myOpenTrack(object sender, EventArgs e)
        {
            string fileName = "../../Resources/Blade_Runner_End_Titles_Unfinished.mid";

            try
            {
                sequencer1.Stop();
                playing = false;
                sequence1.Load(fileName);
                this.Cursor = Cursors.WaitCursor;
                //startButton.Enabled = false;
                //continueButton.Enabled = false;
                //stopButton.Enabled = false;
                //openToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Play(Object stateInfo)
        {
            sequencer1.Start();
            timer1.Start();
            playing = true;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (firstRun)
            {
                firstRun = false;

                //Start animation
                MainLoop(); //start the animation first time window appears
            }
        }

        private void PlayStage1Music()
        {
            //midiHelper.Play("intro",
            //    () =>
            //    {
            //        midiHelper.Play("intro",
            //            () =>
            //            {
            //                midiHelper.Play("intro",
            //                    () =>
            //                    {
            //                    });
            //            });
            //    });
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

            //New Approach
            //string fileName = "../../Resources/Blade_Runner_End_Titles_Unfinished.mid";
            //myForm1 = new SequencerDemo.Form1(fileName);
            //myForm1.Play(this);
            //midiHelper = new MidiHelper1();
            //midiHelper.Load("intro", @"../../Resources/Blade_Runner_2049_-_Main_Theme.mid");
            //midiHelper.Load("intro2", @"../../Resources/Blade_Runner_End_Titles_Unfinished.mid");
            //midiHelper.Load("km_stage2", @"Midis\km_stage2.mid");
            //midiHelper.Load("km_gameover", @"Midis\km_gameover.mid");
            //midiHelper.Load("km_start", @"Midis\km_start.mid");
            //midiHelper.Load("km_crystal", @"Midis\km_crystal.mid");
            //midiHelper.Load("km_ending", @"Midis\km_ending.mid");
            //midiHelper.Play("intro",
            //    () =>
            //    {
            //        PlayStage1Music();
            //    });
            //
            //midiHelper.Play("intro2", null);

            while (!this.closing)   //<=//this.IsDisposed)
            {
                //
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
                            //
                            //RTR 5.14.18
                            //tmp 
                            //midiHelper.Continue("intro2");
                            break;
                        case 1:
                            //maybe
                            //
                            //    this.BackgroundImage = AnimatedForm.Properties.Resources.ff6womg38tlmfdkdythf as Bitmap;
                            //    this.Invalidate(); //refreshes the form
                            //    this.Region = BmpToRegion.Convert(this.BackgroundImage);
                            //    break;
                            //case 2:
                            //    this.BackgroundImage = AnimatedForm.Properties.Resources.br_honeybee as Bitmap;
                            //    this.Invalidate(); //refreshes the form
                            //    this.Region = BmpToRegion.Convert(this.BackgroundImage);
                            //    break;
                            //RTR 5.14.18
                            //tmp 
                            //midiHelper.Continue("intro2");
                            break;
                        case 2:
                            break;
                    }
                    
                    //
                    this.Invalidate(); //refreshes the form
                }

                //RTR 5.14.18
                //Omit
                //frees up the cpu
                //Thread.Sleep(1);
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
            //myScreen++;
            //if (myScreen > 2)
            //{
            //    myScreen = 0;
            //}

            ////halt previous music
            ////
            //midiHelper.Stop("intro2");
            //
            //switch to next track
            //midiHelper.Play("intro2", null);
            //
            //Retain MIDI playback(?)
            //
            closing = true;
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closing = true;
            this.Close();
            Application.Exit();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (!scrolling)
            //{
            //    positionHScrollBar.Value = sequencer1.Position;
            //}
            // RTR 5.14.18
            //
            //int myTrackPosition = sequencer1.Position;
            //
            //try
            //{
            //    playing = true;
            //    sequencer1.Continue();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            // RTR 5.14.18
            // maybe
            // tmp enable
            if (!closing)
            {
                Application.DoEvents();
            }
        }

    }
}
