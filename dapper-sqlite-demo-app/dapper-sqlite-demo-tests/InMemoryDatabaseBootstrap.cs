using Dapper;
using dapper_sqlite_demo.Database;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace dapper_sqlite_demo_tests
{
    public class InMemoryDatabaseBootstrap
    {
        public InMemoryDatabaseBootstrap() : base()
        {

        }

        public async Task SetupAsync(IDbConnection connection)
        {
            connection.Open();
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
        }
    }
}
