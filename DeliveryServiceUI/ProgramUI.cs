using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliveryServiceAppData;
using DeliveryServiceAppRepository;


namespace DeliveryServiceUI
{
   public class ProgramUI
    {
        private readonly DeliveryRepository _deliveryRepo;

        public ProgramUI()
        {
            _deliveryRepo = new DeliveryRepository();
        }

        public void Run()
        {
            RunApp();
        }

        private void RunApp()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                System.Console.WriteLine("Welcome to Warner Transit Federal's Delivery Tracking Menu\n"+
                                        "1. Add New Delivery \n"+
                                        "2. List All Deliveries \n"+
                                        "3. List All EnRoute Deliveries \n"+
                                        "4. List All Completed Deliveries \n"+
                                        "5. Update Delivery Status \n"+
                                        "6. Cancel a delivery \n"+
                                        "-----------------------------------\n"+
                                        "0. Close App\n");
                int userInput = int.Parse(Console.ReadLine()!);
                switch (userInput)
                {
                    case 1:
                        AddDelivery();
                    break;
                    case 2:
                        ViewAllDeliveries();
                    break;
                    case 3:
                        ViewEnRouteDeliveries();
                    break;
                    case 4:
                        ViewCompletedDeliveries();
                    break;
                    case 5:
                        UpdateOrderStatus();
                    break;
                    case 6:
                        CancelDelivery();
                    break;
                    case 0:
                        isRunning = CloseApp();
                    break;
                    default:
                    break;
                }
            }
        }

        private void CancelDelivery()
        {
            List<Delivery> itemsInDB = _deliveryRepo.ListAllDeliveries();
            foreach (Delivery delivery in itemsInDB)
            {
                System.Console.WriteLine($"ID: {delivery.Id}\n");
            }
            System.Console.WriteLine("Please input a delivery ID.");
            int userInput = int.Parse(Console.ReadLine()!);
            Delivery itemInDb = _deliveryRepo.GetDelivery(userInput);
            if (itemInDb != null)
            {
                if (_deliveryRepo.RemoveDelivery(itemInDb.Id))
                {
                    System.Console.WriteLine("Success");
                }
                else
                {
                    System.Console.WriteLine("Failed to delete");
                }
            }
            else
            {
                System.Console.WriteLine("Could not find delivery");
            }
            PressAnyKey();
        }

        private void UpdateOrderStatus()    
        {
            List<Delivery> deliveriesInDb = _deliveryRepo.ListAllDeliveries();
            foreach (Delivery delivery in deliveriesInDb)
            {
                System.Console.WriteLine($"ID: {delivery.Id} | Status: {delivery.OrderStatus}");
            }
            System.Console.WriteLine("Please input the ID of the delivery you would like to update");
            int userInput = int.Parse(Console.ReadLine()!);
            Delivery selectedDelivery = _deliveryRepo.GetDelivery(userInput);
            if (selectedDelivery != null)
            {
                System.Console.WriteLine("Please input the number of the status that you would like the delivery to be updated to: \n"+
                                         "1. Scheduled \n"+
                                         "2. EnRoute \n"+
                                         "3. Complete \n"+
                                         "4. Canceled \n");
                if(int.TryParse(Console.ReadLine(), out int statusNumber))
                {
                   OrderStatus orderStatus = (OrderStatus)statusNumber;
                   selectedDelivery.OrderStatus = orderStatus;
                }
            }
        }

        private void ViewCompletedDeliveries()
        {
            List<Delivery> completedDeliveries = _deliveryRepo.ListCompletedDeliveries();
            foreach (Delivery delivery in completedDeliveries)
            {
                System.Console.WriteLine(delivery);
            }
            PressAnyKey();
        }

        private void ViewEnRouteDeliveries()
        {
            List<Delivery> enRouteDeliveries = _deliveryRepo.ListEnRouteDeliveries();
            foreach (Delivery delivery in enRouteDeliveries)
            {
                System.Console.WriteLine(delivery);
            }
            PressAnyKey();
        }

        private void ViewAllDeliveries()
        {
            List<Delivery> deliveries = _deliveryRepo.ListAllDeliveries();
            foreach (Delivery delivery in deliveries)
            {
                System.Console.WriteLine(delivery);
            }
            PressAnyKey();
        }

        private void AddDelivery()
        {
            Delivery delivery = new Delivery();
            System.Console.WriteLine("Enter an order date: ");
            delivery.OrderDate = DateTime.Parse(Console.ReadLine()!);
            System.Console.WriteLine("Enter a delivery date: ");
            delivery.DeliveryDate = DateTime.Parse(Console.ReadLine()!);
            //! not setting order status because all new deliveries start as scheduled
            delivery.OrderStatus = OrderStatus.Scheduled;
            // user can change later in update delivery method
            System.Console.WriteLine("Enter a quantity");
            delivery.Quantity = int.Parse(Console.ReadLine()!);
            // don't need to make an ID because count increases for each new delivery and is set to = ID 
            if (_deliveryRepo.AddNewDelivery(delivery))
            {
                System.Console.WriteLine("Delivery Added!");
            }
            else
            {
                System.Console.WriteLine("Delivery failed to add");
            }
            PressAnyKey();
        }

          private bool CloseApp()
        {
            System.Console.WriteLine("Thanks");
            return false;
        }

        private void PressAnyKey()
        {
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    
    }
}