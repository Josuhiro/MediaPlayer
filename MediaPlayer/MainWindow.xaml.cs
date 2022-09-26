using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonPlayClick(object sender, RoutedEventArgs e)
        {
            video.Play();
            if (timer != null)
            {
                timer.Start();
            }
        }

        private void ButtonPauseClick(object sender, RoutedEventArgs e)
        {
            video.Pause();
            if (timer != null)
            {
                timer.Stop();
            }
        }

        private void ButtonStopClick(object sender, RoutedEventArgs e)
        {
            video.Stop();
        }

        private void video_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            video.ScrubbingEnabled = true;
            video.Stop();
        }

        private void video_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSlider.Maximum = video.NaturalDuration.TimeSpan.TotalSeconds;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            totalTime.Content = video.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            TimeSlider.Value = video.Position.TotalSeconds;
        }

        private void TimeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video.Position = TimeSpan.FromSeconds(TimeSlider.Value);  
        }

        private void TimeSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            video.Pause();
            if (timer != null)
            {
                timer.Stop();
            }
            
        }

        private void TimeSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            video.Play();
            timer.Start();
        }
    }
}
