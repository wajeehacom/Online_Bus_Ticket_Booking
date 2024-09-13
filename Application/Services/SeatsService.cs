
//using Domain.Entities;
//using Domain.Interface;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class SeatsService
//    {
//        private readonly ISeatsRepository _seatsRepository;
//        private readonly IRepository<BusSeats> _repository;

//        public SeatsService(ISeatsRepository seatsRepository,IRepository<BusSeats> repository)
//        {

//            _seatsRepository=seatsRepository;
//            _repository=repository;
//        }

//        public void AddSeats(BusSeats busSeats)
//        {
//            _seatsRepository.AddSeatNumber(busSeats);
//        }
//        public IEnumerable<BusSeats> GetAll()
//        {
//            return _repository.GetAll();
//        }
//        public void UpdateSeats(BusSeats busSeats)
//        {
//            _seatsRepository.UpdateSeats(busSeats);
//        }

//        public void RemoveSeatsByUsername(string username)
//        {
//            _seatsRepository.RemoveSeatsByUsername(username);
//        }




//    }
//}



// applya snc


using Domain.Entities;
using Domain.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeatsService
    {
        private readonly ISeatsRepository _seatsRepository;
        private readonly IRepository<BusSeats> _repository;

        public SeatsService(ISeatsRepository seatsRepository, IRepository<BusSeats> repository)
        {
            _seatsRepository = seatsRepository;
            _repository = repository;
        }

        public async Task AddSeatsAsync(BusSeats busSeats)
        {
            await _seatsRepository.AddSeatNumberAsync(busSeats);
        }

        public async Task<IEnumerable<BusSeats>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateSeatsAsync(BusSeats busSeats)
        {
            await _seatsRepository.UpdateSeatsAsync(busSeats);
        }

        public async Task RemoveSeatsByUsernameAsync(string username)
        {
            await _seatsRepository.RemoveSeatsByUsernameAsync(username);
        }
    }
}
