using NAudio.Wave;
using System;

namespace Sounds
{
    static class Sound
    {
        private static IWavePlayer waveOutDevice = new WaveOut();
        private static AudioFileReader audioFileReader;
        private static int currentSound;

        public static void Randomize()
        {
            Random random = new Random();
            currentSound = random.Next(46);
        }

        public static bool IsCorrectSound(int _string, int fret)
        {
            return (currentSound == CalculateSound(_string, fret));
        }

        public static void Play()
        {
            Play(currentSound);
        }

        public static void Play(int _string, int fret)
        {
            Play(CalculateSound(_string, fret));
        }

        private static void Play(int soundNumber)
        {
            audioFileReader = new AudioFileReader("Data/Sounds/" + soundNumber.ToString() + ".mp3");
            waveOutDevice.Init(audioFileReader);
            waveOutDevice.Play();
        }

        public static string GetSoundName(int _string, int fret)
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

        private static int CalculateSound(int _string, int fret)
        {
            if (_string < 0 || _string > 5 || fret < 0 || fret > 12)
                throw new ArgumentOutOfRangeException();

            int soundNumber = _string * 5 + fret;

            if (_string > 3)
                soundNumber--;

            return soundNumber;
        }
    }
}
