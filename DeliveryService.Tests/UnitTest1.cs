using DeliveryServiceAppData;
using DeliveryServiceAppRepository;

namespace DeliveryService.Tests;

public class UnitTest1
{
    //* private backing field
    // new up repo here so you dont have to for each individual one
    private DeliveryRepository _delRepo;

    public UnitTest1()
    {
        _delRepo = new DeliveryRepository();
    }

    [Fact]
    public void AddDelivery_ShouldReturnTrue()
    {
        //*AAA
        // Arrange
       Delivery deliveryOne = new Delivery(new DateTime(2023,12,10), new DateTime(2023,12,12),OrderStatus.Scheduled);

       //Action
       bool hasPassed = _delRepo.AddNewDelivery(deliveryOne);

       // Assert
       Assert.True(hasPassed);
    }

    [Fact]
    public void CompareDeliveryCount_ShouldReturnCorrectCount()
    {
        int actualCount = _delRepo.ListAllDeliveries().Count();
        int expectedCount = 0;      // need to seed some deliveries to check for specific count

        Assert.Equal(expectedCount, actualCount);
    }

    [Fact]
    public void RemoveDelivery_ShouldReturnTrue()
    {
        Delivery deliveryTwo = new Delivery(new DateTime(2023,12,09), new DateTime(2023,12,05), OrderStatus.Canceled);

        _delRepo.AddNewDelivery(deliveryTwo);   // only delivery in repo, will have ID of 1

        bool wasRemoved = _delRepo.RemoveDelivery(1);   // pass in ID

        Assert.True(wasRemoved);
    }
}