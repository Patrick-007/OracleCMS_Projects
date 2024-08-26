using CarStockApi.Data;
using CarStockApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarStockApi.Controllers{
    // Defining the API route and marking the class as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase{
        private readonly CarRepository _respository;
        // Constructor to initialize the repository
        public CarsController(){
            _respository = new CarRepository();
        }

        // GET: api/cars/{dealerId}
        // Retrive all cars for a specific dealer
        [HttpGet("{dealerId}")]
        public ActionResult<IEnumerable<Car>> GetCars(int dealerId){
            // Fetch all cars for the dealer and return an OK (200) response with the list of cars
            return Ok(_respository.GetAll(dealerId));
        }

        // POST: api/cars/{dealerId}
        // Add a new car to the dealer's stock
        [HttpPost("{dealerId}")]
        public ActionResult AddCar(int dealerId, Car car){
            _respository.Add(dealerId, car); // Add the car the the repository

            // Return a Created (201) response with the URI of the newly created car
            return CreatedAtAction(nameof(GetCar), new {dealerId = dealerId, carId = car.Car_ID}, car);
        }

        // Get: api/cars/{dealerId}/{carId}
    }

}