using CarFinderSvc.Entities;
using CarFinderSvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace CarFinderSvc.Controllers
{
    /// <summary>
    /// Cars Controller to return car data/details
    /// </summary>
    [RoutePrefix("cars")]
    public class CarsController : ApiController
    {
        private CarFinderSvcEntities db = new CarFinderSvcEntities();
        CustomCarData ccd = new CustomCarData();

        /// <summary>
        /// Returns all car makes
        /// </summary>
        /// <param name="CarMakesPerPage">Enter the number of car makes you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarMakes")]
        public IHttpActionResult GetCarMakes(int CarMakesPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the car makes based on paging parameters
            var carMakes = db.GetCarMakes()
                .Take(CarMakesPerPage)
                .ToList();

            //through error code and message in case object is null
            if (carMakes == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = carMakes,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarMakesPerPage,
                    ReturnedResults = carMakes.Count
                }
            });
        }


        /// <summary>
        /// Returns all car makes by year
        /// </summary>
        /// <param name="year">Enter the year of cars you are searching for</param>
        /// <param name="CarMakesPerPage">Enter the number of car makes you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMakesByYear")]
        public IHttpActionResult GetCarMakeByYear (string year, int CarMakesPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the car makes based on paging parameters
            var makesBY = db.GetCarMakesByYear(year)
                .Take(CarMakesPerPage)
                .ToList();

            //through error code and message in case object is null
            if (makesBY == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = makesBY,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarMakesPerPage,
                    ReturnedResults = makesBY.Count
                }
            });
        }


        /// <summary>
        /// Returns all car models
        /// </summary>
        /// <param name="CarModelsPerPage">Enter the number of car models you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarModel")]
        public IHttpActionResult GetCarModel(int CarModelsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the car models based on paging parameters
            var models = db.GetCarModel()
                .Take(CarModelsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (models == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = models,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarModelsPerPage,
                    ReturnedResults = models.Count
                }
            });
        }


        /// <summary>
        /// Returns cars by engine size
        /// </summary>
        /// <param name="engineSize">Enter the engine size you are searching for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsByEngineSize")]
        [ResponseType(typeof(GetCarsByEngineSize_Result))]
        public IHttpActionResult GetCarsByEngineSize(string engineSize, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var es = db.GetCarsByEngineSize(engineSize)
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats)  ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (es == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            //return Ok(es);

            //Return the list of cars
            return Ok(new
            {
                Data = es,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = es.Count
                }
            });
        }


        /// <summary>
        /// Returns cars by horsepower
        /// </summary>
        /// <param name="horsePower">Enter the amount of horsepower you are searching for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsByHorsePower")]
        [ResponseType(typeof(GetCarsByHorsePower_Result))]
        public IHttpActionResult GetCarsByHorsePower(string horsePower, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var hp = db.GetCarsByHorsePower(horsePower)
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (hp == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = hp,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = hp.Count
                }
            });
        }


        /// <summary>
        /// Returns cars by weight
        /// </summary>
        /// <param name="kg">Enter the weight in kilograms of the cars you are searching for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsByWeight")]
        [ResponseType(typeof(GetCarsByWeight_Result))]
        public IHttpActionResult GetCarsByWeight(string kg, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var weight = db.GetCarsByWeight(kg)
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (weight == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = weight.ToList(),
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = weight.Count
                }
            });
        }


        /// <summary>
        /// Returns cars by year, make, model, and trim
        /// </summary>
        /// <param name="year">Enter the year of the cars you are searching for</param>
        /// <param name="make">Enter the make of the cars you are searching for</param>
        /// <param name="model">Enter the model of the cars you are searching for</param>
        /// <param name="trim">Enter the trim of the cars you are searching for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsByYearMakeModelTrim")]
        [ResponseType(typeof(GetCarsByYearMakeModelTrim_Result))]
        public IHttpActionResult GetCarsByYearMakeModelTrim(string year, string make, string model, string trim, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var ymmt = db.GetCarsByYearMakeModelTrim(year, make, model, trim)
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (ymmt == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = ymmt,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = ymmt.Count
                }
            });
        }


        /// <summary>
        /// Returns cars with over 300HP
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsGreaterThan300HP")]
        [ResponseType(typeof(GetCarsGreaterThan300HP_Result))]
        public IHttpActionResult GetCarsGreaterThan300HP(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var grhp = db.GetCarsGreaterThan300HP()
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (grhp == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = grhp,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = grhp.Count
                }
            });
        }


        /// <summary>
        /// Returns cars under 2000kg
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsUnder2000kg")]
        [ResponseType(typeof(GetCarsUnder2000kg_Result))]
        public IHttpActionResult GetCarsUnder2000kg(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var unkg = db.GetCarsUnder2000kg()
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();
            //through error code and message in case object is null
            if (unkg == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = unkg,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = unkg.Count
                }
            });
        }


        /// <summary>
        /// Returns cars under 2000kg with over 300HP
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarsUnder2000kgGreater300HP")]
        [ResponseType(typeof(GetCarsUnder2000kgGreater300HP_Result))]
        public IHttpActionResult GetCarsUnder2000kgGreater300HP(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var uggr = db.GetCarsUnder2000kgGreater300HP()
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (uggr == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = uggr,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = uggr.Count
                }
            });
        }


        /// <summary>
        /// Returns list of all car trims
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCarTrimLevels")]
        public IHttpActionResult GetCarTrimLevels(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var ctl = db.GetCarTrimLevels()
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (ctl == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = ctl,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = ctl.Count()
                }
            });
        }


        /// <summary>
        /// Returns all mid engine cars
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetMidEngineCars")]
        [ResponseType(typeof(GetMidEngineCars_Result))]
        public IHttpActionResult GetMidEngineCars(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var mec = db.GetMidEngineCars()
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (mec == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = mec,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = mec.Count
                }
            });
        }


        /// <summary>
        /// Returns car models by any given year and make
        /// </summary>
        /// <param name="year">Enter the year of the cars you are searching for</param>
        /// <param name="make">Enter the make of the cars you are searching for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetModelsByGivenYearMake")]
        public IHttpActionResult GetModelsByGivenYearMake(string year, string make, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var mod = db.GetModelsByGivenYearMake(year, make)
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (mod == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = mod,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = mod.Count()
                }
            });
        }


        /// <summary>
        /// Returns car models by any given by year, make, model
        /// </summary>
        /// <param name="year">Enter the year of the cars you are looking for</param>
        /// <param name="make">Enter the make of the cars you are looking for</param>
        /// <param name="model">Enter the model of the cars you are looking for</param>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetModelTrimsByYearMakeModel")]
        public IHttpActionResult GetModelTrimsByYearMakeModel(string year, string make, string model, int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var trim = db.GetModelTrimsByYearMakeModel(year, make, model)
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (trim == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = trim,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = trim.Count()
                }
            });
        }


        /// <summary>
        /// Returns car models by year
        /// </summary>
        /// <param name="CarsPerPage">Enter the number of cars you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetModelYears")]
        public IHttpActionResult GetModelYears(int CarsPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the cars based on paging parameters
            var mody = db.GetModelYears()
                .Take(CarsPerPage)
                .ToList();

            //through error code and message in case object is null
            if (mody == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = mody,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = CarsPerPage,
                    ReturnedResults = mody.Count()
                }
            });
        }


        /// <summary>
        /// Returns a list of SUV's
        /// </summary>
        /// <param name="SUVPerPage">Enter the number of SUV's you would like to display</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSUV")]
        [ResponseType(typeof(GetSUV_Result))]
        public IHttpActionResult GetSUV(int SUVPerPage = 63185)
        {
            // Get total number of records
            int total = db.Cars.Count();

            // Select the suv's based on paging parameters
            var suv = db.GetSUV()
                .Select(x => new CustomCarData
                {
                    Doors = string.IsNullOrEmpty(x.doors) ? "Sorry...No Details" : x.doors,
                    Seats = string.IsNullOrEmpty(x.seats) ? "Sorry...No Details" : x.seats,
                    Model = string.IsNullOrEmpty(x.model) ? "Sorry...No Details" : x.model,
                    Trim = string.IsNullOrEmpty(x.trim) ? "Sorry...No Details" : x.trim,
                    Weight = string.IsNullOrEmpty(x.weight_kg) ? "Sorry...No Details" : x.weight_kg,
                    EngineSize = string.IsNullOrEmpty(x.engine_cyl) ? "Sorry...No Details" : x.engine_cyl,
                    HorsePower = string.IsNullOrEmpty(x.engine_power_ps) ? "Sorry...No Details" : x.engine_power_ps,
                    Make = string.IsNullOrEmpty(x.make) ? "Sorry...No Details" : x.make,
                    Year = string.IsNullOrEmpty(x.year) ? "Sorry...No Details" : x.year,
                    Bodystyle = string.IsNullOrEmpty(x.body_style) ? "Sorry...No Details" : x.body_style,
                    EngineType = string.IsNullOrEmpty(x.engine_type) ? "Sorry...No Details" : x.engine_type,
                    TransmissionType = string.IsNullOrEmpty(x.transmission_type) ? "Sorry...No Details" : x.transmission_type,
                    EnginePosition = string.IsNullOrEmpty(x.engine_position) ? "Sorry...No Details" : x.engine_position
                })
                .Take(SUVPerPage)
                .ToList();

            //through error code and message in case object is null
            if (suv == null)
            {
                return Content(HttpStatusCode.NotFound, "Car not found");
            }

            // Return the list of cars
            return Ok(new
            {
                Data = suv,
                Paging = new
                {
                    TotalInventory = total,
                    PageLimit = SUVPerPage,
                    ReturnedResults = suv.Count
                }
            });
        }
    }
}
