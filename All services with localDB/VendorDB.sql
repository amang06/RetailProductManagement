CREATE DATABASE VendorDB

USE VendorDB

CREATE TABLE [dbo].[Vendor](
	[Vendor_Id] [int] IDENTITY(1,1) NOT NULL,
	[Vendor_Name] [varchar](30) NULL,
	[Delivery_Charge] [int] NULL,
	[Vendor_Rating] [int] NULL,
	[Vendor_Zip_Code] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Vendor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor_Stock]    Script Date: 22-06-2021 21:15:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor_Stock](
	[Product_Id] [int] NOT NULL,
	[Vendor_Id] [int] NOT NULL,
	[Stock_In_Hand] [int] NULL,
	[Expected_StockReplenish_Date] [datetime] NULL,
 CONSTRAINT [PK_VendorStock_PIDVID] PRIMARY KEY CLUSTERED 
(
	[Product_Id] ASC,
	[Vendor_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Vendor] ON 

INSERT [dbo].[Vendor] ([Vendor_Id], [Vendor_Name], [Delivery_Charge], [Vendor_Rating], [Vendor_Zip_Code]) VALUES (1, N'NIke', 100, 10, 201301)
INSERT [dbo].[Vendor] ([Vendor_Id], [Vendor_Name], [Delivery_Charge], [Vendor_Rating], [Vendor_Zip_Code]) VALUES (2, N'Adidas', 200, 9, 119119)
INSERT [dbo].[Vendor] ([Vendor_Id], [Vendor_Name], [Delivery_Charge], [Vendor_Rating], [Vendor_Zip_Code]) VALUES (3, N'Puma', 300, 9, 212121)
INSERT [dbo].[Vendor] ([Vendor_Id], [Vendor_Name], [Delivery_Charge], [Vendor_Rating], [Vendor_Zip_Code]) VALUES (4, N'FIla', 400, 8, 232323)
SET IDENTITY_INSERT [dbo].[Vendor] OFF
GO
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (1, 1, 50, CAST(N'2021-10-10T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (2, 1, 40, CAST(N'2021-09-25T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (3, 2, 0, CAST(N'2021-07-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (4, 2, 70, CAST(N'2021-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (5, 3, 100, CAST(N'2021-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (6, 3, 0, CAST(N'2021-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (7, 4, 0, CAST(N'2021-08-01T00:00:00.000' AS DateTime))
INSERT [dbo].[Vendor_Stock] ([Product_Id], [Vendor_Id], [Stock_In_Hand], [Expected_StockReplenish_Date]) VALUES (8, 4, 0, CAST(N'2021-08-10T00:00:00.000' AS DateTime))
GO
ALTER TABLE [dbo].[Vendor_Stock]  WITH CHECK ADD FOREIGN KEY([Vendor_Id])
REFERENCES [dbo].[Vendor] ([Vendor_Id])