using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Dapper
{
    public interface IDapperConnection
    {
        public Task Execute(object parameters, string procedureName, CommandType commandType);
        public Task<T> Query<T>(object parameters, string procedureName, CommandType commandType);
        public Task<IEnumerable<T>> QueryAll<T>(object? parameters, string procedureName, CommandType commandType);

    }
}
