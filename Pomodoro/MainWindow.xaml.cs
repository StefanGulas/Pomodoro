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

namespace Pomodoro
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime endTime = new DateTime(2021, 01, 15, 0, 0, 0);

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer t = new DispatcherTimer();
            t.Tick += new EventHandler(t_Tick);
            TimeSpan ts = endTime.Subtract(DateTime.Now);
            ZeroDay.Text = endTime.ToString().Remove(10);
            timerLabel.Content = (ts.Days.ToString() + " days");
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = endTime.Subtract(DateTime.Now);
            timerLabel.Content = (ts.Days.ToString() + " days");
        }
    }
}
