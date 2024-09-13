
using System.Collections.Generic;
namespace Domain.Entities
{
    public class Buses
    {
        public int BookingId { get; set; }
        public string BusNumber { get; set; } = "";// Primary Key
        public string BusName { get; set; } = "";

      
        public string PhoneNumber { get; set; } = "";
        public string Image { get; set; } = "";
        public decimal TicketPrice { get; set; }
        public string DepartureLocation { get; set; } = "";
        public string ArrivalLocation { get; set; } = "";
        public string username { get; set; } = " ";


        public TimeSpan Timing
        {
            get; set;
        }

    }
}


