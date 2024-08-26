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

        /// <summary>
        /// Retrieves all cars for a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <returns>A list of cars associated with the specified dealer.</returns>
        /// <response code="200">Returns the list of cars for the dealer.</response>
        [HttpGet("{dealerId}")]
        public ActionResult<IEnumerable<Car>> GetCars(int dealerId){
            return Ok(_repository.GetAll(dealerId));
        }

        /// <summary>
        /// Adds a new car to the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="car">The car object to be added.</param>
        /// <returns>The newly created car object.</returns>
        /// <response code="201">Returns the newly created car.</response>
        [HttpPost("{dealerId}")]
        public ActionResult AddCar(int dealerId, Car car){
            _repository.Add(dealerId, car); 
            return CreatedAtAction(nameof(GetCar), new {dealerId = dealerId, carId = car.Car_ID}, car);
        }

        /// <summary>
        /// Retrieves a specific car by its ID for a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car.</param>
        /// <returns>The requested car object.</returns>
        /// <response code="200">Returns the requested car.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpGet("{dealerId}/{carId}")]
        public ActionResult<Car> GetCar(int dealerId, int carId){
            var car = _repository.GetById(dealerId, carId);
            if(car == null)
                return NotFound();
            return Ok(car);
        }

        /// <summary>
        /// Updates an existing car in the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car to be updated.</param>
        /// <param name="car">The updated car object.</param>
        /// <returns>An HTTP status code indicating the result of the operation.</returns>
        /// <response code="204">Indicates that the car was successfully updated.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpPut("{dealerId}/{carId}")]
        public ActionResult UpdateCar(int dealerId, int carId, Car car){
            var existingCar = _repository.GetById(dealerId, carId);
            if (existingCar==null)
                return NotFound();
            
            _repository.Update(dealerId, car);
            return NoContent();
        }

        /// <summary>
        /// Removes a car from the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car to be removed.</param>
        /// <returns>An HTTP status code indicating the result of the operation.</returns>
        /// <response code="204">Indicates that the car was successfully removed.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpDelete("{dealerId}/{carId}")]
        public ActionResult DeleteCar(int dealerId, int carId){
            var car = _repository.GetById(dealerId, carId); // Fetch the car by ID
            if (car==null)
                return NotFound();

            _repository.Remove(dealerId, car); // Remove the car from repositry
            return NoContent(); 
        }

        // GET: api/cars/search/{dealerId}
        // Search for the car y make and model for a specific dealer
        [HttpGet("search/{dealerId}")]
        public ActionResult<IEnumerable<Car>> SearchCars(int dealerId, string make, string model){
            var cars = _repository.Search(dealerId, make, model); // Perform the search
            return Ok(cars);
        }
    }

}