IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

CREATE TABLE Users (
	[Id] [nvarchar](300) NOT NULL,
	[Password] [nvarchar](1000) NULL,

    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Users (Id, [Password]) values ('myUser', 'myPass')

CREATE TABLE Employees (
	[Id] [nvarchar](300) NOT NULL,
	[Password] [nvarchar](1000) NULL,

    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC)
)