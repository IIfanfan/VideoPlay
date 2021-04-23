using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RP.ScoutRobot.WpfUI
{
    /// <summary>
    /// VideoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VideoWindow
    {
        public VideoWindow(string title,string fileName, int width = 800, int height = 450)
        {
            InitializeComponent();
            this.Title = title;
            this.Width = width;
            this.Height = height;

            this.Left = (System.Windows.SystemParameters.PrimaryScreenWidth - width) / 2;
            this.Top = (System.Windows.SystemParameters.PrimaryScreenHeight - height) / 2;
            MediaPlayer.Source = new Uri(fileName);
        }
        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play();
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position = MediaPlayer.Position + TimeSpan.FromSeconds(20);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Position = MediaPlayer.Position - TimeSpan.FromSeconds(20);
        }
        DispatcherTimer timer = null;
        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            // Get the lenght of the video
            sliderFontSize.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            //媒体文件打开成功
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            sliderFontSize.Value = MediaPlayer.Position.TotalSeconds;
        }

        private void sliderFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Position = TimeSpan.FromSeconds(sliderFontSize.Value);
        }
    }
}
