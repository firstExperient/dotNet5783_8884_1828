﻿using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    BackgroundWorker worker = new BackgroundWorker();

    #region Dependency properties

    public static readonly DependencyProperty TimeProperty = DependencyProperty.Register(nameof(Time), typeof(string), typeof(SimulatorWindow));


    public string Time
    {
        get { return (string)GetValue(TimeProperty); }
        set { SetValue(TimeProperty, value); }
    }

    public static readonly DependencyProperty DataProperty = DependencyProperty.Register(nameof(Data), typeof(Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?>), typeof(SimulatorWindow));


    public Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?>? Data
    {
        get { return (Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?>?)GetValue(DataProperty); }
        set { SetValue(DataProperty, value); }
    }
    public static readonly DependencyProperty EndTimeProperty = DependencyProperty.Register(nameof(EndTime), typeof(string), typeof(SimulatorWindow));


    public string? EndTime
    {
        get { return (string?)GetValue(EndTimeProperty); }
        set { SetValue(EndTimeProperty, value); }
    }

    #endregion

    private const int GWL_STYLE = -16;
    private const int WS_SYSMENU = 0x80000;
    [DllImport("user32.dll", SetLastError = true)]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    public SimulatorWindow()
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
        worker.DoWork += DoWork;
        worker.ProgressChanged += ProgressChanged;
        worker.RunWorkerCompleted += RunWorkerCompleted;

        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;

        worker.RunWorkerAsync();
    }

    void DoWork(object? sender, DoWorkEventArgs e)
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
            Dispatcher.BeginInvoke(() => { 
                Time = DateTime.Now.ToString("T");
                if(Data != null)
                    EndTime = (Data.Item2 - DateTime.Now)?.Seconds.ToString();
            });
        else
        {
            if (e.UserState is Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?>)
                Dispatcher.BeginInvoke(() => Data = e.UserState as Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?>);
        }
    }

    public void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.StopSimulation();
        Simulator.Simulator.RemoveStopedEventListener(StopedEvent);
        Simulator.Simulator.RemoveProgressEventListener(ProgressedEvent);
        Close();
    }

    public void StopSimulation(object sender, RoutedEventArgs e)
    {
        worker.CancelAsync();
       
    }


    public void StopedEvent(object? sender, EventArgs e)
    {
        worker.CancelAsync();
    }

    public void ProgressedEvent(object? sender, EventArgs e)
    {
        ProgressDetails details = e as ProgressDetails ?? throw new Exception("worng event args type");
        Tuple<DateTime?, DateTime?, int?, BO.OrderStatus?, BO.OrderStatus?> data =
            new(details.StartTime, details.EndTime, details.OrderId, details.PreStatus, details.NextStatus);
        worker.ReportProgress(0, data);
    }

}
