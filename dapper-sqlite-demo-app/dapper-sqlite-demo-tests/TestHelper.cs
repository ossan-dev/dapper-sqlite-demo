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
                var parameters = new { log.Id, log.InsertDate, log.HttpVerb, log.User, log.RequestHost, log.RequestPath, log.RequestQueryString, log.RequestBody, log.ResponseStatusCode, log.ResponseBody };
                var sql = @"INSERT INTO [log]
                                               (
                                                 [Id]
                                                ,[InsertDate]
                                                ,[HttpVerb]
                                                ,[User]
                                                ,[RequestHost]
                                                ,[RequestPath]
                                                ,[RequestQueryString]
                                                ,[RequestBody]
                                                ,[ResponseStatusCode]
                                                ,[ResponseBody]
                                                )
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
