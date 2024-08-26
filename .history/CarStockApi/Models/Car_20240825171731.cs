namespace CarStockApi.Models{
    public class Car{
        public int Car_ID {get; set;} // To differentiate between cars
        public string Make {get; set;}
        public string Model {get; set;}
        public int Year {get; set;}

        public int DealerID { get; set; }  // To differentiate between dealers
    }
}
