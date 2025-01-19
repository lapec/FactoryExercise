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

    public List<WorkOrder> GetReleasedOrderByPartName(string part)
    {
        return WorkOrders.Where(x => x.Status == "Released" && x.Specification == part).ToList();
    }

    public bool AreOrdersAlreadyReleased(List<WorkOrder> orders)
    {
        foreach (var order in orders)
        {
            return WorkOrders.Count(x => x.Specification == order.Specification && x.Status == "Released") > 0;
        }

        return false;
    }
}