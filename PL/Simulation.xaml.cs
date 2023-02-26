using System;
using System.Windows;
using System.Timers;
using PL.Products;
using System.ComponentModel;
using Simulator;
using System.Threading;
using BO;

//using System.Threading;

namespace PL;

/// <summary>
/// Interaction logic for Simulation.xaml
/// </summary>
public partial class Simulation : Window
{
    BackgroundWorker bw;

    #region Dependency properties

    public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(nameof(Time), typeof(string), typeof(Simulation));


    public string Time
    {
        get { return (string)GetValue(TimeProperty); }
        set { SetValue(TimeProperty, value); }
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Tuple<string, string, int, BO.OrderStatus, BO.OrderStatus>), typeof(Simulation));


    public Tuple<string?,string?,int?,BO.OrderStatus?,BO.OrderStatus?>? Data
    {
        get { return (Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>?)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }

    public static readonly DependencyProperty PreStatusProperty = DependencyProperty.Register(nameof(PreStatus), typeof(BO.OrderStatus), typeof(Simulation));


    public BO.OrderStatus PreStatus
    {
        get { return (BO.OrderStatus)GetValue(PreStatusProperty); }
        set { SetValue(PreStatusProperty, value); }
    }

    public static readonly DependencyProperty NextStatusProperty = DependencyProperty.Register(nameof(NextStatus), typeof(BO.OrderStatus), typeof(Simulation));


    public BO.OrderStatus NextStatus
    {
        get { return (BO.OrderStatus)GetValue(NextStatusProperty); }
        set { SetValue(NextStatusProperty, value); }
    }

    public static readonly DependencyProperty StartTimeProperty = DependencyProperty.Register(nameof(StartTime), typeof(string), typeof(Simulation));


    public string StartTime
    {
        get { return (string)GetValue(StartTimeProperty); }
        set { SetValue(StartTimeProperty, value); }
    }

    public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(nameof(EndTime), typeof(string), typeof(Simulation));


    public string EndTime
    {
        get { return (string)GetValue(EndTimeProperty); }
        set { SetValue(EndTimeProperty, value); }
    }

    #endregion



    public Simulation()
    {
        InitializeComponent();
        
        bw = new();
        bw.WorkerReportsProgress = true;
        bw.WorkerSupportsCancellation = true;
        bw.DoWork += DoWork;
        bw.ProgressChanged += ProgressChanged;

        bw.RunWorkerAsync();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
       
    }

    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.StopedEventListener(StopedEvent);
        Simulator.Simulator.ProgressEventListener(ProgressedEvent);
        Simulator.Simulator.StartSimulation();
        while (bw.CancellationPending)
        {
            bw.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }

    public void ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        if(e.ProgressPercentage == 1)
            Dispatcher.Invoke(() => Time = DateTime.Now.ToString());
        else
        {
            if(e.UserState is Tuple< string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>)
                Dispatcher.Invoke(() => Data = e.UserState as Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?>);
        }
    }

    public void StopSimulation(object sender, RoutedEventArgs e)
    {
        bw.CancelAsync();
    }

    public void StopedEvent(object? sender, EventArgs e)
    {

    }

    public void ProgressedEvent(object? sender, EventArgs e)
    {
        ProgressDetails details = e as ProgressDetails ?? throw new Exception("worng event args type");
        Tuple<string?, string?, int?, BO.OrderStatus?, BO.OrderStatus?> data =
            new(details.StartTime.ToString(), details.EndTime.ToString(), 0, details.PreStatus, details.NextStatus); 
        bw.ReportProgress(0, data);
    }

}
