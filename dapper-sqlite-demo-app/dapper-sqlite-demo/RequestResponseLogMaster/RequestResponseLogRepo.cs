using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.RequestResponseLogMaster
{
    public class RequestResponseLogRepo : IRequestResponseLogRepo
    {
        private readonly DatabaseConfig _databaseConfig;

        public RequestResponseLogRepo(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        public async Task Create(RequestResponseLog log)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            await connection.ExecuteAsync(@"INSERT INTO [dbo].[request_response_log]
                                               ([insert_date]
                                               ,[http_verb]
                                               ,[user]
                                               ,[request_host]
                                               ,[request_path]
                                               ,[request_query_string]
                                               ,[request_body]
                                               ,[response_status_code]
                                               ,[response_body])
                                         VALUES
                                               (
		                                       @insert_date,
                                               @http_verb,
                                               @user, 
                                               @request_host, 
                                               @request_path, 
                                               @request_query_string,
                                               @request_body,
                                               @response_status_code, 
                                               @response_body)", log);
        }
    }
}
