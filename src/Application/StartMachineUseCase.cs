using tests;

namespace Application;

public class StartMachineUseCase
{
    public User User { get; set; }
    public Machine Machine { get; set; }
    private MockGateway _db { get; set; }

    public StartMachineUseCase(User user, MockGateway db)
    {
        this.User = user;
        this._db = db;
    }

    public List<WorkOrder> Start(List<WorkOrder> orders)
    {
        var createdOrders = new List<WorkOrder>();
        if(AnyValidationErrors(orders) != "valid") {return new List<WorkOrder>();}

        var machine = ChooseMachineToStart(orders);
        
        foreach (var order in machine.StartOrders())
            createdOrders.Add(_db.Create(order));
        
        return createdOrders;
    }

    private Machine ChooseMachineToStart(List<WorkOrder> orders)
    {
        switch (orders[0].Machine)
        {
            case "SteelCutter":
                return new SteelCutterMachine(orders);
                
            default:
                return new Machine(orders);
        }
    }

    private string AnyValidationErrors(List<WorkOrder> orders)
    {
        if(orders == new List<WorkOrder>()) 
            return "Machine cannot start, work orders are missing";

        if (orders.All(o => o.Machine != orders[0].Machine))
            return "This machine cannot start with more than 1 order";

        if(User.IsLoggedIn != true)
            return "User is not logged in";

        if(_db.AreOrdersAlreadyReleased(orders))
            return "This Work Order already exists and machine is already ready to start it";
        
        return "valid";
    }
}