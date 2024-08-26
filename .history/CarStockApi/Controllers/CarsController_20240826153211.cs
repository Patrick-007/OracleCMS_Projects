using CarStockApi.Data;
using CarStockApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CarStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRepository _repository;

        public CarsController(CarRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{dealerId}")]
        public ActionResult<IEnumerable<dynamic>> GetCars(int dealerId)
        {
            try
            {
                _repository.UpdateStock(dealerId);
                return Ok(_repository.GetAll(dealerId));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("{dealerId}")]
        public ActionResult AddCar(int dealerId, Car car)
        {
            car.Make = car.Make.ToLower();
            car.Model = car.Model.ToLower();

            try
            {
                _repository.Add(dealerId, car);
                _repository.UpdateStock(dealerId);
                return CreatedAtAction(nameof(GetCar), new { dealerId = dealerId, carId = car.Car_ID }, car);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{dealerId}/{carId}")]
        public ActionResult<Car> GetCar(int dealerId, int carId)
        {
            try
            {
                var car = _repository.GetById(dealerId, carId);
                return Ok(car);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{dealerId}/{carId}")]
        public ActionResult UpdateCar(int dealerId, int carId, Car car)
        {
            car.Make = car.Make.ToLower();
            car.Model = car.Model.ToLower();

            try
            {
                _repository.Update(dealerId, car);
                _repository.UpdateStock(dealerId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{dealerId}/{carId}")]
        public ActionResult DeleteCar(int dealerId, int carId)
        {
            try
            {
                var car = _repository.GetById(dealerId, carId);
                _repository.Remove(dealerId, car);
                _repository.UpdateStock(dealerId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("search/{dealerId}")]
        public ActionResult<IEnumerable<Car>> SearchCars(int dealerId, string make, string model)
        {
            make = make.ToLower();
            model = model.ToLower();

            try
            {
                var cars = _repository.Search(dealerId, make, model);
                return Ok(cars);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
