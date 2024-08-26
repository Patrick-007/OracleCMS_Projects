using CarStockApi.Data;
using CarStockApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CarStockApi.Controllers
{
    // Defining the API route and marking the class as an API controller
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private CarRepository _repository;

        // Constructor to initialize the repository
        public CarsController()
        {
            _repository = new CarRepository();
        }

        /// <summary>
        /// Retrieve the stocklist for a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <returns>A stocklist of cars with Make, Model, Year, and StockLevel.</returns>
        /// <response code="200">Returns the stocklist for the dealer.</response>
        [HttpGet("{dealerId}")]
        public ActionResult<IEnumerable<dynamic>> GetCars(int dealerId)
        {
            _repository.UpdateStock(dealerId); // Ensure stock is up to date before returning
            return Ok(_repository.GetAll(dealerId));
        }

        /// <summary>
        /// Add a new car to the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="car">The car object to be added.</param>
        /// <returns>The newly created car object.</returns>
        /// <response code="201">Returns the newly created car.</response>
        [HttpPost("{dealerId}")]
        public ActionResult AddCar(int dealerId, Car car)
        {
            _repository.Add(dealerId, car); 
            _repository.UpdateStock(dealerId); // Update stock after adding the car
            return CreatedAtAction(nameof(GetCar), new { dealerId = dealerId, carId = car.Car_ID }, car);
        }

        /// <summary>
        /// Retrieve a specific car by its ID for a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car.</param>
        /// <returns>The requested car object.</returns>
        /// <response code="200">Returns the requested car.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpGet("{dealerId}/{carId}")]
        public ActionResult<Car> GetCar(int dealerId, int carId)
        {
            var car = _repository.GetById(dealerId, carId);
            if (car == null)
                return NotFound();
            return Ok(car);
        }

        /// <summary>
        /// Update an existing car in the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car to be updated.</param>
        /// <param name="car">The updated car object.</param>
        /// <returns>An HTTP status code indicating the result of the operation.</returns>
        /// <response code="204">Indicates that the car was successfully updated.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpPut("{dealerId}/{carId}")]
        public ActionResult UpdateCar(int dealerId, int carId, Car car)
        {
            var existingCar = _repository.GetById(dealerId, carId);
            if (existingCar == null)
                return NotFound();

            _repository.Update(dealerId, car);
            _repository.UpdateStock(dealerId); // Update stock after modifying the car
            return NoContent();
        }

        /// <summary>
        /// Remove a car from the dealer's stock.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="carId">The unique identifier of the car to be removed.</param>
        /// <returns>An HTTP status code indicating the result of the operation.</returns>
        /// <response code="204">Indicates that the car was successfully removed.</response>
        /// <response code="404">If the car is not found.</response>
        [HttpDelete("{dealerId}/{carId}")]
        public ActionResult DeleteCar(int dealerId, int carId)
        {
            var car = _repository.GetById(dealerId, carId);
            if (car == null)
                return NotFound();

            _repository.Remove(dealerId, car);
            _repository.UpdateStock(dealerId); // Update stock after removing the car
            return NoContent();
        }

        /// <summary>
        /// Search for cars by make and model for a specific dealer.
        /// </summary>
        /// <param name="dealerId">The unique identifier of the dealer.</param>
        /// <param name="make">The make of the car.</param>
        /// <param name="model">The model of the car.</param>
        /// <returns>A list of cars matching the search criteria.</returns>
        /// <response code="200">Returns the list of matching cars.</response>
        [HttpGet("search/{dealerId}")]
        public ActionResult<IEnumerable<Car>> SearchCars(int dealerId, string make, string model)
        {
            var cars = _repository.Search(dealerId, make, model);
            return Ok(cars);
        }
    }
}
