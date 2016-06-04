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
        private DoubleAnimation animation;

        public MainWindow()
        {
            animation = new DoubleAnimation
            {
                To = 0,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = TimeSpan.FromSeconds(3),
                FillBehavior = FillBehavior.HoldEnd
            };

            InitializeComponent();

            InitFretLabels(true);

            Sound.Randomize();
        }

        private void Cell_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (sender != null)
            {
                var label = sender as Label;

                int row = (int)label.GetValue(Grid.RowProperty);
                int column = (int)label.GetValue(Grid.ColumnProperty);

                Sounds.Sound.Play(5 - row, column);
                
                if (!btnRandomize.IsEnabled)
                {
                    if (Sounds.Sound.IsCorrectSound(5 - row, column))
                        label.SetValue(Label.BackgroundProperty, GetGradient(Colors.Green));
                    else
                        label.SetValue(Label.BackgroundProperty, GetGradient(Colors.Red));
                }
                else
                    label.SetValue(Label.BackgroundProperty, GetGradient(Colors.White));

                label.BeginAnimation(UIElement.OpacityProperty, null);
                label.Opacity = 0.5;

                label.BeginAnimation(UIElement.OpacityProperty, animation);

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
