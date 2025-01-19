namespace Application;

public class SteelCutterMachine(List<WorkOrder> orders) : Machine(orders)
{
    public override List<WorkOrder> StartOrders()
    {
        if (WorkOrders.Count != 2)
            throw new ArgumentException("This machine cannot start with less than or more than 2 orders");

        return WorkOrders;
    } 
}