CREATE DATABASE ProceedToByDB

USE ProceedToBuyDB

CREATE TABLE [dbo].[Cart](
	[UserId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[ZipCode] [int] NULL,
	[ExpectedDelivery] [datetime] NULL,
	[ProductName] [varchar](50) NULL,
 CONSTRAINT [PK_UIDVIDPID] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MiniUser]    Script Date: 21-06-2021 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MiniUser](
	[UserId] [int] NOT NULL,
	[ZipCode] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 21-06-2021 18:47:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlist](
	[UserId] [int] NOT NULL,
	[ProductName] [varchar](50) NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK_Wishlist_UIDVIDPID] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Cart] ([UserId], [ProductId], [Quantity], [ZipCode], [ExpectedDelivery], [ProductName]) VALUES (1, 1, 1, 201301, CAST(N'2021-06-22T00:00:00.000' AS DateTime), N'Nike-shoes')
GO
INSERT [dbo].[MiniUser] ([UserId], [ZipCode]) VALUES (1, CAST(201301 AS Decimal(18, 0)))
INSERT [dbo].[MiniUser] ([UserId], [ZipCode]) VALUES (2, CAST(301301 AS Decimal(18, 0)))
INSERT [dbo].[MiniUser] ([UserId], [ZipCode]) VALUES (3, CAST(101101 AS Decimal(18, 0)))
INSERT [dbo].[MiniUser] ([UserId], [ZipCode]) VALUES (4, CAST(201201 AS Decimal(18, 0)))
INSERT [dbo].[MiniUser] ([UserId], [ZipCode]) VALUES (1002, CAST(100000 AS Decimal(18, 0)))
GO
INSERT [dbo].[Wishlist] ([UserId], [ProductName], [ProductId], [Quantity], [DateAdded]) VALUES (1, N'Adidas-jersey', 4, 1, CAST(N'2021-06-21T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[MiniUser] ([UserId])