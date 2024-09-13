
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Regstration.Models;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using Microsoft.AspNetCore.Hosting;
//using Regstration.Data;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.Data.SqlClient;
//using System.Globalization;
//using Application.Services;
//using Domain.Entities;
//using Microsoft.Extensions.Caching.Memory;

//namespace Regstration.Controllers
//{
//    public class AddBusesController : Controller
//    {


//        private readonly ILogger<AddBusesController> _logger;
//        private readonly IWebHostEnvironment _env;
//        private readonly IMemoryCache _memoryCache;


//        private readonly BusService _busService;

//        public AddBusesController(ILogger<AddBusesController> logger, IWebHostEnvironment env, BusService busService , IMemoryCache memoryCache)
//        {
//            _logger = logger;
//            _env = env;

//            _busService = busService;
//            _memoryCache = memoryCache;
//        }



//        public IActionResult Index()
//        {
//            return View();
//        }

//        //[HttpGet]
//        ////public IActionResult ShowInfo()
//        //{
//        //    // Fetch the data from the repository
//        //    var buses = _busService.GetAll().ToList();

//        //    // Return the data as JSON
//        //    return Json(buses);
//        //}

//        [HttpGet]
//        public IActionResult ShowInfo()
//        {
//            string cacheKey = "allBuses";
//            List<Buses> buses;

//            // Check if the cache already contains the data
//            if (!_memoryCache.TryGetValue(cacheKey, out buses))
//            {
//                // If not in cache, fetch the data from the service
//                buses = _busService.GetAll().ToList();

//                // Set cache options
//                var cacheOptions = new MemoryCacheEntryOptions()
//                    .SetSlidingExpiration(TimeSpan.FromMinutes(10)); // Cache expires after 30 minutes of inactivity

//                // Cache the data
//                _memoryCache.Set(cacheKey, buses, cacheOptions);
//            }

//            return View(buses);
//        }



//        /* before repossive 
//        public IActionResult ShowInfo()
//        {
//            // Fetch the data from the repository
//            var buses = _busService.GetAll().ToList();

//            // Return the data as JSON
//            return View(buses);
//        }


//        */

//        //[HttpPost]
//        //public IActionResult showInfo(string busName, string busNumber, string phoneNumber, string ticketPrice, string timing)
//        //{
//        //    List<Buses> buses = (List<Buses>)rep.GetAll();
//        //    return View(buses);
//        //}

//        [Authorize(Policy = "adminonly")]


//        public IActionResult showInfo1()
//        {
//            List<Buses> buses = (List<Buses>)_busService.GetAll();
//            return View(buses);
//        }

//        [HttpGet]

//        [Authorize(Policy = "adminonly")]
//        public IActionResult AvailableBuses()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Authorize(Policy = "adminonly")]
//        public IActionResult AvailableBuses(string BusNumber, string BusName, string PhoneNumber, decimal TicketPrice, IFormFile Image, TimeSpan Timing, string DepartureLocation, string ArrivalLocation)
//        {
//            var existingBus = _busService.GetAll().FirstOrDefault(b => b.BusNumber == BusNumber);

//            if (existingBus != null)
//            {
//                return Json(new { success = false, message = "This bus number is already in use. Please select a different bus number." });
//            }

//            string imagePath = null;
//            if (Image != null && Image.Length > 0)
//            {
//                var fileName = Path.GetFileName(Image.FileName);
//                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadImages", fileName);
//                using (var stream = new FileStream(path, FileMode.Create))
//                {
//                    Image.CopyTo(stream);
//                }
//                imagePath = "/uploadImages/" + fileName;
//            }

//            Buses bus = new Buses
//            {
//                BusName = BusName,
//                BusNumber = BusNumber,
//                PhoneNumber = PhoneNumber,
//                TicketPrice = TicketPrice,
//                Image = imagePath,
//                Timing = Timing,
//                DepartureLocation = DepartureLocation,
//                ArrivalLocation = ArrivalLocation
//            };

//            _busService.Add(bus);
//            return Json(new { success = true, message = "Bus added successfully!" });
//        }



//        //[HttpPost]
//        //[Authorize(Policy = "adminonly")]

//        //public IActionResult AvailableBuses(string BusNumber, string BusName, string PhoneNumber, decimal TicketPrice, IFormFile Image, TimeSpan Timing)
//        //{
//        //    var existingBus = rep.GetAll().FirstOrDefault(b => b.BusNumber == BusNumber);

//        //    if (existingBus != null)
//        //    {
//        //        ViewBag.ErrorMessage = "This seat is already booked. Please select a different seat.";
//        //        return View();
//        //    }

//        //    string imagePath = null;
//        //    if (Image != null && Image.Length > 0)
//        //    {
//        //        var fileName = Path.GetFileName(Image.FileName);
//        //        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadImages", fileName);
//        //        using (var stream = new FileStream(path, FileMode.Create))
//        //        {
//        //            Image.CopyTo(stream);
//        //        }
//        //        imagePath = "/UploadImages/" + fileName;
//        //    }

//        //    Buses bus = new Buses
//        //    {
//        //        BusName = BusName,
//        //        BusNumber = BusNumber,
//        //        PhoneNumber = PhoneNumber,
//        //        TicketPrice = TicketPrice,
//        //        Image = imagePath,
//        //        Timing = Timing
//        //    };

//        //    rep.Add(bus);
//        //    return RedirectToAction("ShowInfo", "AddBuses");
//        //}




//        [HttpPost]


//        [Authorize(Policy = "BookingPolicy")]
//        [Authorize(Policy = "PakistaniPolicy")]
//        [Authorize(Policy = "adminonly")]

//        public IActionResult EditBuses(string BusNumber)
//        {

//            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                SqlCommand cmd = new SqlCommand("DELETE FROM Buses WHERE BusNumber = @BusNumber", conn);
//                cmd.Parameters.AddWithValue("@BusNumber", BusNumber);
//                int rowsAffected = cmd.ExecuteNonQuery();

//                if (rowsAffected == 0)
//                {
//                    return NotFound();
//                }
//            }



//            return RedirectToAction("AvailableBuses", "AddBuses");
//        }

//        [HttpGet]
//        public IActionResult Search()
//        {
//            return View();
//        }
//        [HttpPost]
//        public IActionResult SearchBuses(string departureLocation, string arrivalLocation, string time)
//        {
//            // Convert the time string to a TimeSpan
//            TimeSpan searchTime;
//            if (!TimeSpan.TryParse(time, out searchTime))
//            {
//                // Handle the case where the time string is not a valid TimeSpan
//                return BadRequest("Invalid time format.");
//            }

//            var buses = _busService.GetAll()
//                .Where(bus =>
//                    bus.DepartureLocation == departureLocation &&
//                    bus.ArrivalLocation == arrivalLocation &&
//                    bus.Timing == searchTime)
//                .ToList();

//            return PartialView("_BusesPartial", buses);
//        }




//        [HttpPost]
//        [Authorize(Policy = "BookingPolicy")]
//        [Authorize(Policy = "PakistaniPolicy")]


//        public IActionResult DeleteBus(string busNumber)
//        {
//            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                SqlCommand cmd = new SqlCommand("DELETE FROM Buses WHERE BusNumber = @BusNumber", conn);
//                cmd.Parameters.AddWithValue("@BusNumber", busNumber);
//                int rowsAffected = cmd.ExecuteNonQuery();

//                if (rowsAffected == 0)
//                {
//                    return NotFound();
//                }
//            }

//            return RedirectToAction("ShowInfo1", "AddBuses");
//        }




//        //[HttpPost]
//        //public IActionResult UploadFiles(List<IFormFile> transcript)
//        //{
//        //    try
//        //    {
//        //        string wwwrootPath = _env.WebRootPath;
//        //        string path = Path.Combine(wwwrootPath, "uploadImages");
//        //        if (!Directory.Exists(path))
//        //        {
//        //            Directory.CreateDirectory(path);
//        //        }
//        //        foreach (var file in transcript)
//        //        {
//        //            if (file.Length > 0)
//        //            {
//        //                string filePath = Path.Combine(path, file.FileName);
//        //                using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //                {
//        //                    file.CopyTo(fileStream);
//        //                }
//        //            }
//        //        }

//        //        return RedirectToAction("Index", "Home");
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        _logger.LogError(ex, "Error occurred while uploading files");
//        //        return RedirectToAction("Index", "Home");
//        //    }
//        //}

//        public IActionResult Seats()
//        {
//            return View("Seats");
//        }

//        private Buses GetBusByNumber(string busNumber)
//        {
//            Buses bus = null;
//            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

//            using (SqlConnection conn = new SqlConnection(connectionString))
//            {
//                conn.Open();
//                SqlCommand cmd = new SqlCommand("SELECT * FROM Buses WHERE BusNumber = @BusNumber", conn);
//                cmd.Parameters.AddWithValue("@BusNumber", busNumber);

//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        bus = new Buses
//                        {
//                            BusNumber = reader["BusNumber"].ToString(),
//                            BusName = reader["BusName"].ToString(),
//                            PhoneNumber = reader["PhoneNumber"].ToString(),
//                            TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
//                            Image = reader["Image"].ToString(),
//                            Timing = (TimeSpan)reader["Timing"]

//                        };
//                    }
//                }
//            }
//            return bus;
//        }



//    }
//}



//apply asynchronous





using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Regstration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Regstration.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Application.Services;
using Domain.Entities;

namespace Regstration.Controllers
{
   
    public class AddBusesController : Controller
    {
        private readonly ILogger<AddBusesController> _logger;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        private readonly BusService _busService;

        public AddBusesController(ILogger<AddBusesController> logger, IWebHostEnvironment env, BusService busService, IMemoryCache memoryCache)
        {
            _logger = logger;
            _env = env;
            _busService = busService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowInfo()
        {
            string cacheKey = "allBuses";
            List<Buses> buses;

            if (!_memoryCache.TryGetValue(cacheKey, out buses))
            {
                // Fetch asynchronously
                buses = (await _busService.GetAllAsync()).ToList();

                var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(10));
                _memoryCache.Set(cacheKey, buses, cacheOptions);
            }

            return View(buses);
        }
        [Authorize(Policy = "adminonly")]


        public async Task<IActionResult> showInfo1()
        {
            List<Buses> buses = (await _busService.GetAllAsync()).ToList();
            return View(buses);
        }
        [HttpGet]

        [Authorize(Policy = "adminonly")]
        public IActionResult AvailableBuses()
        {
            return View();
        }



        [HttpPost]
        [Authorize(Policy = "adminonly")]
        public async Task<IActionResult> AvailableBuses(string BusNumber, string BusName, string PhoneNumber, decimal TicketPrice, IFormFile Image, TimeSpan Timing, string DepartureLocation, string ArrivalLocation)
        {
            var existingBus = (await _busService.GetAllAsync()).FirstOrDefault(b => b.BusNumber == BusNumber);

            if (existingBus != null)
            {
                return Json(new { success = false, message = "This bus number is already in use. Please select a different bus number." });
            }

            string imagePath = null;
            if (Image != null && Image.Length > 0)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploadImages", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await Image.CopyToAsync(stream);
                }
                imagePath = "/uploadImages/" + fileName;
            }

            Buses bus = new Buses
            {
                BusName = BusName,
                BusNumber = BusNumber,
                PhoneNumber = PhoneNumber,
                TicketPrice = TicketPrice,
                Image = imagePath,
                Timing = Timing,
                DepartureLocation = DepartureLocation,
                ArrivalLocation = ArrivalLocation
            };

            await _busService.AddAsync(bus);
            return Json(new { success = true, message = "Bus added successfully!" });
        }

        [HttpPost]
        [Authorize(Policy = "BookingPolicy")]
        [Authorize(Policy = "PakistaniPolicy")]
        [Authorize(Policy = "adminonly")]
        public async Task<IActionResult> EditBuses(string BusNumber)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM Buses WHERE BusNumber = @BusNumber", conn);
                cmd.Parameters.AddWithValue("@BusNumber", BusNumber);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }

            return RedirectToAction("AvailableBuses", "AddBuses");
        }

        [HttpPost]
        [Authorize(Policy = "BookingPolicy")]
        [Authorize(Policy = "PakistaniPolicy")]
        public async Task<IActionResult> DeleteBus(string busNumber)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM Buses WHERE BusNumber = @BusNumber", conn);
                cmd.Parameters.AddWithValue("@BusNumber", busNumber);
                int rowsAffected = await cmd.ExecuteNonQueryAsync();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
            }

            return RedirectToAction("ShowInfo1", "AddBuses");
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBuses(string departureLocation, string arrivalLocation, string time)
        {
            // Convert the time string to a TimeSpan
            if (!TimeSpan.TryParse(time, out TimeSpan searchTime))
            {
                // Handle invalid time format
                return BadRequest("Invalid time format.");
            }

            // Fetch and filter buses based on search criteria
            var buses = (await _busService.GetAllAsync())
                .Where(bus =>
                    bus.DepartureLocation == departureLocation &&
                    bus.ArrivalLocation == arrivalLocation &&
                    bus.Timing == searchTime)
                .ToList();

            // Return partial view with the search results
            return PartialView("_BusesPartial", buses);
        }

        private async Task<Buses> GetBusByNumberAsync(string busNumber)
        {
            Buses bus = null;
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Buses WHERE BusNumber = @BusNumber", conn);
                cmd.Parameters.AddWithValue("@BusNumber", busNumber);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        bus = new Buses
                        {
                            BusNumber = reader["BusNumber"].ToString(),
                            BusName = reader["BusName"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            TicketPrice = Convert.ToDecimal(reader["TicketPrice"]),
                            Image = reader["Image"].ToString(),
                            Timing = (TimeSpan)reader["Timing"]
                        };
                    }
                }
            }
            return bus;
        }
    }
}
