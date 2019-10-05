USE [Livraria]
GO

/****** Object:  Table [dbo].[TBLivro]    Script Date: 01/01/2019 01:54:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBLivro](
	[id] [bigint] NOT NULL,
	[nome] [nvarchar](50) NOT NULL,
	[autor] [nvarchar](50) NOT NULL,
	[edicao] [int] NOT NULL,
	[inbs] [nvarchar](50) NOT NULL,
	[imagem] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TBLivro] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


