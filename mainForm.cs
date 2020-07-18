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
        public frmMain()
        {
            InitializeComponent();
        }

        private NAudio.Wave.WaveFileReader waveFileReader = null;

        private NAudio.Wave.DirectSoundOut soundOut = null;

        //private NAudio.Wave.WaveIn sourceStream = null;


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

        }


        private void btnTest1_Click(object sender, EventArgs e)
        {
            btnTest1.BackColor = (AudioTests.MicrophoneTest(0) ? Color.Green : Color.Red);
        }

        private void btnTest2_Click(object sender, EventArgs e)
        {
            AudioTests.SpeakerTest(0);
        }

        private void btnTest3_Click(object sender, EventArgs e)
        {
            AudioTests.SpeakerTest(1);
        }
    }
}
