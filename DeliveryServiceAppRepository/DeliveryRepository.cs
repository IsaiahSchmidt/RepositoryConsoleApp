using DeliveryServiceAppData;

namespace DeliveryServiceAppRepository
{
    public class DeliveryRepository 
    {
        private readonly List<Delivery> _deliveryDbContent = new List<Delivery>();
        // increment count for id (++)
        private int _idCount = 0;

        public bool AddNewDelivery(Delivery delivery)
        {
            if (delivery is null)
            {
                return false;
            }
            else
            {
                _idCount++;
                delivery.Id = _idCount;
                _deliveryDbContent.Add(delivery);
                return true;
            }
        }

        public List<Delivery> ListAllDeliveries()
        {
            return _deliveryDbContent;
        }

        public Delivery GetDelivery(int idSelection)
        {
            return _deliveryDbContent.FirstOrDefault(del => del.Id == idSelection)!;
        }

        public List<Delivery> ListEnRouteDeliveries(int idSelection)
        {
            List<Delivery> enRouteDeliveries = new List<Delivery>();
            foreach (Delivery delivery in _deliveryDbContent)
            {
                if (delivery.OrderStatus == OrderStatus.EnRoute)
                {
                    enRouteDeliveries.Add(delivery);
                }
                else
                {
                    Console.WriteLine("failed to list deliveries...");
                }
            } 
            return enRouteDeliveries;
        }

        public List<Delivery> ListCompletedDeliveries()
        {
            List<Delivery> completedDeliveries = new List<Delivery>();
            foreach (Delivery delivery in _deliveryDbContent)
            {
                if (delivery.OrderStatus == OrderStatus.Complete)
                {
                    completedDeliveries.Add(delivery);
                }
            }
            return completedDeliveries;
        }

        public bool UpdateDeliveryStatus(int _idCount, Delivery newDelivery)
        {
            Delivery deliveryInDatabase = GetDelivery(_idCount);
            if (deliveryInDatabase != null)
            {
                deliveryInDatabase.OrderStatus = newDelivery.OrderStatus;
                return true;
            }
            return false;
        }

        public bool RemoveDelivery(int idSelection)
        {
            var deliveryInDB = GetDelivery(idSelection);
            return _deliveryDbContent.Remove(deliveryInDB);
        }
        
    }
}