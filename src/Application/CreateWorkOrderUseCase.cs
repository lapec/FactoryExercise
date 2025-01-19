using tests;

namespace Application;

public class CreateWorkOrderUseCase
{
    public User User { get; set; }
    private MockGateway _db { get; set; }

    public CreateWorkOrderUseCase(User user, MockGateway db)
    {
        this.User = user;
        this._db = db;
    }
    
    public WorkOrder CreateWorkOrder(WorkOrder order)
    {
        if(HasValidationErrors()) {return new WorkOrder();};
        return _db.Create(order);
    }
    
    private bool  HasValidationErrors() => User.IsLoggedIn != true || HasReleasedOrders();
    private bool HasReleasedOrders() => _db.GetReleasedOrderByPartName().Count() > 0;
    
}