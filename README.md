DBDScript


USE [NotesManagementDB]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 29-04-2024 12:52:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[Email] [varchar](255) NULL,
	[MobileNo] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


---------------------------------------------------------------

USE [NotesManagementDB]
GO

/****** Object:  Table [dbo].[Notes]    Script Date: 29-04-2024 12:52:00 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notes](
	[NoteId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Notes] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Notes]  WITH CHECK ADD  CONSTRAINT [FK_Notes_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Notes] CHECK CONSTRAINT [FK_Notes_Users]
GO



------------------------------------------


USE [NotesManagementDB]
GO

/****** Object:  Table [dbo].[LoginAccessToken]    Script Date: 29-04-2024 12:51:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LoginAccessToken](
	[UserId] [int] NULL,
	[AccessToken] [nvarchar](500) NULL
) ON [PRIMARY]
GO






