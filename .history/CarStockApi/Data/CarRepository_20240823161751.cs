using CarStockApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarStockApi.Data{
    public class CarRepository{
        // A private list to store the cars in memory
        private readonly List<Car> _cars = new List<Car>();

        // Method to retrive all cars in the respository
        public IEnumerable<Car> GetAll() => _cars;

        // Method to retrieve a specific car based on its car ID
        public Car GetById(int carId) => _cars.FirstOrDefault(car => car.Car_ID == carId);

        public void Add(Car car) => _cars.Add(car);

        public void Remove(int carId){
            var car = GetById(carId);
            if (car != null) _cars.Remove
        }





    }
}