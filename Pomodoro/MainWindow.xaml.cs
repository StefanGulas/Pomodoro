using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isPomodoro = true;
        bool isBreak = false;
        bool isInterruption = false;
        DateTime endTimePomodoro = DateTime.Now.AddSeconds(26);
        DateTime endTimeBreak = DateTime.Now.AddSeconds(6);
        TimeSpan breakTimeSpan;
        TimeSpan ts;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(t_Tick);
            t.Start();
            Continue.Content = "Pause";
        }

        void t_Tick(object sender, EventArgs e)
        {
            {

                if (!isInterruption)
                {
                    if (isPomodoro)
                    {
                        ts = endTimePomodoro.Subtract(DateTime.Now);
                        Activity.Text = "Fokussierung";
                    }
                    else
                    {
                        ts = endTimeBreak.Subtract(DateTime.Now);
                        Activity.Text = "Pause";

                    }

                    timerLabel.Content = (ts.Seconds.ToString() + " Minuten");
                    if (ts.Seconds.ToString() == "0")
                    {
                        SystemSounds.Beep.Play();
                        if (isPomodoro) isPomodoro = false;
                        else isPomodoro = true;
                        endTimePomodoro = DateTime.Now.AddSeconds(26);
                        endTimeBreak = DateTime.Now.AddSeconds(6);
                    }
                }
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (!isInterruption)
            {
                //endTimePomodoro = DateTime.Now.AddSeconds(26);
                //endTimeBreak = DateTime.Now.AddSeconds(6);
            }
            else
            {

            if (isPomodoro)
            {
                isPomodoro = false;
                Continue.Content = "Pause";
            }
            else
            {
                isPomodoro = true;
                Continue.Content = "Fokussierung";
                    //timerLabel.Content = (ts.Seconds.ToString() + " Minuten");
            }
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (!isInterruption)
            {
                if (isPomodoro) breakTimeSpan = endTimePomodoro.Subtract(DateTime.Now);
                else breakTimeSpan = endTimeBreak.Subtract(DateTime.Now);
                isInterruption = true;

            }
            else
            {
                if (isPomodoro) endTimePomodoro = DateTime.Now.Add(breakTimeSpan);
                else endTimeBreak = DateTime.Now.Add(breakTimeSpan);
                isInterruption = false;
            }
        }
    }
}
