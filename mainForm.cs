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

    public partial class frmMain : Form
    {
        int numSamples = 4000; // number of x-axis pints
        WaveIn waveIn;
        Queue<double> sampleQueue;

        public frmMain()
        {
            InitializeComponent();
            sampleQueue = new Queue<double>(Enumerable.Repeat(0.0, numSamples).ToList());
            chrtWaveForm.ChartAreas[0].AxisY.Maximum = Int16.MaxValue;
            chrtWaveForm.ChartAreas[0].AxisY.Minimum = Int16.MinValue;
        }

        private NAudio.Wave.WaveFileReader waveFileReader = null;

        private NAudio.Wave.DirectSoundOut soundOut = null;

        void wi_DataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 2)
            {
                sampleQueue.Enqueue(BitConverter.ToInt16(e.Buffer, i));
                sampleQueue.Dequeue();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (soundOut != null)
            {
                soundOut.Stop();
                soundOut.Dispose();
                soundOut = null;
            }

            if (waveFileReader != null)
            {
                waveFileReader.Dispose();
                waveFileReader = null;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if (AudioTests.MicrophoneAvailableTest(0))
            {
                waveIn = new WaveIn();
                waveIn.StartRecording();
                waveIn.WaveFormat = new WaveFormat(4100, 16, 1); // rate, bits, channels
                waveIn.DataAvailable += new EventHandler<WaveInEventArgs>(wi_DataAvailable);
                timer1.Enabled = true;
            }
            btnTest1.BackColor = (AudioTests.MicrophoneAvailableTest(0) ? Color.Green : Color.Red);
        }


        private void btnTest1_Click(object sender, EventArgs e)
        {
            btnTest1.BackColor = (AudioTests.MicrophoneAvailableTest(0) ? Color.Green : Color.Red);
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            AudioTests.SpeakerTest(0);
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            AudioTests.SpeakerTest(1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                chrtWaveForm.Series["Series1"].Points.DataBindY(sampleQueue);
            }
            catch
            {
                Console.WriteLine("No bytes recorded");
            }
        }
    }
}
