USE [Livraria]
GO

/****** Object:  Table [dbo].[TBUsuario]    Script Date: 01/01/2019 01:55:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBUsuario](
	[id] [bigint] NOT NULL,
	[login] [nvarchar](50) NOT NULL,
	[senha] [nvarchar](50) NOT NULL,
	[validade] [smalldatetime] NOT NULL,
	[privilegio] [int] NOT NULL,
 CONSTRAINT [PK_TBUsuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


