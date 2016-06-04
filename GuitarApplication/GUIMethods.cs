using Sounds;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GuitarApplication
{
    public partial class MainWindow : Window
    {
        private void InitFretLabels(bool hintEnable)
        {
            for (int x = 0; x < 13; x++)
                for (int y = 0; y < 6; y++)
                {
                    var label = new Label();

                    label.SetValue(Grid.ColumnProperty, x);
                    label.SetValue(Grid.RowProperty, y);
                    label.SetValue(Label.ForegroundProperty, Brushes.White);

                    if (hintEnable)
                        label.SetValue(Label.ContentProperty, Sound.GetSoundName(y, x));

                    label.SetValue(Label.MarginProperty, new Thickness(1));
                    label.SetValue(Label.BackgroundProperty, GetGradient(Colors.White));
                    label.SetValue(Label.OpacityProperty, 0.0);

                    label.MouseDown += new MouseButtonEventHandler(Cell_MouseDown);

                    GridFretoboard.Children.Add(label);
                }
        }

        private LinearGradientBrush GetGradient(Color centerColor)
        {
            LinearGradientBrush gradient = new LinearGradientBrush();
            gradient.StartPoint = new Point(0, 0);
            gradient.EndPoint = new Point(0, 1);

            GradientStop start = new GradientStop();
            start.Color = Colors.Black;
            start.Offset = 0.0;
            gradient.GradientStops.Add(start);

            GradientStop center = new GradientStop();
            center.Color = centerColor;
            center.Offset = 0.5;
            gradient.GradientStops.Add(center);

            GradientStop stop = new GradientStop();
            stop.Color = Colors.Black;
            stop.Offset = 1.0;
            gradient.GradientStops.Add(stop);

            return gradient;
        }

        private static void RandomizeSound()
        {
            Sound.Randomize();
            Sound.Play();
        }
    }

}
