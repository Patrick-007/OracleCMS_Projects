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
            // Fetch all cars for the dealer and return 
        }
    }

}