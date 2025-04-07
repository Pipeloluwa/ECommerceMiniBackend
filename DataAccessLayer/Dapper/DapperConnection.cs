using Dapper;
using DataAccessLayer.IRepository;
using DomainLayer.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dapper
{
    public class DapperConnection(
        IConfiguration configuration
        ): IDapperConnection
    {

        private readonly string connectionString = configuration.GetConnectionString("DefaultConnection");

        public async Task Execute(object parameters, string procedureName, CommandType commandType)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            await connection.ExecuteAsync(
                    procedureName,
                    parameters,
                    commandType: commandType
                );
            await connection.CloseAsync();
        }

        public async Task<T> Query<T>(object parameters, string procedureName, CommandType commandType)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            T? result = await connection.QueryFirstOrDefaultAsync<T>(
                    procedureName,
                    parameters,
                    commandType: commandType
                );


            await connection.CloseAsync();

            return result!;
        }

        public async Task<IEnumerable<T>> QueryAll<T>(object? parameters, string procedureName, CommandType commandType)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            IEnumerable<T> result = await connection.QueryAsync<T>(
                    procedureName,
                    parameters,
                    commandType: commandType
                );


            await connection.CloseAsync();

            return result!;
        }

    }
}
