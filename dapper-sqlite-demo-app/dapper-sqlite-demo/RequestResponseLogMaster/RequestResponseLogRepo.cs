using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
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
            var parameters = new { Id = log.Id, InsertDate = DateTime.Now, HttpVerb = log.HttpVerb, User = log.User, RequestHost = log.RequestHost, RequestPath = log.RequestPath, RequestQueryString = log.RequestQueryString, RequestBody = log.RequestBody, ResponseStatusCode = log.ResponseStatusCode, ResponseBody = log.ResponseBody };
            var sql = @"INSERT INTO [request_response_log]
                                               (
                                                [id]
                                               ,[insert_date]
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
                                               @Id,
		                                       @InsertDate,
                                               @HttpVerb,
                                               @User, 
                                               @RequestHost, 
                                               @RequestPath, 
                                               @RequestQueryString,
                                               @RequestBody,
                                               @ResponseStatusCode, 
                                               @ResponseBody);";
            await connection.ExecuteAsync(sql, parameters);
        }
    }
}
