using Dapper;
using dapper_sqlite_demo.LogMaster;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace dapper_sqlite_demo_tests
{
    public static class TestHelper
    {
        public static async Task SeedDataInDbAsync(IDbConnection connection, IEnumerable<Log> logs) 
        {
            foreach (var log in logs)
            {
                var parameters = new { Id = log.Id, InsertDate = DateTime.Now, HttpVerb = log.HttpVerb, User = log.User, RequestHost = log.RequestHost, RequestPath = log.RequestPath, RequestQueryString = log.RequestQueryString, RequestBody = log.RequestBody, ResponseStatusCode = log.ResponseStatusCode, ResponseBody = log.ResponseBody };
                var sql = @"INSERT INTO [log]
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
}
