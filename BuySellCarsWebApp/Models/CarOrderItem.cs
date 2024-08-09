namespace BuySellCarsWebApp.Models
{
    public class CarOrderItem : OrderItem
    {
        public int CarId { get; set; }
        public Car Car { get; set; }
    }

}
