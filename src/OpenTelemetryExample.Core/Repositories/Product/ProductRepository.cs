using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace OpenTelemetryExample.Core.Repositories.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        public ProductRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public async Task<int> AddAsync(Models.Product entity)
        {
            var sql = @"INSERT INTO ""Products"" (""Name"",""Description"",""Sku"",""Price"") VALUES (@Name,@Description,@Sku,@Price)";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = @"DELETE FROM ""Products"" WHERE ""Id"" = @Id";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        public async Task<IReadOnlyList<Models.Product>> GetAllAsync()
        {
            var sql = @"SELECT * FROM ""Products""";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Models.Product>(sql);
                return result.ToList();
            }
        }
        public async Task<Models.Product> GetByIdAsync(int id)
        {
            var sql = @"SELECT * FROM ""Products"" WHERE ""Id"" = @Id";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Models.Product>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> UpdateAsync(Models.Product entity)
        {
            var sql = @"UPDATE ""Products"" SET ""Name"" = @Name, ""Description"" = @Description, ""Sku"" = @Sku, ""Price"" = @Price WHERE ""Id"" = @Id";
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
