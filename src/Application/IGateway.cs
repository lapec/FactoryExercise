namespace Application;

public interface IGateway
{
    WorkOrder Create(WorkOrder workOrder);
    List<WorkOrder> GetAllOrders();
    List<WorkOrder> GetReleasedOrderByPartName();
}