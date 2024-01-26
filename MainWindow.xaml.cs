using System.Text;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace BeehiveManagementSystem
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;
        
        private Queen queen = new Queen();
        public MainWindow()
        {
            InitializeComponent();
            statusReport.Text = queen.StatusReport;

            timer.Interval = TimeSpan.FromSeconds(1.5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            WorkShift_Click(this, new RoutedEventArgs());
        }

        private void AssignJob_Click(object sender, RoutedEventArgs e)
        {
            queen.AssignBee(jobSelector.Text);
            statusReport.Text = queen.StatusReport;
        }

        private void WorkShift_Click(object sender, RoutedEventArgs e)
        {
            queen.WorkTheNexthift();
            statusReport.Text = queen.StatusReport;
        }

    }



}