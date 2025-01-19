using Application;

namespace tests;

public class CreateWorkOrderTests
{
    private User _user;
    private CreateWorkOrderUseCase _useCase;
    private List<WorkOrder> _inMemoryCollection; 
    private MockGateway Db { get; set; }
    
    [SetUp]
    public void Setup()
    {
        _user = new User();
        _inMemoryCollection = new List<WorkOrder>();
        
        Db = new MockGateway(_inMemoryCollection);
        _useCase = new CreateWorkOrderUseCase(_user, Db);
    }

    [Test]
    public void IfUserIsLoggedIn_canCreateWorkOrder()
    {
        _user.IsLoggedIn = true;
        _useCase.CreateWorkOrder(new WorkOrder(){Specification="XYOrder"});
        Assert.True(IsOrderCreated());
    }

    [Test]
    public void IfUserIsNotLoggedIn_cannotCreateTheOrder()
    {
        _useCase.CreateWorkOrder(new WorkOrder(){Specification="XYOrder"});
        Assert.False(IsOrderCreated());
    }

    [Test]
    public void IfWorkOrderAlreadyExists_orderCannotBeCreated()
    {
        _user.IsLoggedIn = true;
        _inMemoryCollection.Add(new WorkOrder() {Status = "Released", Specification="YOrder"});
        
        _useCase.CreateWorkOrder(new WorkOrder(){Status = "Released", Specification="XYOrder"});
        
        Assert.False(IsOrderCreated());
    }

    [Test]
    public void IfDataProvided_orderWithProvidedDataIsCreated()
    {
        _user.IsLoggedIn = true;
        
        var order =_useCase.CreateWorkOrder(new WorkOrder()
        {
            Specification="A202D",
            Machine = "SteelCutter",
            Status = "Released"
        });
        
        Assert.AreEqual("A202D", order.Specification);
        Assert.AreEqual("SteelCutter", order.Machine);
        Assert.AreEqual("Released", order.Status);
    }

    private bool IsOrderCreated()
    {
        return Db.GetAllOrders().Where(x=>x.Specification=="XYOrder").ToList().Count == 1;
    }
}