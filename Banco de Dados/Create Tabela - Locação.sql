USE [Livraria]
GO

/****** Object:  Table [dbo].[TBLocacao]    Script Date: 01/01/2019 01:55:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TBLocacao](
	[id] [bigint] NOT NULL,
	[idcliente] [bigint] NOT NULL,
	[idlivro] [bigint] NOT NULL,
	[data] [smalldatetime] NOT NULL,
	[entrega] [smalldatetime] NULL,
	[preco] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TBLocacao] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TBLocacao]  WITH CHECK ADD  CONSTRAINT [FK_TBLocacao_TBCliente] FOREIGN KEY([idcliente])
REFERENCES [dbo].[TBCliente] ([id])
GO

ALTER TABLE [dbo].[TBLocacao] CHECK CONSTRAINT [FK_TBLocacao_TBCliente]
GO

ALTER TABLE [dbo].[TBLocacao]  WITH CHECK ADD  CONSTRAINT [FK_TBLocacao_TBLivro] FOREIGN KEY([idlivro])
REFERENCES [dbo].[TBLivro] ([id])
GO

ALTER TABLE [dbo].[TBLocacao] CHECK CONSTRAINT [FK_TBLocacao_TBLivro]
GO


