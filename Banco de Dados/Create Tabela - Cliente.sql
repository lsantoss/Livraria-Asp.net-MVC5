USE [Livraria]
GO

/****** Object:  Table [dbo].[TBCliente]    Script Date: 01/01/2019 01:53:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBCliente](
	[id] [bigint] NOT NULL,
	[nome] [nvarchar](50) NOT NULL,
	[idade] [int] NOT NULL,
	[sexo] [nvarchar](50) NOT NULL,
	[telefone] [nvarchar](50) NOT NULL,
	[cpf] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TBCliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


