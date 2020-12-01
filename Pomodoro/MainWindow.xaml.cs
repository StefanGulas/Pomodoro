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
    DateTime endTimePomodoro = DateTime.Now.AddMinutes(25);
    DateTime endTimeBreak = DateTime.Now.AddMinutes(5);
    TimeSpan breakTimeSpan;
    TimeSpan ts;
    public int pomodoroCounter = 0;
    int counter = 0;

    public MainWindow()
    {
      InitializeComponent();
      DispatcherTimer t = new DispatcherTimer();
      t.Tick += new EventHandler(t_Tick);
      t.Start();
      Continue.Content = "Pomodoro starten";
      Unterbrochen.Visibility = Visibility.Hidden;
      AnzahlPomodoros.Text = pomodoroCounter.ToString();
    }

    void t_Tick(object sender, EventArgs e)
    {
      {
        if (!isInterruption)
        {
          if (isPomodoro)
          {
            if (counter > 10000)
            {
              SystemSounds.Question.Play();
              counter = 0;
            }
            counter++;
            ts = endTimePomodoro.Subtract(DateTime.Now);
            Activity.Text = "Fokussierung";
            Continue.Content = "Jetzt Pausieren";
          }
          else
          {
            ts = endTimeBreak.Subtract(DateTime.Now);
            Activity.Text = "Pause";
            Continue.Content = "Jetzt Fokussieren";
          }

          timerLabel.Content = (ts.Minutes.ToString() + " Min " + (ts.Seconds).ToString() + " Sek");
          if (ts.Minutes.ToString() == "0")
          {
            SystemSounds.Beep.Play();
            if (isPomodoro)
            {
              isPomodoro = false;
              pomodoroCounter++;
            }
            else isPomodoro = true;
            endTimePomodoro = DateTime.Now.AddMinutes(25);
            endTimeBreak = DateTime.Now.AddMinutes(5);
          }
        }
      }
    }

    private void Continue_Click(object sender, RoutedEventArgs e)
    {
      if (!isInterruption)

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
        endTimePomodoro = DateTime.Now.AddMinutes(25);
        endTimeBreak = DateTime.Now.AddMinutes(5);
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
