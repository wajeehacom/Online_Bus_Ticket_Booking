using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IBusRepository
    {
        Task<string> GetBusNameAsync(string name);

      
        Task<IEnumerable<Buses>> GetAllBusesAsync(string justUsername);

    }
}
