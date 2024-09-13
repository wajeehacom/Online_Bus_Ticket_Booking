




//using Dapper;
//using Domain.Interface;
//using Microsoft.Data.SqlClient;

//using System.Collections.Generic;
//using System.Text.Json;
//using System.Text.Json.Serialization;


//namespace Infrastructure1.Repositories
//{

//    public class GenericRepository<TEntity> : IRepository<TEntity>
//    {
//        private readonly string connectionString;

//        public GenericRepository(string connectionString)
//        {
//            this.connectionString = connectionString;

//        }
//        public void Add(TEntity entity)
//        {
//            //get table
//            var tableName = typeof(TEntity).Name;
//            //get column names 
//            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "BookingId");
//            // sari lines get ho jae lekin id get na ho

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

//        public void Update(TEntity entity)
//        {
//            var tableName = typeof(TEntity).Name;
//            var primaryKey = "BookingId";
//            var properties = typeof(TEntity).GetProperties().Where(x => x.Name != primaryKey); // Exclude the Image property

//            var setClause = string.Join(",", properties.Select(a => $"{a.Name} = @{a.Name}"));

//            var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey}";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                connection.Execute(query, entity);

//            }
//        }
//    public void DeleteById(int BookingId)
//        {
//            var tableName = typeof(TEntity).Name;

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                var query = $"DELETE FROM {tableName} WHERE BookingId = @BookingId";
//                connection.Execute(query, new { BookingId = BookingId });

//            }
//        }

//        public IEnumerable<TEntity> GetAll()
//        {
//            var tableName = typeof(TEntity).Name;

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();


//                return connection.Query<TEntity>($"SELECT * FROM {tableName}");

//            }

//        }

//        public IEnumerable<TEntity> GetAll(string name)
//        {
//            var tableName = typeof(TEntity).Name;
//            var entities = new List<TEntity>();

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = $"SELECT * FROM {tableName} where Name = @name";
//                return connection.Query<TEntity>(query, new { name });



//            }


//        }


//        public TEntity FindById(int code)
//        {
//            var tableName = typeof(TEntity).Name;
//            var primaryKey = "BookingId";

//            using (var connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                return connection.Query<TEntity>($"SELECT * FROM {tableName} WHERE {primaryKey} = @BookingId", new { BookingId = code }).FirstOrDefault();
//            }
//        }

//    }

//}



//applya nchrouns



using Dapper;
using Domain.Entities;
using Domain.Interface;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure1.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
    {
        private readonly string _connectionString;

        public GenericRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var properties = typeof(TEntity).GetProperties().Where(p => p.Name != "BookingId");

            var columnNames = string.Join(",", properties.Select(p => p.Name));
            var parameterNames = string.Join(",", properties.Select(p => "@" + p.Name));

            var query = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var tableName = typeof(TEntity).Name;
            var primaryKey = "BookingId";
            var properties = typeof(TEntity).GetProperties().Where(x => x.Name != primaryKey);

            var setClause = string.Join(",", properties.Select(a => $"{a.Name} = @{a.Name}"));

            var query = $"UPDATE {tableName} SET {setClause} WHERE {primaryKey} = @{primaryKey}";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(query, entity);
            }
        }

        public async Task DeleteByIdAsync(int bookingId)
        {
            var tableName = typeof(TEntity).Name;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var query = $"DELETE FROM {tableName} WHERE BookingId = @BookingId";
                await connection.ExecuteAsync(query, new { BookingId = bookingId });
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var tableName = typeof(TEntity).Name;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryAsync<TEntity>($"SELECT * FROM {tableName}");
            }
        }
        public async Task<IEnumerable<Booking>> GetAllAsync(string name)
        {
            var tableName = typeof(Booking).Name;
            var entities = new List<Booking>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {tableName} where Name = @name";
                return connection.Query<Booking>(query, new { name });



            }
        }



        //public async Task<IEnumerable<TEntity>> GetAllAsync(string name)
        //{



        //    var tableName = typeof(TEntity).Name;
        //    var entities = new List<TEntity>();

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        connection.Open();
        //        string query = $"SELECT * FROM {tableName} where Username = @name";
        //        return connection.Query<TEntity>(query, new { name });



        //    }
        //}

        //public async Task<IEnumerable<TEntity>> GetAllAsync(string name)
        //{
        //    var query = "SELECT * FROM Buses WHERE Name = @Username"; // Update column name if needed

        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        return await connection.QueryAsync<TEntity>(query, new { Name = name }); // Match parameter name
        //    }
        //}


        public async Task<TEntity> FindByIdAsync(int bookingId)
        {
            var tableName = typeof(TEntity).Name;
            var primaryKey = "BookingId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QueryFirstOrDefaultAsync<TEntity>($"SELECT * FROM {tableName} WHERE {primaryKey} = @BookingId", new { BookingId = bookingId });
            }
        }
    }
}








