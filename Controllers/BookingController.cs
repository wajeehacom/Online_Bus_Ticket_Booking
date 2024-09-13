

////depenedency injection


//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using NuGet.Protocol.Plugins;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using Microsoft.CodeAnalysis;
//using System.Security.Claims;
//using Microsoft.EntityFrameworkCore;
//using Regstration.Data;
//using System.Diagnostics;
//using Microsoft.Data.SqlClient;
//using Newtonsoft.Json;
//using Infrastructure1.Repositories;
//using Application.Services;
//using Domain.Entities;
//using Microsoft.Extensions.Caching.Memory;

//namespace Regstration.Controllers
//{
//    public class BookingController : Controller
//    {
//        //private readonly IRepository<Booking> _rep;
//        //private readonly IRepository<BusSeats> _rep1;
//        //private readonly IRepository<Buses> _busRepository;
//        //private readonly BusRepository _busRepo;

//        private BookingService _bookingService;
//        private SeatsService _seatsService;
//        private BusService _busService;
//        Booking booking = new Booking();
//        private readonly IMemoryCache _memoryCache;

//        public BookingController(BookingService bookingService, SeatsService seatsService, BusService busService, IMemoryCache memoryCache)
//        {
//            _bookingService = bookingService;
//            _seatsService = seatsService;
//            _busService = busService;
//            _memoryCache = memoryCache;

//        }



//        [HttpGet]
//        [Authorize(Policy = "PakistaniPolicy")]
//        [Authorize(Policy = "BookingPolicy")]
//        public JsonResult CheckAllBooking()
//        {
//            List<Booking> lists = (List<Booking>)_bookingService.GetAll();
//            return Json(lists);
//        }

//        [HttpGet]
//        public ViewResult Check()
//        {
//            string cacheKey = "allBookings";
//            List<Booking> lists;

//            // Check if the cache already contains the data
//            if (!_memoryCache.TryGetValue(cacheKey, out lists))
//            {
//                // If not in cache, fetch the data from the service
//                lists = (List<Booking>)_bookingService.GetAll();

//                // Set cache options
//                var cacheOptions = new MemoryCacheEntryOptions()
//                    .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Cache expires after 30 minutes of inactivity

//                // Cache the data
//                _memoryCache.Set(cacheKey, lists, cacheOptions);
//            }

//            return View(lists);

//        }


//        /* for json andbefore inmemmoery cache

//        //[HttpGet]

//        //[Authorize(Policy = "PakistaniPolicy")]
//        //[Authorize(Policy = "BookingPolicy")]
//        //public ViewResult Check()
//        //{
//        //    List<Booking> lists = (List<Booking>)_rep.GetAll();
//        //    return View(lists);


//        //}

//        */
//        [Authorize(Policy = "PakistaniPolicy")]
//        [Authorize(Policy = "BookingPolicy")]
//        [HttpGet]
//        public ViewResult Booking()
//        {
//            return View();
//        }

//        [HttpPost]
//        [Authorize(Policy = "PakistaniPolicy")]
//        [Authorize(Policy = "BookingPolicy")]
//        public ViewResult ConfirmBooking(Booking bookings)
//        {

//            if (!ModelState.IsValid)
//            {
//                return View(bookings);
//            }

//            string justUsername = string.Empty;
//            string username = HttpContext.User.Identity.Name;
//            int atIndex = username.IndexOf('@');
//            if (atIndex != -1)
//            {
//                justUsername = username.Substring(0, atIndex);
//            }

//            var allBusNumbers = _busService.GetAll().Select(s => s.BusNumber);
//            string busNumbersAsString = string.Join(",", allBusNumbers);

//            string b = _busService.GetBusesByName(justUsername);
//            Buses buses = (Buses)_busService.GetAllBuses(busNumbersAsString);

//            List<Booking> booking = (List<Booking>)_bookingService.GetAll(justUsername);
//            var seats = _seatsService.GetAll().ToList();
//            var selectedSeats = _seatsService.GetAll().Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

//            BookingBuses bookingBuses = new BookingBuses
//            {
//                buses = buses,
//                booking = booking,
//                SelectedSeatNumbers = selectedSeats
//            };

//            string bookingInfo = HttpContext.Session.GetString("BookingInfo");
//            if (!string.IsNullOrEmpty(bookingInfo))
//            {
//                bookingBuses.booking = new List<Booking> { System.Text.Json.JsonSerializer.Deserialize<Booking>(bookingInfo) };
//            }

//            return View(bookingBuses);
//        }


//        // for assnchronous

//        [HttpPost]
//        [Authorize(Policy = "BookingPolicy")]
//        [Authorize(Policy = "PakistaniPolicy")]
//        public IActionResult Booking(int id, string F, string T, string D, string B, string N, string Ph, string Pas)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(booking);
//            }



//            string userId = GetUserId();
//            booking.Source = F;
//            booking.Destination = T;
//            booking.DepartureDate = D;
//            booking.BusType = B;
//            booking.Name = N;
//            booking.PhoneNumber = Ph;
//            booking.Passenger = Pas;

//            //busName = B;
//            HttpContext.Session.SetString("BookingInfo", System.Text.Json.JsonSerializer.Serialize(booking));

//            _bookingService.AddBooking(booking);


//            return RedirectToAction("showInfo", "AddBuses");
//        }







//        [HttpPost]
//        public IActionResult Remove(string bookingId)
//        {
//            string userId = GetUserId();

//            List<Booking> items = GetBookingFromCookie(HttpContext.Request, userId);

//            if (items.Count == 0)
//            {
//                HttpContext.Response.Cookies.Delete($"CartItems_{userId}");
//            }
//            else
//            {
//                SaveBookingToCookie(items, HttpContext, userId);
//            }

//            return RedirectToAction("Index", "Cart");
//        }

//        public IActionResult FaisalMovers()
//        {
//            return View("FaisalMovers");
//        }

//        public IActionResult SkyWays()
//        {
//            return View("SkyWays");
//        }

//        public IActionResult NiaziExpress()
//        {
//            return View("NiaziExpress");
//        }

//        public IActionResult Daewoo()
//        {
//            return View("Daewoo");
//        }

//        public IActionResult UpdateBooking(int id)
//        {
//            Debug.WriteLine($"Debug: UpdateBooking action called with id: {id}");
//            Trace.WriteLine($"Trace: UpdateBooking action called with id: {id}");
//            _bookingService.DeleteBooking(id);
//            return RedirectToAction("Booking");
//        }

//        public IActionResult DeleteBooking(int id)
//        {
//            Debug.WriteLine($"Debug: DeleteBooking action called with id: {id}");
//            Trace.WriteLine($"Trace: DeleteBooking action called with id: {id}");
//            _bookingService.DeleteBooking(id);
//            return RedirectToAction("SelectedBus", "Booking");
//        }

//        private string GetUserId()
//        {
//            return User.FindFirstValue(ClaimTypes.NameIdentifier);
//        }
//        public IActionResult Confirm()
//        {
//            Debug.WriteLine("Debug: Confirm action called");
//            Trace.WriteLine("Trace: Confirm action called");

//            string justUsername = string.Empty;
//            string username = HttpContext.User.Identity.Name;
//            int atIndex = username.IndexOf('@');
//            if (atIndex != -1)
//            {
//                justUsername = username.Substring(0, atIndex);
//            }

//            List<Buses> busesList = _busService.GetAllBuses(justUsername).ToList();
//            Buses buses = busesList.FirstOrDefault(); // Get the first bus or handle as needed

//            List<Booking> booking = (List<Booking>)_bookingService.GetAll(justUsername);
//            var seats = _seatsService.GetAll().ToList();
//            var selectedSeats = _seatsService.GetAll().Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

//            BookingBuses bookingBuses = new BookingBuses
//            {
//                buses = buses, // Use the first bus or handle the list if needed
//                booking = booking,
//                SelectedSeatNumbers = selectedSeats
//            };

//            return View(bookingBuses);
//        }


//        public IActionResult SelectedBus()
//        {
//            string justUsername = string.Empty;
//            string username = HttpContext.User.Identity.Name;
//            int atIndex = username.IndexOf('@');
//            if (atIndex != -1)
//            {
//                justUsername = username.Substring(0, atIndex);
//            }

//            // Handle multiple buses
//            List<Buses> busesList = _busService.GetAllBuses(justUsername).ToList();

//            List<Booking> booking = _bookingService.GetAll(justUsername).ToList();
//            var seats = _seatsService.GetAll().ToList();
//            var selectedSeats = _seatsService.GetAll().Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

//            BookingBuses bookingBuses = new BookingBuses
//            {
//                buses = busesList.FirstOrDefault(), // Or assign the list if your BookingBuses expects multiple buses
//                booking = booking,
//                SelectedSeatNumbers = selectedSeats
//            };

//            return View(bookingBuses);
//        }




//        private List<Booking> GetBookingFromCookie(HttpRequest request, string userId)
//        {
//            if (request.Cookies.TryGetValue($"bookingDetail {userId}", out string bookingItemsJson))
//            {
//                return JsonConvert.DeserializeObject<List<Booking>>(bookingItemsJson);
//            }
//            return new List<Booking>();
//        }



//        private void SaveBookingToCookie(List<Booking> booking, HttpContext context, string userId)
//        {
//            var options = new CookieOptions
//            {
//                Expires = DateTime.Now.AddDays(1)
//            };

//            var bookingJson = System.Text.Json.JsonSerializer.Serialize(booking);
//            var cookieName = $"bookingDetail_{userId}"; // Use underscores instead of spaces
//            context.Response.Cookies.Append(cookieName, bookingJson, options);
//        }

//    }
//}



// apply asnchronous





using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Regstration.Data;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Infrastructure1.Repositories;
using Application.Services;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

public class BookingController : Controller
{
    private readonly BookingService _bookingService;
    private readonly SeatsService _seatsService;
    private readonly BusService _busService;
    private readonly IMemoryCache _memoryCache;
     Booking booking = new Booking();

    public BookingController(BookingService bookingService, SeatsService seatsService, BusService busService, IMemoryCache memoryCache)
    {
        _bookingService = bookingService;
        _seatsService = seatsService;
        _busService = busService;
        _memoryCache = memoryCache;
    }

    [HttpGet]
    [Authorize(Policy = "PakistaniPolicy")]
    [Authorize(Policy = "BookingPolicy")]
    public async Task<JsonResult> CheckAllBooking()
    {
        var lists = await _bookingService.GetAllAsync();
        return Json(lists);
    }

    [HttpGet]
    public async Task<ViewResult> Check()
    {
        string cacheKey = "allBookings";
        List<Booking> lists;

        // Check if the cache already contains the data
        if (!_memoryCache.TryGetValue(cacheKey, out lists))
        {
            // If not in cache, fetch the data from the service
            lists = (List<Booking>)await _bookingService.GetAllAsync();
            // Set cache options
            var cacheOptions = new MemoryCacheEntryOptions()//MemoryCacheEntryOptions provides a way to configure how cache entries behave in terms of expiration, eviction, and priority.

                .SetSlidingExpiration(TimeSpan.FromMinutes(30)); // Cache expires after 30 minutes of inactivitystale data is not kept in the cache.

            // Cache the data
            _memoryCache.Set(cacheKey, lists, cacheOptions);
        }

        return View(lists);
    }

    [Authorize(Policy = "PakistaniPolicy")]
    [Authorize(Policy = "BookingPolicy")]
    [HttpGet]
    public ViewResult Booking()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = "PakistaniPolicy")]
    [Authorize(Policy = "BookingPolicy")]
    public async Task<IActionResult> ConfirmBooking(Booking bookings)
    {
        if (!ModelState.IsValid)
        {
            return View(bookings);
        }

        string justUsername = string.Empty;
        string username = HttpContext.User.Identity.Name;
        int atIndex = username.IndexOf('@');
        if (atIndex != -1)
        {
            justUsername = username.Substring(0, atIndex);
        }

        var allBusNumbers = (await _busService.GetAllAsync()).Select(s => s.BusNumber);
        string busNumbersAsString = string.Join(",", allBusNumbers);


        //            string b = _busService.GetBusesByName(justUsername);
        //            Buses buses = (Buses)_busService.GetAllBuses(busNumbersAsString);




        var b = await _busService.GetBusNameAsync(justUsername);
        Buses buses = (Buses)await _busService.GetAllBusesAsync(busNumbersAsString);




        List<Booking> booking = (await _bookingService.GetAllAsync(justUsername)).ToList();
        var seats = (await _seatsService.GetAllAsync()).ToList();
        var selectedSeats = (await _seatsService.GetAllAsync()).Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

        BookingBuses bookingBuses = new BookingBuses
        {
            buses = buses,
            booking = booking,
            SelectedSeatNumbers = selectedSeats
        };

        string bookingInfo = HttpContext.Session.GetString("BookingInfo");
        if (!string.IsNullOrEmpty(bookingInfo))
        {
            bookingBuses.booking = new List<Booking> { System.Text.Json.JsonSerializer.Deserialize<Booking>(bookingInfo) };
        }

        return View(bookingBuses);
    }

    [HttpPost]
    [Authorize(Policy = "BookingPolicy")]
    [Authorize(Policy = "PakistaniPolicy")]
    public async Task<IActionResult> Booking(int id, string F, string T, string D, string B, string N, string Ph, string Pas)
    {
        if (!ModelState.IsValid)
        {
            return View(booking);
        }

        string userId = GetUserId();
        booking.Source = F;
        booking.Destination = T;
        booking.DepartureDate = D;
        booking.BusType = B;
        booking.Name = N;
        booking.PhoneNumber = Ph;
        booking.Passenger = Pas;

        HttpContext.Session.SetString("BookingInfo", System.Text.Json.JsonSerializer.Serialize(booking));

        await _bookingService.AddBookingAsync(booking);

        return RedirectToAction("showInfo", "AddBuses");
    }

    [HttpPost]
    public async Task<IActionResult> Remove(string bookingId)
    {
        string userId = GetUserId();

        List<Booking> items = GetBookingFromCookie(HttpContext.Request, userId);

        if (items.Count == 0)
        {
            HttpContext.Response.Cookies.Delete($"CartItems_{userId}");
        }
        else
        {
            SaveBookingToCookie(items, HttpContext, userId);
        }

        return RedirectToAction("Index", "Cart");
    }

    public IActionResult FaisalMovers()
    {
        return View("FaisalMovers");
    }

    public IActionResult SkyWays()
    {
        return View("SkyWays");
    }

    public IActionResult NiaziExpress()
    {
        return View("NiaziExpress");
    }

    public IActionResult Daewoo()
    {
        return View("Daewoo");
    }

    public async Task<IActionResult> UpdateBooking(int id)
    {
        Debug.WriteLine($"Debug: UpdateBooking action called with id: {id}");
        Trace.WriteLine($"Trace: UpdateBooking action called with id: {id}");
        await _bookingService.DeleteBookingAsync(id);
        return RedirectToAction("Booking");
    }

    public async Task<IActionResult> DeleteBooking(int id)
    {
        Debug.WriteLine($"Debug: DeleteBooking action called with id: {id}");
        Trace.WriteLine($"Trace: DeleteBooking action called with id: {id}");
        await _bookingService.DeleteBookingAsync(id);
        return RedirectToAction("SelectedBus", "Booking");
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public async Task<IActionResult> Confirm()
    {
        Debug.WriteLine("Debug: Confirm action called");
        Trace.WriteLine("Trace: Confirm action called");

        string justUsername = string.Empty;
        string username = HttpContext.User.Identity.Name;
        int atIndex = username.IndexOf('@');
        if (atIndex != -1)
        {
            justUsername = username.Substring(0, atIndex);
        }

        List<Buses> busesList = (await _busService.GetAllBusesAsync(justUsername)).ToList();
        Buses buses = busesList.FirstOrDefault(); // Get the first bus or handle as needed

        List<Booking> booking = (await _bookingService.GetAllAsync(justUsername)).ToList();
        var seats = (await _seatsService.GetAllAsync()).ToList();
        var selectedSeats = (await _seatsService.GetAllAsync()).Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

        BookingBuses bookingBuses = new BookingBuses
        {
            buses = buses, // Use the first bus or handle the list if needed
            booking = booking,
            SelectedSeatNumbers = selectedSeats
        };

        return View(bookingBuses);
    }


    public async Task<IActionResult> SelectedBus()
    {
        string justUsername = string.Empty;
        string username = HttpContext.User.Identity.Name;
        int atIndex = username.IndexOf('@');
        if (atIndex != -1)
        {
            justUsername = username.Substring(0, atIndex);
        }

        // Handle multiple buses
        List<Buses> busesList = (await _busService.GetAllBusesAsync(justUsername)).ToList();

        List<Booking> booking = (await _bookingService.GetAllAsync(justUsername)).ToList();
        var seats = (await _seatsService.GetAllAsync()).ToList();
        var selectedSeats = (await _seatsService.GetAllAsync()).Where(s => s.Username == username).Select(s => s.SeatNumber).ToList();

        BookingBuses bookingBuses = new BookingBuses
        {
            buses = busesList.FirstOrDefault(), // Or assign the list if your BookingBuses expects multiple buses
            booking = booking,
            SelectedSeatNumbers = selectedSeats
        };

        return View(bookingBuses);
    }

    private List<Booking> GetBookingFromCookie(HttpRequest request, string userId)
    {
        if (request.Cookies.TryGetValue($"bookingDetail_{userId}", out string bookingItemsJson))
        {
            return JsonConvert.DeserializeObject<List<Booking>>(bookingItemsJson);
        }
        return new List<Booking>();
    }

    private void SaveBookingToCookie(List<Booking> booking, HttpContext context, string userId)
    {
        var options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };

        var bookingJson = System.Text.Json.JsonSerializer.Serialize(booking);
        var cookieName = $"bookingDetail_{userId}";
        context.Response.Cookies.Append(cookieName, bookingJson, options);
    }
}

