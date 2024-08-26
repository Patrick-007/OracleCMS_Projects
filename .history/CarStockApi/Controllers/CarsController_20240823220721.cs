using CarStockApi.Data;
using CarStockApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarStockApi.Controllers{
    // Defining the API route and marking the class as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase{
        private readonly CarRepository _repository;
        // Constructor to initialize the repository
        public CarsController(){
            _repository = new CarRepository();
        }

        // GET: api/cars/{dealerId}
        // Retrive all cars for a specific dealer
        [HttpGet("{dealerId}")]
        public ActionResult<IEnumerable<Car>> GetCars(int dealerId){
            // Fetch all cars for the dealer and return an OK (200) response with the list of cars
            return Ok(_repository.GetAll(dealerId));
        }

        // POST: api/cars/{dealerId}
        // Add a new car to the dealer's stock
        [HttpPost("{dealerId}")]
        public ActionResult AddCar(int dealerId, Car car){
            _repository.Add(dealerId, car); // Add the car the the repository

            // Return a Created (201) response with the URI of the newly created car
            return CreatedAtAction(nameof(GetCar), new {dealerId = dealerId, carId = car.Car_ID}, car);
        }

        // Get: api/cars/{dealerId}/{carId}
        // Retrieve a specific car by its ID for a specific dealer
        [HttpGet("{dealerId}/{carId}")]
        public ActionResult<Car> GetCar(int dealerId, int carId){
            var car = _repository.GetById(dealerId, carId); // Fetch the car by ID
            if(car == null)
                return NotFound(); // Return a Not Found (404) response if the car doesn't exist
            return Ok(car); // Return an OK (200) response with the car details

        }
    }

}