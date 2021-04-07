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
            return await _connection.QueryAsync<Log>(@"SELECT [id]
                                              ,[insert_date]
                                              ,[http_verb]
                                              ,[user]
                                              ,[request_host]
                                              ,[request_path]
                                              ,[request_query_string]
                                              ,[request_body]
                                              ,[response_status_code]
                                              ,[response_body]
                                          FROM [request_response_log]");
        }
    }
}
