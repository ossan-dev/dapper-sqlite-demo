using dapper_sqlite_demo.Database;
using dapper_sqlite_demo.LogMaster;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace dapper_sqlite_demo_tests
{
    public class LogRepoTest
    {
        private readonly IDbConnection _connection;
        public LogRepoTest()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            _connection = connection;
        }

        [Fact]
        public async Task LogRepo_Insert_New_Record_In_Db() 
        {
            // arrange
            var log = new Log
                {
                    Id = 2, HttpVerb="GET", InsertDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), RequestBody = "myRequestBody", RequestHost="myRequestHost", RequestPath="myRequestPath", RequestQueryString="myRequestQueryString", ResponseBody = "myResponseBody", ResponseStatusCode = 200, User="mockedUser"
                };

            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act 
            ILogRepo logRepo = new LogRepo(_connection);
            await logRepo.Create(log);

            // assert
            var logs = await TestHelper.GetLogsFromDbAsync(_connection);
            Assert.Equal(JsonSerializer.Serialize(log), JsonSerializer.Serialize(logs.ToArray()[0]));
        }

        [Fact]
        public async Task LogRepo_Must_Insert_Multiple_Logs()
        {
            // arrange
            var logs = new List<Log>
            {
                new Log
                {
                    Id = 1, HttpVerb="GET", InsertDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), RequestBody = "myRequestBody", RequestHost="myRequestHost", RequestPath="myRequestPath", RequestQueryString="myRequestQueryString", ResponseBody = "myResponseBody", ResponseStatusCode = 200, User="mockedUser"
                },
                new Log
                {
                    Id = 2, HttpVerb="GET", InsertDate= new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), RequestBody = "myRequestBody", RequestHost="myRequestHost", RequestPath="myRequestPath", RequestQueryString="myRequestQueryString", ResponseBody = "myResponseBody", ResponseStatusCode = 200, User="mockedUser"
                }
            };

            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act
            ILogRepo logRepo = new LogRepo(_connection);
            foreach (var log in logs)
            {
                await logRepo.Create(log);
            }

            // assert
            var logsResult = await TestHelper.GetLogsFromDbAsync(_connection);
            Assert.Equal(JsonSerializer.Serialize(logs), JsonSerializer.Serialize(logsResult));
        }

        [Fact]
        public async Task LogRepo_Must_Fail_If_Any_Fields_Is_NULL() 
        {
            // arrange
            var log = new Log
            {
                Id = 2,
                HttpVerb = "GET",
                InsertDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                RequestBody = "myRequestBody",
                RequestHost = "myRequestHost",
                RequestPath = "myRequestPath",
                RequestQueryString = "myRequestQueryString",
                ResponseBody = "myResponseBody",
                ResponseStatusCode = 200,
                User = null
            };

            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act
            ILogRepo logRepo = new LogRepo(_connection);
            Func<Task> func = async () => await logRepo.Create(log);

            // assert
            await Assert.ThrowsAsync<SqliteException>(func);
        }

        [Fact]
        public async Task LogRepo_Must_Fail_If_Any_Fields_Is_Missing()
        {
            // arrange
            var log = new Log
            {
                Id = 2,
                HttpVerb = "GET",
                InsertDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),
                RequestBody = "myRequestBody",
                RequestHost = "myRequestHost",
                RequestPath = "myRequestPath",
                RequestQueryString = "myRequestQueryString",
                ResponseBody = "myResponseBody",
                ResponseStatusCode = 200
            };

            IDbBootstrap dbBootstrap = new DbBootstrap(_connection);
            await dbBootstrap.SetupAsync();

            // act
            ILogRepo logRepo = new LogRepo(_connection);
            Func<Task> func = async () => await logRepo.Create(log);

            // assert
            await Assert.ThrowsAsync<SqliteException>(func);
        }
    }
}
