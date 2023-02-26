using BO;
namespace Simulator;



public class ProgressDetails: EventArgs
{
    public OrderStatus? PreStatus;
    public OrderStatus? NextStatus;
    public DateTime? StartTime;
    public DateTime? EndTime;
    public int? OrderId;
    public ProgressDetails(OrderStatus? pre, OrderStatus? next, DateTime? start, DateTime? end, int? orderId)
    {
        PreStatus = pre;
        NextStatus = next;
        StartTime = start;
        EndTime = end;
        OrderId = orderId;  
    }
}