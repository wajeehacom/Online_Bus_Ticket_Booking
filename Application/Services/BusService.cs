//using System;
//using Domain.Entities;
//using Domain.Interface;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class BusService
//    {
//        private readonly IBusRepository _busRepository;
//        private readonly IRepository<Buses> _repository;

//        public BusService(IBusRepository busRepository,IRepository<Buses> repository)
//        {
//            _busRepository = busRepository;
//            _repository = repository;
//        }

//        public void Add(Buses bus)
//        {
//           _repository.Add(bus);
//        }

//        public void UpdateBus(Buses bus)
//        {
//            _repository.Update(bus);
//        }
//        public IEnumerable<Buses> GetAll()
//        {
//            return _repository.GetAll();
//        }
//        public void DeleteBus(int id)
//        {
//            _repository.DeleteById(id);
//        }

//        public Buses GetBusById(int id)
//        {
//            return _repository.FindById(id);
//        }

//        public IEnumerable<Buses> GetAllBuses(string justUsername)
//        {
//            return _repository.GetAll();
//        }

//        public string GetBusesByName(string name)
//        {
//            return _busRepository.GetBusName(name);
//        }


//    }
//}



//apply asnchr


using Domain.Entities;
using Domain.Interface;
namespace Application.Services
{
    public class BusService
    {

        private readonly IBusRepository _busRepository;
        private readonly IRepository<Buses> _repository;

        public BusService(IBusRepository busRepository, IRepository<Buses> repository)
        {
            _busRepository = busRepository;
            _repository = repository;
        }
        public async Task AddAsync(Buses bus)
        {
            await _repository.AddAsync(bus);
        }

        public async Task UpdateAsync(Buses bus)
        {
            await _repository.UpdateAsync(bus);
        }

        public async Task<IEnumerable<Buses>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Buses> GetBusByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<string> GetBusNameAsync(string name)
        {
            return await _busRepository.GetBusNameAsync(name);
        }
        
        public async Task<IEnumerable<Buses>> GetAllBusesAsync(string justUsername)
        {
            // Implementation that retrieves buses based on username
            return await _busRepository.GetAllBusesAsync(justUsername);



        }
      
    }
}
