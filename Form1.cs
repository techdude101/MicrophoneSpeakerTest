using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicrophoneSpeakerTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private NAudio.Wave.WaveFileReader waveFileReader = null;

        private NAudio.Wave.DirectSoundOut soundOut = null;

        private NAudio.Wave.WaveIn sourceStream = null;

        private void btnPlay_Click(object sender, EventArgs e)
        {
            waveFileReader = new NAudio.Wave.WaveFileReader("C:\\Users\\Tech\\Downloads\\funny-voices-daniel_simon.wav");

            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = 0;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(0).Channels);
            
            NAudio.Wave.WaveInProvider waveIn = new NAudio.Wave.WaveInProvider(sourceStream);
            
            waveViewer1.SamplesPerPixel = 1200;
            waveViewer1.WaveStream = waveFileReader;
            
            soundOut = new NAudio.Wave.DirectSoundOut();
            soundOut.Init(waveIn);

            sourceStream.StartRecording();

            //soundOut.Init(new NAudio.Wave.WaveChannel32(waveFileReader));
            //soundOut.Play();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //soundOut.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (soundOut != null)
            {
                soundOut.Stop();
                soundOut.Dispose();
                soundOut = null;
            }

            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
        }
    }
}
