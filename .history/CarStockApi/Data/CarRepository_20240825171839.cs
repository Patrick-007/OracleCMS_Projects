using CarStockApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarStockApi.Data{
    public class CarRepository{
        // Dictionary to store cars by DealerID
        private Dictionary<int, List<Car>> _dealerCars = new Dictionary<int, List<Car>>();

        public CarRepository(){
            _dealerCars = new Dictionary<int, List<Car>>{
                { 
                    1, new List<Car> // Dealer with ID 1
                    {
                        new Car { Car_ID = 1, Make = "Audi", Model = "A4", Year = 2018},
                        
                        new Car { Car_ID = 2, Make = "BMW", Model = "X5", Year = 2020}
                    }
                },
                { 
                    2, new List<Car> // Dealer with ID 2
                    {
                        new Car { Car_ID = 3, Make = "Tesla", Model = "Model 3", Year = 2021},
                        new Car { Car_ID = 4, Make = "Mercedes", Model = "C-Class", Year = 2019}
                    }
                }
            };
        }

        // Get all cars for a specific dealer
        public IEnumerable<Car> GetAll(int dealerId){
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId] : new List<Car>();
        }

        // Get a specific car by its ID for a specific dealer
        public Car GetById(int dealerId, int carId){
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId].FirstOrDefault(car => car.Car_ID == carId) : null;
        }

        // Add a new car to a dealer's stock
        public void Add(int dealerId, Car car){
            if (!_dealerCars.ContainsKey(dealerId)){
                _dealerCars[dealerId] = new List<Car>();
            }
            _dealerCars[dealerId].Add(car);
        }

        // Remove a car from a dealer's stock
        public void Remove(int dealerId, Car car){
            if (_dealerCars.ContainsKey(dealerId)){
                _dealerCars[dealerId].Remove(car);
            }
        }

        // Update an existing car in a dealer's stock
        public void Update(int dealerId, Car car){
            var existingCar = GetById(dealerId, car.Car_ID);
            if (existingCar != null){
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
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
