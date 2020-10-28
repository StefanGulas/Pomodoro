using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace Pomodoro
{
    public partial class MainWindow : Window
    {
        bool isPomodoro = true;
        bool isInterruption = false;
        DateTime endTimePomodoro = DateTime.Now.AddMinutes(26);
        DateTime endTimeBreak = DateTime.Now.AddMinutes(6);
        TimeSpan breakTimeSpan;
        TimeSpan ts;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(t_Tick);
            t.Start();
            Continue.Content = "Pause";
            Unterbrochen.Visibility = Visibility.Hidden;
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

                    timerLabel.Content = (ts.Minutes.ToString() + " Minuten");
                    if (ts.Minutes.ToString() == "0")
                    {
                        SystemSounds.Beep.Play();
                        if (isPomodoro) isPomodoro = false;
                        else isPomodoro = true;
                        endTimePomodoro = DateTime.Now.AddMinutes(26);
                        endTimeBreak = DateTime.Now.AddMinutes(6);
                    }
                }
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (isInterruption)
            {
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
            }
                endTimePomodoro = DateTime.Now.AddMinutes(26);
                endTimeBreak = DateTime.Now.AddMinutes(6);
            }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (!isInterruption)
            {
                if (isPomodoro) breakTimeSpan = endTimePomodoro.Subtract(DateTime.Now);
                else breakTimeSpan = endTimeBreak.Subtract(DateTime.Now);
                isInterruption = true;
                Unterbrochen.Visibility = Visibility.Visible;

            }
            else
            {
                if (isPomodoro) endTimePomodoro = DateTime.Now.Add(breakTimeSpan);
                else endTimeBreak = DateTime.Now.Add(breakTimeSpan);
                isInterruption = false;
                Unterbrochen.Visibility = Visibility.Hidden;
            }
        }
    }
}
