using Domain;

namespace Domain.Entities   
{

    public class BusSeats
    {
        public int SeatId { get; set; }
        
        public int SeatNumber { get; set; }
        public bool IsAssigned { get; set; }


        public string Username { get; set; }
      
    }
}
