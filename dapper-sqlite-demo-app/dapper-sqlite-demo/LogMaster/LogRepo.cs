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
    public class LogRepo : ILogRepo
    {
        private readonly IDbConnection _connection;

        public LogRepo(IDbConnection connection)
        {
            _connection = connection;
        }
        public async Task Create(Log log)
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
                            @ResponseBody
                            );";
            await _connection.ExecuteAsync(sql, parameters);
        }
    }
}
