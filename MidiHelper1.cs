using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Sanford.Multimedia.Midi;
using System.ComponentModel;

namespace AnimatedForm
{
    public class MidiHelper1
    {
        private int outDeviceID = 0;
        private OutputDevice outDevice;
        private Dictionary<string, Sequence> dicSequence = new Dictionary<string, Sequence>();
        private Dictionary<string, Sequencer> dicSequencer = new Dictionary<string, Sequencer>();
        private Dictionary<string, NoArgDelegate> dicPlayingCompleteDelegate = new Dictionary<string, NoArgDelegate>();
        private Dictionary<string, int> dicSequencerMessageCount = new Dictionary<string, int>();
        private Dictionary<string, bool> dicSequencerInitialized = new Dictionary<string, bool>();

        private bool playing = false;
        private bool closing = false;
        public delegate void NoArgDelegate();
        NoArgDelegate loadCompleted;
        NoArgDelegate playingCompleted;

        #region ctor
        public MidiHelper1()
        {
            if (outDevice == null)
                outDevice = new OutputDevice(outDeviceID);
        }
        #endregion ctor

        #region methods

        public void InitializeSequencer(string midiKey)
        {
            var sequence = dicSequence[midiKey];
            var sequencer = dicSequencer[midiKey];

            sequencer.Stop();
            playing = false;

            sequence.Format = 1;
            sequencer.Position = 0;
            sequencer.Sequence = sequence;
            sequencer.ChannelMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.ChannelMessageEventArgs>(this.HandleChannelMessagePlayed);
            sequencer.Stopped += new System.EventHandler<Sanford.Multimedia.Midi.StoppedEventArgs>(this.HandleStopped);
            sequencer.SysExMessagePlayed += new System.EventHandler<Sanford.Multimedia.Midi.SysExMessageEventArgs>(this.HandleSysExMessagePlayed);
            sequencer.Chased += new System.EventHandler<Sanford.Multimedia.Midi.ChasedEventArgs>(this.HandleChased);
            sequence.LoadCompleted += HandleLoadCompleted;
        }

        public void Load(string midiKey, string midiFile)
        {
            dicSequence.Add(midiKey, new Sequence());
            dicSequencer.Add(midiKey, new Sequencer(midiKey));
            dicPlayingCompleteDelegate.Add(midiKey, null);
            dicSequencerMessageCount.Add(midiKey, 0);
            dicSequencerInitialized.Add(midiKey, false);

            InitializeSequencer(midiKey);

            dicSequencer[midiKey].Stop();

            dicSequencer[midiKey].ChannelMessagePlayed += (s, e) =>
            {
                dicSequencerMessageCount[midiKey]++;
            };

            dicSequencer[midiKey].PlayingCompleted += (s, e) =>
            {
                if (dicSequencerMessageCount[midiKey] > 0)
                {
                    var playingCompleted = dicPlayingCompleteDelegate[midiKey];

                    if (playingCompleted != null)
                        playingCompleted();
                    dicSequencer[midiKey].Stop();
                    dicSequencerMessageCount[midiKey] = 0;
                }
            };

            playing = false;
            //dicSequence[midiKey].LoadAsync(midiFile);
            dicSequence[midiKey].Load(midiFile);
        }

        public void Play(string midiKey, NoArgDelegate playingCompleted)
        {
            playing = true;

            dicPlayingCompleteDelegate[midiKey] = playingCompleted;

            if (!dicSequencerInitialized[midiKey])
            {
                dicSequencerInitialized[midiKey] = true;
                dicSequencer[midiKey].GetTracks();
            }
            dicSequencer[midiKey].Stop();
            dicSequencer[midiKey].Start();
        }

        public void Continue(string midiKey)
        {
            playing = true;
            dicSequencer[midiKey].Continue();
        }

        public void Stop(string midiKey)
        {
            playing = false;
            dicSequencer[midiKey].Stop();
        }

        public void StopAll()
        {
            foreach (var kv in dicSequencer)
            {
                kv.Value.Stop();
            }
        }

        public void DisposeAll()
        {
            StopAll();
            foreach (var s in dicSequence)
            {
                s.Value.Dispose();
            }

            if (outDevice != null)
            {
                outDevice.Dispose();
            }
        }

        #endregion methods

        #region events
        private void HandleChannelMessagePlayed(object sender, ChannelMessageEventArgs e)
        {
            if (closing)
            {
                return;
            }

            outDevice.Send(e.Message);
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
            outDevice.Send(e.Message); //Sometimes causes an exception to be thrown because the output device is overloaded.
        }

        private void HandleStopped(object sender, StoppedEventArgs e)
        {
            foreach (ChannelMessage message in e.Messages)
            {
                outDevice.Send(message);
            }
        }

        private void HandleLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (loadCompleted != null)
                loadCompleted();
        }

        #endregion events
    }
}
