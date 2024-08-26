namespace CarStockApi.Models
{
    public class Car
    {
        public int Car_ID {get; set;} // To differentiate between cars
        public string Make {get; set;}= string.Empty;
        public string Model {get; set;}= string.Empty;
        public int Year {get; set;}
    }

    public class StockItem
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int StockLevel { get; set; }
    }
}
