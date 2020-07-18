using NAudio.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicrophoneSpeakerTest
{
    class AudioTests
    {
        public static String[] tests = new String[] {"Microphone", "Speaker - Left", "Speaker - Right" };
        public static Boolean SpeakerTest(int channel)
        {
            String wavFile = "";
            wavFile = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            NAudio.Wave.WaveFileReader wave = null;
            NAudio.Wave.DirectSoundOut output = null;

            if (channel == 0)
                wavFile += "\\440Hz_sine_left.wav";
            else
                wavFile += "\\440Hz_sine_right.wav";

            wave = new NAudio.Wave.WaveFileReader(wavFile);
            output = new NAudio.Wave.DirectSoundOut();
            
            output.Init(new WaveChannel32(wave));
            output.Play();

            return false;
        }

        public static Boolean MicrophoneTest(int deviceId)
        {
            NAudio.Wave.WaveIn sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceId;
            try
            {
                sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceId).Channels);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public static Boolean DistortionTest()
        {
            return false;
        }
    }

}
