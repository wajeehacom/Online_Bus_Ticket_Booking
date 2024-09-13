

//using System.Collections.Generic;

//namespace Domain.Interface
//{
//    public interface IRepository<TEntity>
//    {
//        TEntity FindById(int bookingId);

//        void DeleteById(int BookId);
//        IEnumerable<TEntity> GetAll();


//        void Add(TEntity entity);
//        public IEnumerable<TEntity> GetAll(string name);

//        public void Update(TEntity entity);


//    }
//}


//apply anchr


using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> FindByIdAsync(int bookingId);

        Task DeleteByIdAsync(int bookingId);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<Booking>> GetAllAsync(string name);
   

        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);
    }
}
