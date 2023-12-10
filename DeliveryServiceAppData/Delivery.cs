namespace DeliveryServiceAppData;


public class Delivery
{
    public Delivery(){}

    public Delivery(DateTime orderDate, DateTime deliveryDate, OrderStatus orderStatus)
    {
        OrderDate = orderDate;
        DeliveryDate = deliveryDate;
        OrderStatus = orderStatus;
        //! do not include ints here
    }

    //order date
    public DateTime OrderDate {get; set;}
    //delivery date
    public DateTime DeliveryDate {get; set;}    // to pass in as parameter: new DateTime(YYYY,MM,DD)
    //order status 
    public OrderStatus OrderStatus {get; set;}
    //item number 
    public int ItemNumber {get; set;}
    //item quantity
    public int Quantity {get; set;}
    //customer ID (unique to each)
    public int Id {get; set;}
 
    public override string ToString()
    {
        string str = $"Order Date: {OrderDate}\n"+
                    $"Delivery Date: {DeliveryDate}\n"+
                    $"Order Status: {OrderStatus}\n"+
                    $"ItemNumber: {ItemNumber}\n"+
                    $"Quantity: {Quantity}\n"+
                    $"Customer ID: {Id}\n"+
                    "===============================\n";

        return str;
    }
}