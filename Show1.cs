using System;
using System.Windows.Forms;
using Sanford.Multimedia.Midi;










//
namespace AnimatedForm
{
    public partial class Show1 : Form
    {
        private bool scrolling = false;

        private bool playing = false;

        private bool closing = false;

        private OutputDevice outDevice;

        private int outDeviceID = 0;
        private Sanford.Multimedia.Midi.Sequence sequence1;
        private Sanford.Multimedia.Midi.Sequencer sequencer1;

        public Show1()
        {
            InitializeComponent();

            //ShowDialog();
            //    Button btnOK = new Button();
            //    btnOK.DialogResult = DialogResult.OK;
            //    btnOK.Text = "OK";
            //    btnOK.Click += (s, e) => Close();
            //    btnOK.Parent = this;

            //    Button btnCancel = new Button();
            //    btnCancel.DialogResult = DialogResult.Cancel;
            //    btnCancel.Text = "Cancel";
            //    btnCancel.Click += (s, e) => Close();
            //    btnCancel.Parent = this;

            //    AcceptButton = btnOK;
            //    CancelButton = btnCancel;

            //this.Show();
            //
            //sequencer1 = new Sequencer();
            //sequence1 = new Sequence();
            //
            // 
            // sequence1
            // 
            //this.sequence1.Format = 1;
            // 
            // sequencer1
            // 
            //this.sequencer1.Position = 0;
            //this.sequencer1.Sequence = this.sequence1;
            //this.sequencer1.PlayingCompleted += new System.EventHandler(this.HandlePlayingCompleted);
            //this.sequencer1.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            //this.sequencer1.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            //this.sequencer1.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            //this.sequencer1.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);

            // timer1
            // 
            //this.timer1.Interval = 1000;
            //this.timer1.Tick += new System.EventHandler(this.timer1_Tick);

        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //}

        //private void Play(Object stateInfo)
        //{
        //    sequencer1.Start();
        //    timer1.Start();
        //}

        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
        }


        //private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        //{
        //    if (closing)
        //    {
        //        return;
        //    }

        //    outDevice.Send(e.Message);
        //    //pianoControl1.Send(e.Message);
        //}

        //private void HandleChased(object sender, ChasedEventArgs e)
        //{
        //    foreach (ChannelMessage message in e.Messages)
        //    {
        //        outDevice.Send(message);
        //    }
        //}

        //private void HandleSysExMessagePlayed(object sender, SysExMessageEventArgs e)
        //{
        //    //     outDevice.Send(e.Message); Sometimes causes an exception to be thrown because the output device is overloaded.
        //}

        //private void HandleStopped(object sender, StoppedEventArgs e)
        //{
        //    foreach (ChannelMessage message in e.Messages)
        //    {
        //        outDevice.Send(message);
        //        //pianoControl1.Send(message);
        //    }
        //}

        //private void HandlePlayingCompleted(object sender, EventArgs e)
        //{
        //    timer1.Stop();
        //}


        protected override void OnShown(EventArgs e)
        {
            //try
            //{
            //    outDevice = new OutputDevice(outDeviceID);
            //    //sequence1.LoadProgressChanged += HandleLoadProgressChanged;
            //    //sequence1.LoadCompleted += HandleLoadCompleted;
            //    outDevice.RunningStatusEnabled = true;
            //    outDevice.Reset();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error!",
            //    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    Close();
            //}
            //
            //string fileName = "./Resources/BR-Intro2.mid";
            //try
            //{
            //    sequencer1.Stop();
            //    //playing = false;
            //    sequence1.Load(fileName);

            //    this.Cursor = Cursors.WaitCursor;
            //    while (sequence1.IsBusy is true)
            //    {
            //        Sleep(1);
            //    }
            //    this.Cursor = Cursors.Default;

            //    sequencer1.Sequence = sequence1;
            //    //startButton.Enabled = false;
            //    //continueButton.Enabled = false;
            //    //stopButton.Enabled = false;
            //    //openToolStripMenuItem.Enabled = false;
            //    //ThreadPool.QueueUserWorkItem(new WaitCallback(Play));
            //    //
            //    sequencer1.Start();
            //    timer1.Start();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            base.OnShown(e);
        }


        private void Sleep(int v)
        {
            try
            {
                System.Threading.Thread.Sleep(v * 1000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Stop);

            }
        }

    private void Button1_Click(object sender, EventArgs e)
        {
            Form s2 = new Show2();
            s2.Show();
            //Next=>
            this.Close();
        }
    }
}
