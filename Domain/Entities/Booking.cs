

using System.ComponentModel.DataAnnotations;
using System;

namespace Domain.Entities
{
    public class Booking
    {

       

        [Required(ErrorMessage = "Source is required")]
        public string Source { get; set; } = "";

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; } = "";

        [Required(ErrorMessage = "Departure Date is required")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
        public string DepartureDate { get; set; } = "";

        [Required(ErrorMessage = "Select Bus type Fakhar Transport")]
        public string BusType { get; set; } = "";

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Phone number is required")]
        
        public string PhoneNumber { get; set; } = "";

        [Required(ErrorMessage = "Select your gender")]
        public string Passenger { get; set; } = "";
       public int BookingId { get; set; }
    }


}

