namespace Application;

public class Machine
{
    public enum MachineType
    {
        Default,
        SteelCutter,
    }
    
    public string MachineName { get; set; } = "";
    protected List<WorkOrder> WorkOrders { get; set; }

    public Machine(List<WorkOrder> workOrders) => WorkOrders = workOrders;
    public virtual List<WorkOrder> StartOrders()
    {
        if (WorkOrders.Count > 1)
            throw new ArgumentException("This machine cannot start with more than 1 order");

        return WorkOrders;
    }
}