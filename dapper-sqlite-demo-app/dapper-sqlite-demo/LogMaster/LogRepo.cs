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
            var parameters = new { log.Id, InsertDate = DateTime.Now, log.HttpVerb, log.User, log.RequestHost, log.RequestPath, log.RequestQueryString, log.RequestBody, log.ResponseStatusCode, log.ResponseBody };
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
                            ,[response_body]
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
