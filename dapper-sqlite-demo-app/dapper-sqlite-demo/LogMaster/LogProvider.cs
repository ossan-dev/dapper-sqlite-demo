using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.LogMaster
{
    public class LogProvider : ILogProvider
    {
        private readonly IDbConnection _connection;

        public LogProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<Log>> Get()
        {
            var logs = await _connection.QueryAsync<Log>(@"SELECT [Id]
                                              ,[InsertDate]
                                              ,[HttpVerb]
                                              ,[User]
                                              ,[RequestHost]
                                              ,[RequestPath]
                                              ,[RequestQueryString]
                                              ,[RequestBody]
                                              ,[ResponseStatusCode]
                                              ,[ResponseBody]
                                          FROM [log]");

            return logs;
        }
    }
}
