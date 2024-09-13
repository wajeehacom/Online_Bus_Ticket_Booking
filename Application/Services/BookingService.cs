//using Domain.Entities;
//using Domain.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;


//namespace Application.Services
//{
//    public class BookingService
//    {
//        private readonly  IRepository<Booking> _repository;

//        public BookingService(IRepository<Booking> repository)
//        {
//            _repository= repository;
//        }

//        public void AddBooking(Booking booking)
//        {
//            _repository.Add(booking);
//        }

//        public void UpdateBooking(Booking booking)
//        {
//            _repository.Update( booking);
//        }

//        public void DeleteBooking(int id)
//        {
//            _repository.DeleteById(id);
//        }
//        public IEnumerable<Booking> GetAll(string name)
//        {
//           return  _repository.GetAll(name);

//        }

//        public IEnumerable<Booking> GetAll()
//        {
//            return _repository.GetAll();
//        }



//    }
//}


//apply anch




using Domain.Entities;
using Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookingService
    {
        private readonly IRepository<Booking> _repository;

        public BookingService(IRepository<Booking> repository)
        {
            _repository = repository;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await _repository.AddAsync(booking);
        }

        public async Task UpdateBookingAsync(Booking booking)
        {
            await _repository.UpdateAsync(booking);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<Booking> FindByIdAsync(int bookingId) => await _repository.FindByIdAsync(bookingId);
        public async Task<IEnumerable<Booking>> GetAllAsync(string name)
        {
            return await _repository.GetAllAsync(name);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}

