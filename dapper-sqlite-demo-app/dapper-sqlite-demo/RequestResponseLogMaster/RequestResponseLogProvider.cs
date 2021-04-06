using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.RequestResponseLogMaster
{
    public class RequestResponseLogProvider : IRequestResponseLogProvider
    {
        private readonly DatabaseConfig _databaseConfig;

        public RequestResponseLogProvider(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<IEnumerable<RequestResponseLog>> Get()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryAsync<RequestResponseLog>(@"SELECT [id]
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
