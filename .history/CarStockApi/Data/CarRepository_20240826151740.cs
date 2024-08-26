using CarStockApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarStockApi.Data{
    public class CarRepository{
        // Dictionary to store cars by DealerID
        private Dictionary<int, List<Car>> _dealerCars;

        // Dictionary to store Stocklist by DealerID
        private Dictionary<int, List<StockItem>> _dealerStockList;

        public CarRepository(){
            _dealerCars = new Dictionary<int, List<Car>>{
                { 
                    0, new List<Car> // Dealer with ID 0
                    {
                        new Car { Car_ID = 0, Make = "audi", Model = "a4", Year = 2018},
                        new Car { Car_ID = 1, Make = "audi", Model = "a4", Year = 2017},
                        new Car { Car_ID = 2, Make = "bmw", Model = "x5", Year = 2020},
                        new Car { Car_ID = 3, Make = "bmw", Model = "x5", Year = 2020}
                    }
                },
                { 
                    1, new List<Car> // Dealer with ID 1
                    {
                        new Car { Car_ID = 0, Make = "tesla", Model = "model 3", Year = 2021},
                        new Car { Car_ID = 1, Make = "mercedes", Model = "c-class", Year = 2019},
                        new Car { Car_ID = 2, Make = "mercedes", Model = "c-class", Year = 2019},
                        new Car { Car_ID = 3, Make = "mercedes", Model = "c-class", Year = 2019}
                    }
                }
            };

            _dealerStockList = new Dictionary<int, List<StockItem>>();
            UpdateAllStocks(); // Initialize stock lists for all dealers
        }

        // Update the stock list for all dealers
        public void UpdateAllStocks(){
            foreach (var dealerId in _dealerCars.Keys){
                UpdateStock(dealerId);
            }
        }

        // Update the stock list for a specific dealer
        public void UpdateStock(int dealerId){
            if (_dealerCars.ContainsKey(dealerId)){
                var stockList = _dealerCars[dealerId]
                    .GroupBy(car => new { car.Make, car.Model, car.Year })
                    .Select(group => new StockItem {
                        Make = group.Key.Make,
                        Model = group.Key.Model,
                        Year = group.Key.Year,
                        StockLevel = group.Count()
                    }).ToList();

                _dealerStockList[dealerId] = stockList;
            }
        }

        // Get the stock list for a specific dealer
        public IEnumerable<StockItem> GetAll(int dealerId){
            return _dealerStockList.ContainsKey(dealerId) ? _dealerStockList[dealerId] : new List<StockItem>();
        }

        // Get a specific car by its ID for a specific dealer
        public Car GetById(int dealerId, int carId){
            var car = _repository.GetById(dealerId, carId);
            if (car == null)
            {
                // Handle the null case, e.g., return a NotFound result in a controller action
                return NotFound();
            }
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId].FirstOrDefault(car => car.Car_ID == carId) : null;
        }

        // Add a new car to a dealer's stock
        public void Add(int dealerId, Car car){
            if (!_dealerCars.ContainsKey(dealerId)){
                _dealerCars[dealerId] = new List<Car>();
            }

            // Ensure Car_ID is unique within the dealer's inventory
            car.Car_ID = _dealerCars[dealerId].Count > 0 
                ? _dealerCars[dealerId].Max(c => c.Car_ID) + 1 
                : 0;

            _dealerCars[dealerId].Add(car);
            UpdateStock(dealerId); // Update the stock list after adding a car
        }

        // Remove a car from a dealer's stock
        public void Remove(int dealerId, Car car){
            if (_dealerCars.ContainsKey(dealerId)){
                var existingCar = _dealerCars[dealerId].FirstOrDefault(c => c.Car_ID == car.Car_ID);
                if (existingCar != null){
                    _dealerCars[dealerId].Remove(existingCar); // Remove the car if it exists
                    UpdateStock(dealerId); // Update the stock list after removing a car
                }
            }
        }

        // Update an existing car in a dealer's stock
        public void Update(int dealerId, Car car){
            if (_dealerCars.ContainsKey(dealerId)){
                var existingCar = GetById(dealerId, car.Car_ID);
                if (existingCar != null){
                    existingCar.Make = car.Make;
                    existingCar.Model = car.Model;
                    existingCar.Year = car.Year;
                    UpdateStock(dealerId); // Update the stock list after updating a car
                }
            }
        }

        // Search cars by make and model for a specific dealer
        public IEnumerable<Car> Search(int dealerId, string make, string model){
            return _dealerCars.ContainsKey(dealerId) 
                ? _dealerCars[dealerId].Where(car => car.Make.ToLower().Contains(make.ToLower()) && car.Model.ToLower().Contains(model.ToLower())) 
                : new List<Car>();
        }
    }
}
