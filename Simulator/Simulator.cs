

using BlApi;
using BO;

namespace Simulator;

public static class Simulator
{
    private static IBl? bl = BlApi.Factory.Get();

    private static volatile bool flag = false;
    
    private static event EventHandler Stoped;
    private static event EventHandler Progressed;

    private static Random rand = new Random();
    public static void StartSimulation()
    {
        Thread simulator = new Thread(simulate);
        simulator.Start();
    }

    private static void simulate()
    {
       
        while (!flag)
        {
            BO.Order? order = bl?.Order.GetForSimulator();
            
            if (order != null)
            {
                int seconds = rand.Next(3, 11);
                if(Progressed != null)
                {
                    Progressed(null,
                        new ProgressDetails(order.Status!,
                                         order.Status == OrderStatus.Confirmed ? OrderStatus.Shipped : OrderStatus.Delivered,
                                         DateTime.Now,
                                         DateTime.Now + new TimeSpan(0, 0, seconds)));
                }
                Thread.Sleep(seconds * 1000);
                if (order.Status == OrderStatus.Confirmed)
                    bl?.Order.ShipOrder(order.ID);
                else
                    bl?.Order.DeliverOrder(order.ID);
            }
            Thread.Sleep(1000);
        }
        if(Stoped != null)
            Stoped(null, EventArgs.Empty);
    }

    public static void StopSimulation()
    {
        flag = true;
    }

    public static void StopedEventListener(EventHandler e) => Stoped += e;
    public static void ProgressEventListener(EventHandler e) => Progressed += e;
    public static void RemoveStopedEventListener(EventHandler e) => Stoped -= e;
    public static void RemoveProgressEventListener(EventHandler e) => Progressed -= e;

}


