


//using Dapper;
//using Domain.Entities;
//using Domain.Interface;
//using Microsoft.Data.SqlClient;

//using static Dapper.SqlMapper;

//namespace Infrastructure1.Repositories
//{

//    public class SeatsRepository : ISeatsRepository
//    {


//        private readonly IRepository<BusSeats> _repository;
//        private readonly string connectionString;

//        public SeatsRepository(IRepository<BusSeats> repository, string connectionString)
//        {
//            _repository = repository;

//            this.connectionString = connectionString;


//        }


//        public void Add(BusSeats entity)
//        {
//            _repository.Add(entity);
//        }


//        public IEnumerable<BusSeats> GetAll()
//        {
//            return _repository.GetAll();
//        }
//        public BusSeats FindById(int code)
//        {
//            return _repository.FindById(code);
//        }
//        public IEnumerable<BusSeats> GetAll(string name)
//        {
//            return _repository.GetAll(name);

//        }


//        public void updateBooking(BusSeats entity)
//        {
//            _repository.Update(entity);
//        }
//        public void DeleteById(int id)
//        {
//            _repository.DeleteById(id);
//        }

//        public void RemoveSeatsByUsername(string username)
//        {
//            var query = "DELETE FROM BusSeats WHERE Username = @Username";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                connection.Execute(query, new { Username = username });
//            }
//        }

//        public void AddSeatNumber(BusSeats entity)
//        {
//            var tableName = typeof(BusSeats).Name;
//            //get column names 
//            var properties = typeof(BusSeats).GetProperties().Where(p => p.Name != "SeatId"); // sari lines get ho jae lekin id get na ho

//            // now get prop in term of , separated 

//            var columnName = string.Join(",", properties.Select(p => p.Name));

//            // make paramters
//            var parameterNames = string.Join(",", properties.Select(y => "@" + y.Name));

//            // now make query 

//            var query = $"insert into {tableName} ({columnName}) values ({parameterNames})";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                connection.Execute(query, entity);
//            }
//        }

//        public void UpdateSeats(BusSeats entity)
//        {
//            var entityType = typeof(BusSeats);

//            var properties = entityType.GetProperties();
//            var tableName = entityType.Name;
//            var keyProperty = properties.FirstOrDefault(prop => prop.Name == "SeatId");

//            if (keyProperty == null)
//            {
//                throw new Exception("Entity must have an Id property");
//            }

//            var keyPropertyValue = keyProperty.GetValue(entity);
//            var columnNames = properties.Select(p => p.Name).Where(n => n != "SeatId").ToArray();
//            var columnUpdates = columnNames.Select(cn => $"{cn} = @{cn}");

//            var updateSql = $"UPDATE {tableName} SET {string.Join(", ", columnUpdates)} WHERE SeatId = @SeatId";

//            using (var connection = new SqlConnection(connectionString))
//            {

//                connection.Open();
//                connection.Execute(updateSql, entity);

//            }
//        }


//    }
//}

// applya snchrous



using Dapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure1.Repositories
{
    public class SeatsRepository : ISeatsRepository
    {
        private readonly IRepository<BusSeats> _repository;
        private readonly string connectionString;

        public SeatsRepository(IRepository<BusSeats> repository, string connectionString)
        {
            _repository = repository;
            this.connectionString = connectionString;
        }

        public async Task AddSeatNumberAsync(BusSeats entity)
        {
            var tableName = typeof(BusSeats).Name;
            var properties = typeof(BusSeats).GetProperties().Where(p => p.Name != "SeatId");
            var columnName = string.Join(",", properties.Select(p => p.Name));
            var parameterNames = string.Join(",", properties.Select(y => "@" + y.Name));
            var query = $"INSERT INTO {tableName} ({columnName}) VALUES ({parameterNames})";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task UpdateSeatsAsync(BusSeats entity)
        {
            var tableName = typeof(BusSeats).Name;
            var keyProperty = typeof(BusSeats).GetProperties().FirstOrDefault(p => p.Name == "SeatId");

            if (keyProperty == null)
            {
                throw new Exception("Entity must have an Id property");
            }

            var keyPropertyValue = keyProperty.GetValue(entity);
            var columnNames = typeof(BusSeats).GetProperties().Select(p => p.Name).Where(n => n != "SeatId").ToArray();
            var columnUpdates = columnNames.Select(cn => $"{cn} = @{cn}");
            var updateSql = $"UPDATE {tableName} SET {string.Join(", ", columnUpdates)} WHERE SeatId = @SeatId";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(updateSql, entity);
            }
        }

        public async Task RemoveSeatsByUsernameAsync(string username)
        {
            var query = "DELETE FROM BusSeats WHERE Username = @Username";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, new { Username = username });
            }
        }
    }
}


