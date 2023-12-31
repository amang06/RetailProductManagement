CREATE DATABASE UserDB

USE UserDB

CREATE TABLE [dbo].[User](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[User_Name] [varchar](15) NULL,
	[Password] [varchar](30) NULL,
	[Zip_Code] [numeric](6, 0) NULL,
	[Token] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([User_Id], [User_Name], [Password], [Zip_Code], [Token]) VALUES (1, N'Aman', N'amanpass', CAST(201301 AS Numeric(6, 0)), N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IkFtYW4iLCJuYmYiOjE2MjQyODAyODUsImV4cCI6MTYyNDI4MTE4NSwiaWF0IjoxNjI0MjgwMjg1fQ.oaNyp1W7G89ZeIHHbIqA8LgR9ADB1buVd8P3pdG67VA')
INSERT [dbo].[User] ([User_Id], [User_Name], [Password], [Zip_Code], [Token]) VALUES (2, N'Jayanti', N'jayantipass', CAST(301301 AS Numeric(6, 0)), NULL)
INSERT [dbo].[User] ([User_Id], [User_Name], [Password], [Zip_Code], [Token]) VALUES (3, N'Yogesh', N'yogeshpass', CAST(101101 AS Numeric(6, 0)), NULL)
INSERT [dbo].[User] ([User_Id], [User_Name], [Password], [Zip_Code], [Token]) VALUES (4, N'Sonali', N'sonalipass', CAST(201201 AS Numeric(6, 0)), NULL)
INSERT [dbo].[User] ([User_Id], [User_Name], [Password], [Zip_Code], [Token]) VALUES (1002, N'testuser', N'testuser', CAST(100000 AS Numeric(6, 0)), NULL)
SET IDENTITY_INSERT [dbo].[User] OFF