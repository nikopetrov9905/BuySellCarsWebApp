namespace BuySellCarsWebApp.Models
{
    public class CarPartOrderItem : OrderItem
    {
        public int CarPartId { get; set; }
        public CarPart CarPart { get; set; }
    }

}
