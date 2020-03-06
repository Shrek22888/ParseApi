using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using ReposytoryPattern;
using ReposytoryPattern.Data.Entities;
using ParseApi.Models.Responce;
using ParseApi.Models.Request;
using System.Net;

namespace ParseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private AppDbContext _db;


        public CarsController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Get()
        {
            List<CarModel> cars = new List<CarModel>();
            
            foreach(Car car in _db.Cars)
            {
                CarModel carM = new CarModel { Name = car.Name, Year = car.Year };
                cars.Add(carM);
            }

            JsonContentResult result = new JsonContentResult();

            result.Content = JsonConvert.SerializeObject(cars, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            });
            result.StatusCode = (int)HttpStatusCode.OK;
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CarRequestModel req)
        {
            JsonContentResult result = new JsonContentResult();

            if(string.IsNullOrEmpty(req.Name) || string.IsNullOrEmpty(req.Year))
            {
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                result.Content = "Error all field should me matched";
                return result;
            }
            else
            {
                Car newCar = new Car { Name = req.Name, Year = req.Year };
                await _db.Cars.AddAsync(newCar);

                await _db.SaveChangesAsync();

                result.StatusCode = (int)HttpStatusCode.OK;
                result.Content = "New car added successfully";
                return result;
            }
                

        }

    }
}