
//// list iof buses
//using Application.Services;
//using Domain.Entities;
//using Domain.Interface;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.Data.SqlClient;
//using Microsoft.EntityFrameworkCore;
//using NuGet.Packaging;
//using Regstration.Hubs;
//using Regstration.Models;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Security.Claims;

//namespace Regstration.Controllers
//{
//    public class ListOfBusesController : Controller
//    {


//        //private readonly IRepository<Buses> _busRepository;
//        //private readonly IRepository<Booking> _bookingRepository;
//        //private readonly IRepository<BusSeats> _busSeatRepository;
//        //private readonly SeatsRepository _mySeatsRepository;




//        private BookingService _bookingService;
//        private SeatsService _seatsService;
//        private BusService _busService;
//        private readonly IHubContext<SeatHub> _hubContext;
//        public ListOfBusesController(IHubContext<SeatHub> hubContext, BookingService bookingService,SeatsService seatsService,BusService busService)
//        {
//            _bookingService = bookingService;
//            _seatsService = seatsService;
//            _busService = busService;
//            _hubContext = hubContext;

//        }

//        public IActionResult Index()
//        {
//            return View();
//        }


//        public IActionResult SeatNumbers()
//        {
//            var username = HttpContext.User.Identity.Name;

//            var allSeats = Enumerable.Range(1, 50).ToArray(); // Assuming you have 50 seats

//            var seatAssignments = new bool[allSeats.Length];

//            // Retrieve seat assignments for the current user
//            var userSeats = _seatsService.GetAll().Where(s => s.Username == username).ToList();
//            foreach (var seat in userSeats)
//            {
//                seatAssignments[seat.SeatNumber - 1] = true; // Mark the seat as booked
//            }

//            var model = new SeatViewModel
//            {
//                SeatNumbers = allSeats,
//                SeatAssignments = seatAssignments,
//                SelectedSeatCount = seatAssignments.Count(sa => sa)
//            };

//            return View(model);
//        }
//        [HttpPost]
//        public async Task<IActionResult> BookSeat(int seatNumber)
//        {
//            string username = HttpContext.User.Identity.Name;

//            // Check if the seat is already assigned to another user
//            var seat = _seatsService.GetAll().FirstOrDefault(s => s.SeatNumber == seatNumber);

//            if (seat != null && !seat.IsAssigned)
//            {
//                // Assign the seat
//                seat.IsAssigned = true;
//                seat.Username = username;
//                _seatsService.UpdateSeats(seat);

//                // Notify all clients about the seat booking
//                await _hubContext.Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);
//            }
//            else if (seat == null)
//            {
//                // Create a new seat assignment
//                seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true, Username = username };
//                _seatsService.AddSeats(seat);

//                // Notify all clients about the seat booking
//                await _hubContext.Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);
//            }
//            else
//            {
//                // Optionally, handle the case where the seat is already assigned
//                // For example, display an error message or log this event
//            }

//            return RedirectToAction("SeatNumbers");
//        }

//        //[HttpPost]
//        //public async Task<IActionResult> BookSeat(int seatNumber)
//        //{
//        //    string username = HttpContext.User.Identity.Name;

//        //    var seat = _seatsService.GetAll().FirstOrDefault(s => s.SeatNumber == seatNumber);

//        //    if (seat != null && !seat.IsAssigned)
//        //    {
//        //        seat.IsAssigned = true;
//        //        seat.Username = username;
//        //        _seatsService.UpdateSeats(seat);
//        //    }
//        //    else if (seat == null)
//        //    {
//        //        seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true, Username = username };
//        //        _seatsService.AddSeats(seat);
//        //    }

//        //    // Notify clients via SignalR
//        //    await _hubContext.Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);

//        //    return RedirectToAction("SeatNumbers");
//        //}

//    //[HttpPost]
//    //public IActionResult BookSeat(int seatNumber)
//    //{
//    //    string username = HttpContext.User.Identity.Name; // Get the username of the logged-in user

//    //    var seat = _seatsService.GetAll().FirstOrDefault(s => s.SeatNumber == seatNumber);

//    //    if (seat != null && !seat.IsAssigned)
//    //    {
//    //        seat.IsAssigned = true;
//    //        seat.Username = username; // Save the username
//    //        _seatsService.UpdateSeats(seat);
//    //    }
//    //    else if (seat == null)
//    //    {
//    //        seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true, Username = username }; // Save the username
//    //        _seatsService.AddSeats(seat);
//    //    }

//    //    return RedirectToAction("SeatNumbers");
//    //}

//    [HttpPost]
//        public IActionResult UpdateSeats()
//        {
//            var username = HttpContext.User.Identity.Name;

//            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;";


//            string query = "DELETE FROM BusSeats WHERE Username = @Username";

//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                using (SqlCommand command = new SqlCommand(query, connection))
//                {

//                    command.Parameters.AddWithValue("@Username", username);

//                    try
//                    {

//                        connection.Open();

//                        int rowsAffected = command.ExecuteNonQuery();


//                        return RedirectToAction("SeatNumbers");
//                    }
//                    catch (Exception ex)
//                    {

//                        return RedirectToAction("Error", "Home");
//                    }
//                }




//                //[HttpPost]
//                //public IActionResult BookSeat(int seatNumber)
//                //{
//                //    var seat = busSeatRepository.GetAll().FirstOrDefault(s => s.SeatNumber == seatNumber);


//                //    if (seat != null && !seat.IsAssigned)
//                //    {
//                //        seat.IsAssigned = true;
//                //        busSeatRepository.Update(seat);
//                //    }
//                //    else if (seat == null)
//                //    {
//                //        seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true };
//                //        busSeatRepository.AddSeatNumber(seat);
//                //    }


//                //    return RedirectToAction("SeatNumbers");
//                //}

//            }
//        }
//    }
//}

//public class SeatViewModel
//{
//    public int[] SeatNumbers { get; set; }
//    public bool[] SeatAssignments { get; set; }
//    public int SelectedSeatCount { get; set; }
//    public List<int> SelectedSeats { get; set; } = new List<int>();
//}



//using Application.Services;
//using Domain.Entities;
//using Domain.Interface;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Regstration.Hubs;
//using Regstration.Models;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Regstration.Controllers
//{
//    public class ListOfBusesController : Controller
//    {
//        private readonly BookingService _bookingService;
//        private readonly SeatsService _seatsService;
//        private readonly BusService _busService;
//        private readonly IHubContext<SeatHub> _hubContext;

//        public ListOfBusesController(IHubContext<SeatHub> hubContext, BookingService bookingService, SeatsService seatsService, BusService busService)
//        {
//            _bookingService = bookingService;
//            _seatsService = seatsService;
//            _busService = busService;
//            _hubContext = hubContext;
//        }

//        public IActionResult SeatNumbers()
//        {
//            var username = HttpContext.User.Identity.Name;
//            var allSeats = Enumerable.Range(1, 50).ToArray(); // Assuming 50 seats
//            var seatAssignments = new bool[allSeats.Length];

//            // Retrieve seat assignments for the current user
//            var userSeats = _seatsService.GetAll().Where(s => s.Username == username).ToList();
//            foreach (var seat in userSeats)
//            {
//                seatAssignments[seat.SeatNumber - 1] = true; // Mark the seat as booked
//            }

//            var model = new SeatViewModel
//            {
//                SeatNumbers = allSeats,
//                SeatAssignments = seatAssignments,
//                SelectedSeatCount = seatAssignments.Count(sa => sa)
//            };

//            return View(model);
//        }


//        [HttpPost]
//        public async Task<IActionResult> BookSeat(int seatNumber)
//        {
//            string username = HttpContext.User.Identity.Name;

//            var seat = _seatsService.GetAll().FirstOrDefault(s => s.SeatNumber == seatNumber);

//            if (seat != null && seat.IsAssigned)
//            {
//                // Seat is already assigned
//                return Content("Seat already booked by another user.", "text/plain");
//            }
//            else
//            {
//                if (seat == null)
//                {
//                    seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true, Username = username };
//                    _seatsService.AddSeats(seat);
//                }
//                else
//                {
//                    seat.IsAssigned = true;
//                    seat.Username = username;
//                    _seatsService.UpdateSeats(seat);
//                }

//                await _hubContext.Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);


//                return RedirectToAction("SeatNumbers");
//            }

//            //    return RedirectToAction("SeatNumbers");
//        }





//        [HttpPost]
//        public IActionResult UpdateSeats()
//        {
//            var username = HttpContext.User.Identity.Name;
//            _seatsService.RemoveSeatsByUsername(username); // Custom method to remove seats by username

//            return RedirectToAction("SeatNumbers");
//        }
//    }
//}

//public class SeatViewModel
//{
//    public int[] SeatNumbers { get; set; }
//    public bool[] SeatAssignments { get; set; }
//    public int SelectedSeatCount { get; set; }
//    public List<int> SelectedSeats { get; set; } = new List<int>();
//}






// apply asnch



using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Regstration.Hubs;
using Regstration.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Regstration.Controllers
{
    public class ListOfBusesController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly SeatsService _seatsService;
        private readonly BusService _busService;
        private readonly IHubContext<SeatHub> _hubContext;

        public ListOfBusesController(IHubContext<SeatHub> hubContext, BookingService bookingService, SeatsService seatsService, BusService busService)
        {
            _bookingService = bookingService;
            _seatsService = seatsService;
            _busService = busService;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> SeatNumbers()
        {
            var username = HttpContext.User.Identity.Name;
            var allSeats = Enumerable.Range(1, 50).ToArray(); // Assuming 50 seats
            var seatAssignments = new bool[allSeats.Length];

            // Retrieve seat assignments for the current user
            var userSeats = (await _seatsService.GetAllAsync()).Where(s => s.Username == username).ToList();
            foreach (var seat in userSeats)
            {
                seatAssignments[seat.SeatNumber - 1] = true; // Mark the seat as booked
            }

            var model = new SeatViewModel
            {
                SeatNumbers = allSeats,
                SeatAssignments = seatAssignments,
                SelectedSeatCount = seatAssignments.Count(sa => sa)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> BookSeat(int seatNumber)
        {
            string username = HttpContext.User.Identity.Name;

            var seat = (await _seatsService.GetAllAsync()).FirstOrDefault(s => s.SeatNumber == seatNumber);

            if (seat != null && seat.IsAssigned)
            {
                // Seat is already assigned
                return Content("Seat already booked by another user.", "text/plain");
            }
            else
            {
                if (seat == null)
                {
                    seat = new BusSeats { SeatNumber = seatNumber, IsAssigned = true, Username = username };
                    await _seatsService.AddSeatsAsync(seat);
                }
                else
                {
                    seat.IsAssigned = true;
                    seat.Username = username;
                    await _seatsService.UpdateSeatsAsync(seat);
                }

                await _hubContext.Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);

                return RedirectToAction("SeatNumbers");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSeats()
        {
            var username = HttpContext.User.Identity.Name;
            await _seatsService.RemoveSeatsByUsernameAsync(username); // Custom method to remove seats by username

            return RedirectToAction("SeatNumbers");
        }
    }
}
public class SeatViewModel
{
    public int[] SeatNumbers { get; set; }
    public bool[] SeatAssignments { get; set; }
    public int SelectedSeatCount { get; set; }
    public List<int> SelectedSeats { get; set; } = new List<int>();
}


