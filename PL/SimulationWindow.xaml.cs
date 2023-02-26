using System;
using System.Windows;
using System.ComponentModel;
using Simulator;
using System.Threading;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows.Interop;

//using System.Threading;

namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class SimulationWindow: Window
{
    BackgroundWorker worker;
    Timer timer;

    #region Dependency properties

    //public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(nameof(Time), typeof(string), typeof(SimulationWindow));


    //public string Time
    //{
    //    get { return (string)GetValue(TimeProperty); }
    //    set { SetValue(TimeProperty, value); }
    //}

    //public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Tuple<string, string, int, BO.OrderStatus, BO.OrderStatus>), typeof(SimulationWindow));


    //public Tuple<string?,string?,int?,BO.OrderStatus?,BO.OrderStatus?>? Data
    //{
    //    get { return (Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>?)GetValue(DataProperty); }
    //    set { SetValue(DataProperty, value); }
    //}

    //public static readonly DependencyProperty PreStatusProperty = DependencyProperty.Register(nameof(PreStatus), typeof(BO.OrderStatus), typeof(SimulationWindow));


    //public BO.OrderStatus PreStatus
    //{
    //    get { return (BO.OrderStatus)GetValue(PreStatusProperty); }
    //    set { SetValue(PreStatusProperty, value); }
    //}

    //public static readonly DependencyProperty NextStatusProperty = DependencyProperty.Register(nameof(NextStatus), typeof(BO.OrderStatus), typeof(SimulationWindow));


    //public BO.OrderStatus NextStatus
    //{
    //    get { return (BO.OrderStatus)GetValue(NextStatusProperty); }
    //    set { SetValue(NextStatusProperty, value); }
    //}

    //public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(nameof(StartTime), typeof(string), typeof(SimulationWindow));


    //public string StartTime
    //{
    //    get { return (string)GetValue(StartTimeProperty); }
    //    set { SetValue(StartTimeProperty, value); }
    //}

    //public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(nameof(EndTime), typeof(string), typeof(SimulationWindow));


    //public string EndTime
    //{
    //    get { return (string)GetValue(EndTimeProperty); }
    //    set { SetValue(EndTimeProperty, value); }
    //}

    #endregion

    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    public SimulationWindow()
    {
        InitializeComponent();


        Loaded += (object sender, RoutedEventArgs e) =>
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        };
        StartSimulator();
    }
    
    void StartSimulator()
    {
        worker = new BackgroundWorker();
        worker.DoWork += DoWork;
        worker.ProgressChanged += ProgressChanged;
        worker.RunWorkerCompleted += RunWorkerCompleted;

        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        worker.RunWorkerAsync();
        //worker.ReportProgress(1);
        //ProgressChanged(null, new ProgressChangedEventArgs(1,null));
    }

    void DoWork(object sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.StopedEventListener(StopedEvent);
        Simulator.Simulator.ProgressEventListener(ProgressedEvent);
        Simulator.Simulator.StartSimulation();
        while (!worker.CancellationPending)
        {
            worker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    public void ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        if (e.ProgressPercentage == 1)
            Dispatcher.BeginInvoke(() => MessageBox.Show("hi"));// Time = DateTime.Now.ToString());
        else
        {
            if (e.UserState is Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>)
                Dispatcher.BeginInvoke(() => MessageBox.Show("hi"));// Data = e.UserState as Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>);
        }
    }

    public void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {

    }

    public void StopSimulation(object sender, RoutedEventArgs e)
    {
        worker.CancelAsync();
        Close();
    }


    public void StopedEvent(object? sender, EventArgs e)
    {

    }

    public void ProgressedEvent(object? sender, EventArgs e)
    {
        ProgressDetails details = e as ProgressDetails ?? throw new Exception("worng event args type");
        Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?> data =
            new(details.StartTime.ToString(), details.EndTime.ToString(), 0, details.PreStatus, details.NextStatus);
        worker.ReportProgress(0, data);
    }

}
