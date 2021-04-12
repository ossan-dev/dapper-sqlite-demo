CREATE DATABASE [log-db]
GO

USE [log-db]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsertDate] [datetime] NOT NULL,
	[HttpVerb] [varchar](20) NOT NULL,
	[User] [nvarchar](100) NOT NULL,
	[RequestHost] [nvarchar](500) NOT NULL,
	[RequestPath] [nvarchar](max) NOT NULL,
	[RequestQueryString] [nvarchar](max) NOT NULL,
	[RequestBody] [nvarchar](max) NOT NULL,
	[ResponseStatusCode] [int] NOT NULL,
	[ResponseBody] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_request_response_log] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[log] ADD  DEFAULT (getutcdate()) FOR [InsertDate]
GO