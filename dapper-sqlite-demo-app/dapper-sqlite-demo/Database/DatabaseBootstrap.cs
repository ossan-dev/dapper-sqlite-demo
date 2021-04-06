﻿using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_sqlite_demo.Database
{
    public class DatabaseBootstrap : IDatabaseBootstrap
    {
        private readonly DatabaseConfig _databaseConfig;

        public DatabaseBootstrap(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public void Setup()
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);

            var table = connection.Query<string>("SELECT name FROM sqlite_master WHERE type='table' AND name = 'request_response_log';");
            var tableName = table.FirstOrDefault();
            if (!string.IsNullOrEmpty(tableName) && tableName == "request_response_log")
                return;
            connection.Execute(@"
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
        }
    }
}
