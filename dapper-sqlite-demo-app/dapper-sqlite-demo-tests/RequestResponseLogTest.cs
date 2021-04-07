using Dapper;
using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.RequestResponseLogMaster;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class RequestResponseLogTest
    {
        [Fact]
        public async Task SelectCountQuery_Must_Return_Exact_Number() 
        {
            // arrange
            var logs = new List<RequestResponseLog>
            {
                new RequestResponseLog{ Id = 1, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-1), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 200, User="mockedUser"},
                new RequestResponseLog{ Id = 2, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-2), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 500, User="mockedUser"},
                new RequestResponseLog{ Id = 3, HttpVerb="GET", InsertDate= DateTime.Now.AddYears(-3), RequestBody = "{'requestBody': 'request from mock instance'}", RequestHost="mockedInstance", RequestPath="/mocked/api/", RequestQueryString="?instance=mocked", ResponseBody = "{'responseBody': 'response mocked by me'}", ResponseStatusCode = 400, User="mockedUser"}
            };

            var connection = new SqliteConnection("DataSource=:memory:");
            await connection.OpenAsync();
            await connection.ExecuteAsync(@"
                                CREATE TABLE [request_response_log](
	                                [id] [int] IDENTITY(1,1) NOT NULL,
	                                [insert_date] [datetime] NOT NULL,
	                                [http_verb] text NOT NULL,
	                                [user] text NOT NULL,
	                                [request_host] text NOT NULL,
	                                [request_path] text NOT NULL,
	                                [request_query_string] text NOT NULL,
	                                [request_body] text NOT NULL,
	                                [response_status_code] [int] NOT NULL,
	                                [response_body] text NOT NULL)
                                ");

            foreach (var log in logs)
            {
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

            var queryResult = await connection.ExecuteScalarAsync<int>(@"SELECT COUNT(Id) FROM request_response_log");

            Assert.Equal(logs.Count, queryResult);
        }
    }
}
