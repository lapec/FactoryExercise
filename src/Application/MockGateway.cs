namespace Application;

public class MockGateway : IGateway
{
    private List<WorkOrder> WorkOrders { get; set; }
    public MockGateway(List<WorkOrder> WorkOrders)
    {
        this.WorkOrders = WorkOrders;
    }
    
    public WorkOrder Create(WorkOrder workOrder)
    {
        WorkOrders.Add(workOrder);
        return WorkOrders[^1];
    }

    public List<WorkOrder> GetAllOrders()
    {
        return WorkOrders;
    }

    public List<WorkOrder> GetReleasedOrderByPartName()
    {
        return WorkOrders.Where(x => x.Status == "Released").ToList();
    }
}