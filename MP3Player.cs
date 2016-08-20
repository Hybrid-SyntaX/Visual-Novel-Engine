using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
namespace Visual_Novel_Engine
{
    public class MP3Player:IDisposable
    {
        //Declarations required for audio out and the MP3 stream
        IWavePlayer waveOutDevice;
        WaveStream mainOutputStream;
        WaveChannel32 volumeStream;

        public static bool IsPlaying(MP3Player mp3)
        {
            if (mp3!=null && mp3.waveOutDevice.PlaybackState == PlaybackState.Playing)
                return true;
            else return false;
        }
        public MP3Player(string fileName)
        {
            Open(fileName);
        }
        public void Open(string FileName)
        {
            waveOutDevice = new WaveOut();
            mainOutputStream = CreateInputStream(FileName);
        }

        public void Play()
        {

            waveOutDevice.Init(mainOutputStream);
            waveOutDevice.Play();
        }
        private WaveStream CreateInputStream(string fileName)
        {
            WaveChannel32 inputStream;
            if (fileName.EndsWith(".mp3"))
            {
                WaveStream mp3Reader = new Mp3FileReader(fileName);
                inputStream = new WaveChannel32(mp3Reader);
            }
            else
            {
                throw new InvalidOperationException("Unsupported extension");
            }
            volumeStream = inputStream;
            return volumeStream;
        }
        public void Stop()
        {
            if (waveOutDevice != null)
            {
                waveOutDevice.Stop();
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
            if (mainOutputStream != null)
            {
                // this one really closes the file and ACM conversion
                volumeStream.Close();
                volumeStream.Dispose();
                volumeStream = null;
                // this one does the metering stream
                mainOutputStream.Close();
                mainOutputStream.Dispose();
                mainOutputStream = null;
            }
            if (waveOutDevice != null)
            {
                waveOutDevice.Dispose();
                waveOutDevice = null;
            }
        }

        ~MP3Player()
        {
            Stop();
            
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
