//namespace Regstration.Models.ViewModels
//{
//    public class BookingBuses
//    {
//        public List<Booking> Booking { get; set; }
//        public List<Buses> Buses { get; set; }


//    }
//}

namespace Domain.Entities
{
    public class BookingBuses
    {

        public Buses buses { get; set; }
        public List<Booking> booking { get; set; }
        public List<int> SelectedSeatNumbers { get; set; }
    }
}

