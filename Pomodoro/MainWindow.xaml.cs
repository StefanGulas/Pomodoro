using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly int pomodoroLength = 25;
        int breakLength = 5;
        bool isPomodoro = true;
        bool isBreak = false;
        DateTime endTimePomodoro = DateTime.Now.AddSeconds(5);
        DateTime endTimeBreak = DateTime.Now.AddSeconds(2);


        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            //while (true)
            {

                TimeSpan ts;
                if (isBreak)
                {
                    endTimePomodoro =endTimePomodoro.AddTicks(1);
                    endTimePomodoro =endTimeBreak.AddTicks(1);
                    ts = endTimePomodoro.Subtract(DateTime.Now);
                    
                }
                else
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
                timerLabel.Content = (ts.Seconds.ToString() + " Minutes");
                if (ts.Seconds.ToString() == "-1")
                {
                    SystemSounds.Beep.Play();
                    if (isPomodoro) isPomodoro = false;
                    else isPomodoro = true;
                    endTimePomodoro = DateTime.Now.AddSeconds(5);
                    endTimeBreak = DateTime.Now.AddSeconds(2);


                    //if (isPomodoro)
                    //{
                    //    ts = endTimeBreak.Subtract(DateTime.Now);
                    //    isPomodoro = false;
                    //}
                    //else
                    //{
                    //    ts = endTimePomodoro.Subtract(DateTime.Now);
                    //    isPomodoro = true;
                    //}

                }
                }
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (isPomodoro)
            {
                isPomodoro = false;
            }
            else isPomodoro = true;
            endTimePomodoro = DateTime.Now.AddSeconds(5);
            endTimeBreak = DateTime.Now.AddSeconds(2);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            if (!isBreak)
            {
                isBreak = true;

            }
            else
            {
                isBreak = false;
            }
            endTimePomodoro = DateTime.Now.AddSeconds(5);
            endTimeBreak = DateTime.Now.AddSeconds(2);
        }
    }
}
