CREATE DATABASE ProductDB

USE ProductDB

CREATE TABLE [dbo].[Product](
	[Product_Id] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [varchar](30) NULL,
	[Description] [varchar](max) NULL,
	[Image_Name] [varchar](max) NULL,
	[Vendor_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Rating]    Script Date: 21-06-2021 18:48:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Rating](
	[Product_Id] [int] NOT NULL,
	[User_Id] [int] NOT NULL,
	[Rating] [int] NULL,
	[User_Name] [varchar](50) NULL,
 CONSTRAINT [PK_ProdRating_ProdIdUserId] PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC,
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (1, N'Nike-Shoes', N'shoes', N'nike-shoes.jpg', 1)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (2, N'Nike-Jersey', N'jersey', N'nike-jersey.jpg', 1)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (3, N'Adidas-shoes', N'shoes', N'adidas-shoes.jpg', 2)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (4, N'Adidas-jersey', N'jersey', N'adidas-jersey.jpg', 2)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (5, N'Puma-shoes', N'shoes', N'puma-shoes.jpg', 3)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (6, N'Puma-jersey', N'jersey', N'puma-jersey.jpg', 3)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (7, N'Fila-shoes', N'shoes', N'fila-shoes.jpg', 4)
INSERT [dbo].[Product] ([Product_Id], [Product_Name], [Description], [Image_Name], [Vendor_Id]) VALUES (8, N'Fila-jersey', N'jersey', N'fila-jersey.jpg', 4)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
INSERT [dbo].[Product_Rating] ([Product_Id], [User_Id], [Rating], [User_Name]) VALUES (1, 1, 10, N'Aman')
INSERT [dbo].[Product_Rating] ([Product_Id], [User_Id], [Rating], [User_Name]) VALUES (2, 1, 10, N'Aman')

