using Sounds;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GuitarApplication
{
    public partial class MainWindow : Window
    {
        private class Cell
        {
            public int row { get; private set; }
            public int column { get; private set; }

            public Cell(int row, int column)
            {
                this.row = row;
                this.column = column;
            }
        }

        private static class Animation
        {
            public static double startOpacity = 0.55;
            private static DoubleAnimation animation =
                            new DoubleAnimation
                            {
                                To = 0,
                                BeginTime = TimeSpan.FromSeconds(0),
                                Duration = TimeSpan.FromSeconds(3.2),
                                FillBehavior = FillBehavior.HoldEnd
                            };

            public static void FlashPickedFret(Label label, Color color, bool entireString = true)
            {
                if (entireString)
                {
                    foreach (var fret in (label.Parent as Grid).Children)
                        if ((int)(fret as Label).GetValue(Grid.RowProperty) == (int)label.GetValue(Grid.RowProperty) &&
                            (int)(fret as Label).GetValue(Grid.ColumnProperty) >= (int)label.GetValue(Grid.ColumnProperty))
                        {
                            (fret as Label).SetValue(BackgroundProperty, GetGradient(color));
                            (fret as Label).BeginAnimation(OpacityProperty, null);
                            (fret as Label).Opacity = startOpacity;
                            (fret as Label).BeginAnimation(OpacityProperty, animation);
                        }

                }
                else
                {
                    label.Background.BeginAnimation(OpacityProperty, null);
                    label.Background.Opacity = startOpacity;
                    label.Background.BeginAnimation(UIElement.OpacityProperty, animation);
                }
            }

            public static void SetAnimationTime(double seconds)
            { animation.Duration = TimeSpan.FromSeconds(seconds); }
        }

        private void InitFretLabels(bool hintEnable = true)
        {
            for (int x = 0; x < 13; x++)
                for (int y = 0; y < 6; y++)
                {
                    var label = new Label();

                    label.SetValue(Grid.ColumnProperty, x);
                    label.SetValue(Grid.RowProperty, y);
                    label.SetValue(Label.ForegroundProperty, Brushes.White);

                    if (hintEnable)
                        label.SetValue(Label.ContentProperty, sound.GetSoundName(y, x));

                    label.SetValue(Label.MarginProperty, new Thickness(1));
                    label.SetValue(Label.BackgroundProperty, GetGradient(Colors.White));
                    label.Background.SetValue(Label.OpacityProperty, 0.0);

                    label.MouseDown += new MouseButtonEventHandler(Cell_MouseDown);

                    GridFretoboard.Children.Add(label);
                }

            for (int y = 0; y < 6; y++)
            {
                var label = new Label();

                //label.SetValue(Grid.ColumnProperty, 0);
                label.SetValue(Grid.RowProperty, y);
                label.SetValue(Label.ForegroundProperty, Brushes.White);

                //if (hintEnable)
                //    label.SetValue(Label.ContentProperty, Sound.GetSoundName(y, 0));

                //label.SetValue(Label.MarginProperty, new Thickness(1));
                //label.SetValue(Label.BackgroundProperty, GetGradient(Colors.White));
                label.SetValue(Label.OpacityProperty, 0.0);

                label.MouseDown += new MouseButtonEventHandler(Cell_MouseDown);

                GridFretoboardEmptyStrings.Children.Add(label);
            }
        }

        private static LinearGradientBrush GetGradient(Color centerColor)
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

        private Color FretColor(bool checkSound, bool isCorrectSound)
        {
            if (checkSound)
            {
                if (isCorrectSound)
                    return Colors.Green;
                else
                    return Colors.Red;
            }
            else
                return Colors.White;
        }

        private Cell GetColumnAndRow(Label label)
        {
            return new Cell((int)label.GetValue(Grid.RowProperty), (int)label.GetValue(Grid.ColumnProperty));
        }

    }

}
