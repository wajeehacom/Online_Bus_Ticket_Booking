
using Microsoft.AspNetCore.SignalR;

namespace Regstration.Hubs
{
    public class SeatHub : Hub
    {
        public async Task SeatBooked(int seatNumber)
        {
            // Notify all clients about the booked seat
            await Clients.All.SendAsync("ReceiveSeatBooking", seatNumber);
        }
    }
}
