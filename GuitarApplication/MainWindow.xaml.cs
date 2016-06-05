using Sounds;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GuitarApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitFretLabels();

            Sound.Randomize();
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (sender != null)
            {
                var label = sender as Label;
                var cell = GetColumnAndRow(label);
                var checkSound = !btnRandomize.IsEnabled;
                var isCorrectSound = Sound.IsCorrectSound(5 - cell.row, cell.column);

                Sound.Play(5 - cell.row, cell.column);
                Animation.FlashPickedFret(label, FretColor(checkSound, isCorrectSound));

                btnRandomize.IsEnabled = true;
            }
        }

        private void btnRandomize_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = false;
            RandomizeSound();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Sound.Play();
        }
    }
}
