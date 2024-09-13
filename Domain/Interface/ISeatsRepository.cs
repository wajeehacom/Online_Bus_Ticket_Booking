using Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Domain.Interface
//{
//    public interface ISeatsRepository
//    {
//        public void AddSeatNumber(BusSeats entity);
//        public void UpdateSeats(BusSeats entity);
//        public void RemoveSeatsByUsername(string username);
//    }
//}

//applya snch


using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface ISeatsRepository
    {
        Task AddSeatNumberAsync(BusSeats entity);
        Task UpdateSeatsAsync(BusSeats entity);
        Task RemoveSeatsByUsernameAsync(string username);
    }
}
