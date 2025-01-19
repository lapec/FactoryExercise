namespace Application;

public interface IGateway
{
    WorkOrder Create(WorkOrder workOrder);
    List<WorkOrder> GetAllOrders();
    bool AreOrdersAlreadyReleased(List<WorkOrder> orders);
}