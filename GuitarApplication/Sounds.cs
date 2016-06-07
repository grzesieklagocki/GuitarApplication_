using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Sounds
{
    class Sound
    {
        private int currentSound;
        private AudioFileReader[] singleNoteSounds;
        private WaveOut waveOut = new WaveOut();
        const int notesCount = 46;

        public Sound(string directory)
        {
            singleNoteSounds = new AudioFileReader[notesCount];

            for (int i = 0; i < notesCount; i++)
                singleNoteSounds[i] = new AudioFileReader(("Data/Sounds/" + directory + "/" + i.ToString() + ".mp3"));
        }

        private int Randomize()
        {
            var random = new Random();

            return random.Next(notesCount + 1);
        }

        private void PlaybackStopped(object sender, StoppedEventArgs e)
        {
            (sender as WaveOut).Dispose();
        }

        /// <summary>
        /// Play random note
        /// </summary>
        public void Play()
        { Play(currentSound = Randomize()); }

        public void Play(int _string, int fret)
        { Play(CalculateSound(_string, fret)); }

        private void Play(int soundNumber)
        {
            var waveOutDevice = new WaveOut();

            try
            {
                //waveOut.Stop();
                //waveOut.Dispose();

                var waveOut = new WaveOut();
                //waveOut.PlaybackStopped += PlaybackStopped;

                waveOut.Init(singleNoteSounds[soundNumber]);
                waveOut.Play();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public bool IsCorrectSound(int _string, int fret)
        { return (currentSound == CalculateSound(_string, fret)); }

        public string GetSoundName(int _string, int fret)
        {
            int soundNumber = CalculateSound(_string, fret);
            string soundName = string.Empty;

            switch (soundNumber % 12)
            {
                case 0:
                    soundName = "E";
                    break;
                case 1:
                    soundName = "F";
                    break;
                case 2:
                    soundName = "Fis";
                    break;
                case 3:
                    soundName = "G";
                    break;
                case 4:
                    soundName = "Gis";
                    break;
                case 5:
                    soundName = "A";
                    break;
                case 6:
                    soundName = "Ais";
                    break;
                case 7:
                    soundName = "H";
                    break;
                case 8:
                    soundName = "C";
                    break;
                case 9:
                    soundName = "Cis";
                    break;
                case 10:
                    soundName = "D";
                    break;
                case 11:
                    soundName = "Dis";
                    break;
            }

            return soundName;
        }

        private int CalculateSound(int _string, int fret)
        {
            if (_string < 0 || _string > 5 || fret < 0 || fret > 12)
                throw new ArgumentOutOfRangeException();


            int soundNumber = _string * 5 + fret;

            if (_string > 3)
                soundNumber--;

            return soundNumber;
        }


        private class SingleNoteSound
        {
            private WaveOut waveOutDevice = new WaveOut();
            private AudioFileReader audioFileReader;

            public SingleNoteSound(int soundNumber, string directory)
            {
                audioFileReader = new AudioFileReader("Data/Sounds/" + directory + "/" + soundNumber.ToString() + ".mp3");
                waveOutDevice.Init(audioFileReader);
            }

            public void Play()
            { waveOutDevice.Play(); }
        }
    }
}
