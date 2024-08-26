using CarStockApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarStockApi.Data
{
    public class CarRepository
    {
        // Dictionary to store cars by DealerID
        private readonly Dictionary<int, List<Car>> _dealerCars = new Dictionary<int, List<Car>>();

        // Get all cars for a specific dealer
        public IEnumerable<Car> GetAll(int dealerId) 
        {
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId] : new List<Car>();
        }

        // Get a specific car by its ID for a specific dealer
        public Car GetById(int dealerId, int carId)
        {
            return _dealerCars.ContainsKey(dealerId) ? _dealerCars[dealerId].FirstOrDefault(car => car.Car_ID == carId) : null;
        }

        // Add a new car to a dealer's stock
        public void Add(int dealerId, Car car)
        {
            if (!_dealerCars.ContainsKey(dealerId))
            {
                _dealerCars[dealerId] = new List<Car>();
            }
            _dealerCars[dealerId].Add(car);
        }

        // Remove a car from a dealer's stock
        public void Remove(int dealerId, Car car)
        {
            if (_dealerCars.ContainsKey(dealerId))
            {
                _dealerCars[dealerId].Remove(car);
            }
        }

        // Update an existing car in a dealer's stock
        public void Update(int dealerId, Car car)
        {
            var existingCar = GetById(dealerId, car.Car_ID);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.StockLevel = car.StockLevel;
            }
        }

        // Search cars by make and model for a specific dealer
        public IEnumerable<Car> Search(int dealerId, string make, string model)
        {
            return _dealerCars.ContainsKey(dealerId) 
                ? _dealerCars[dealerId].Where(car => car.Make.ToLower().Contains(make.ToLower()) && car.Model.ToLower().Contains(model.ToLower())) 
                : new List<Car>();
        }
    }
}
