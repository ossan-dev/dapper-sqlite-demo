using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Database
{
    public class DbBootstrap : IDbBootstrap
    {
        private readonly IDbConnection _connection;        

        public DbBootstrap(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task SetupAsync()
        {
            var table = await _connection.QueryAsync<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'log';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "log")
                return;            
            await _connection.ExecuteAsync(@"
                                CREATE TABLE [log](
	                                [Id] [int] NOT NULL,
	                                [InsertDate] [datetime] NOT NULL,
	                                [HttpVerb] text NOT NULL,
	                                [User] text NOT NULL,
	                                [RequestHost] text NOT NULL,
	                                [RequestPath] text NOT NULL,
	                                [RequestQueryString] text NOT NULL,
	                                [RequestBody] text NOT NULL,
	                                [ResponseStatusCode] [int] NOT NULL,
	                                [ResponseBody] text NOT NULL)
                                ");
        }
    }
}
