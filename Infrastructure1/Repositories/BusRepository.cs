
//using Dapper;
//using Domain.Entities;
//using Domain.Interface;
//using Microsoft.Data.SqlClient;

//using static Dapper.SqlMapper;

//namespace Infrastructure1.Repositories
//{
//    public class BusRepository : IBusRepository
//    {
//        private readonly IRepository<Buses> _repository;
//        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ProjectDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

//        public BusRepository(IRepository<Buses> repository, string connectionString)
//        {
//            _repository = repository;

//            this.connectionString = connectionString;


//        }

//        public BusRepository()
//        {
//        }

//        public void Add(Buses entity)
//        {
//            _repository.Add(entity);
//        }


//        public IEnumerable<Buses> GetAll()
//        {
//            return _repository.GetAll();
//        }
//        public Buses FindById(int code)
//        {
//            return _repository.FindById(code);
//        }
//        public IEnumerable<Buses> GetAll(string name)
//        {
//            return _repository.GetAll(name);

//        }


//        public void Update(Buses entity)
//        {
//            _repository.Update(entity);
//        }
//        public void DeleteById(int id)
//        {
//            _repository.DeleteById(id);
//        }


//        public string GetBusName(string name)
//        {
//            var tableName = typeof(Buses).Name;
//            string busName = null;

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                return connection.QueryFirstOrDefault<string>($"SELECT Name FROM {tableName} WHERE Name = @name", new { name });

//            }

//        }

//        public Buses GetAllBuses(string name)
//        {
//            var tableName = typeof(Buses).Name;
//            Buses entity = default(Buses);

//            using (var connection = new SqlConnection(connectionString))
//            {


//                connection.Open();


//                return (Buses)connection.QueryFirstOrDefault<Buses>($"SELECT * FROM {tableName} WHERE Username = @name", new { name });

//            }

//        }


//    }
//}



//apply asnch



using Dapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infrastructure1.Repositories
{
    public class BusRepository : IBusRepository
    {
        private readonly string _connectionString;
        private readonly IRepository<Buses> _repository;



        public BusRepository(IRepository<Buses> repository, string connectionString)
        {
            _repository = repository;

           _connectionString = connectionString;


        }

        public async Task AddAsync(Buses entity)
        {
           await _repository.AddAsync(entity);
           
        }

        public async Task<IEnumerable<Buses>> GetAllAsync()
        {
             return await _repository.GetAllAsync();
        }

        public async Task<Buses> GetByIdAsync(int id)
        {
            return await _repository.FindByIdAsync(id);
        }

        public async Task UpdateAsync(Buses entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _repository.DeleteByIdAsync(id);
        }

        public async Task<string> GetBusNameAsync(string name)
        {
            

            var tableName = typeof(Buses).Name;
            string busName = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.QueryFirstOrDefault<string>($"SELECT Name FROM {tableName} WHERE Name = @name", new { name });

            }
        }


        public async Task<IEnumerable<Buses>> GetAllBusesAsync(string name)
        {
            var tableName = typeof(Buses).Name;
            IEnumerable<Buses> busesList;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                busesList = await connection.QueryAsync<Buses>($"SELECT * FROM {tableName} WHERE Username = @name", new { name });
            }

            return busesList;
        }

        //public async Task<IEnumerable<Buses>> GetAllBusesAsync(string name)
        //{
        //    var tableName = typeof(Buses).Name;
        //    Buses entity = default(Buses);

        //    using (var connection = new SqlConnection(_connectionString))
        //    {


        //        connection.Open();


        //        return (IEnumerable<Buses>)connection.QueryFirstOrDefault<Buses>($"SELECT * FROM {tableName} WHERE Username = @name", new { name });

        //    }





        //}





    }
}

