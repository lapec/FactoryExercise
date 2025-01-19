using Application;

namespace tests;

public class CreateWorkOrderTests
{
    private User _user;
    private StartMachineUseCase _useCase;
    private List<WorkOrder> _inMemoryCollection; 
    private MockGateway Db { get; set; }
    
    [SetUp]
    public void Setup()
    {
        _user = new User();
        _inMemoryCollection = new List<WorkOrder>();
        
        Db = new MockGateway(_inMemoryCollection);
        _useCase = new StartMachineUseCase(_user, Db);
    }

    [Test]
    public void IfUserIsLoggedIn_canCreateWorkOrder()
    {
        _user.IsLoggedIn = true;
        var createdOrder = _useCase.Start([new WorkOrder(){Specification="XYOrder"}]);
        Assert.True(createdOrder.Count > 0);
    }

    [Test]
    public void IfUserIsNotLoggedIn_cannotCreateTheOrder()
    {
        var createdOrder = _useCase.Start([new WorkOrder(){Specification="XYOrder"}]);
        Assert.That(createdOrder, Is.EqualTo(new List<WorkOrder>()));
    }

    [Test]
    public void IfWorkOrderAlreadyExists_orderCannotBeCreated()
    {
        _user.IsLoggedIn = true;
        
        _inMemoryCollection.Add(new WorkOrder() {Status = "Released", Specification="XYOrder"});
        var createdOrders = _useCase.Start([new WorkOrder() { Status = "Released", Specification = "XYOrder" }]);
        Assert.That(createdOrders, Is.EqualTo(new List<WorkOrder>()));
    }

    [Test]
    public void IfDataProvided_orderWithProvidedDataIsCreated()
    {
        _user.IsLoggedIn = true;
        
        var order =_useCase.Start([new WorkOrder()
        {
            Specification="A202D",
            Machine = "TBMachine",
            Status = "Released"
        }]);
        
        Assert.That(order[0].Specification, Is.EqualTo("A202D"));
        Assert.That(order[0].Machine, Is.EqualTo("TBMachine"));
        Assert.That(order[0].Status, Is.EqualTo("Released"));
    }

    [Test]
    public void IfSteelCutterMachine_twoOrdersForTwoSidesAreCreated()
    {
        _user.IsLoggedIn = true;
        
        var order =_useCase.Start([
            new WorkOrder() { Id = "1", Specification="A202D", Machine = "SteelCutter", Status = "Released" },
            new WorkOrder() { Id = "2", Specification="A207R", Machine = "SteelCutter", Status = "Released" }
        ]);
        
        Assert.That(order[0].Specification, Is.EqualTo("A202D"));
        Assert.That(order[0].Machine, Is.EqualTo("SteelCutter"));
        Assert.That(order[0].Status, Is.EqualTo("Released"));
        Assert.That(order[1].Specification, Is.EqualTo("A207R"));
        Assert.That(order[1].Machine, Is.EqualTo("SteelCutter"));
        Assert.That(order[1].Status, Is.EqualTo("Released"));
    }
    
    [Test]
    public void IfOnlyOneOrderProvidedForSBCMachine_orderCannotBeCreated()
    {
        _user.IsLoggedIn = true;
        
        var ex = Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            _useCase.Start([
                new WorkOrder() { Id = "1", Specification="A202D", Machine = "SteelCutter", Status = "Released" },
            ]);
        });
        
        Assert.That(ex.Message, Is.EqualTo("This machine cannot start with less than or more than 2 orders"));
    }
}