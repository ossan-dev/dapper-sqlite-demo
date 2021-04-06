using Dapper;
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
            connection.Execute(@"/****** Object:  Table [dbo].[request_response_log]    Script Date: 06/04/2021 14:07:16 ******/
                                SET ANSI_NULLS ON
                                GO

                                SET QUOTED_IDENTIFIER ON
                                GO

                                CREATE TABLE [dbo].[request_response_log](
	                                [id] [int] IDENTITY(1,1) NOT NULL,
	                                [insert_date] [datetime] NOT NULL,
	                                [http_verb] [varchar](20) NOT NULL,
	                                [user] [nvarchar](100) NOT NULL,
	                                [request_host] [nvarchar](500) NOT NULL,
	                                [request_path] [nvarchar](max) NOT NULL,
	                                [request_query_string] [nvarchar](max) NOT NULL,
	                                [request_body] [nvarchar](max) NOT NULL,
	                                [response_status_code] [int] NOT NULL,
	                                [response_body] [nvarchar](max) NOT NULL,
                                 CONSTRAINT [PK_request_response_log] PRIMARY KEY NONCLUSTERED 
                                (
	                                [id] ASC
                                )WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
                                ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
                                GO

                                ALTER TABLE [dbo].[request_response_log] ADD  DEFAULT (getutcdate()) FOR [insert_date]
                                GO

                                ");
        }
    }
}
