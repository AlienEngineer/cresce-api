IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO

CREATE TABLE Users (
	[Id]                [NVARCHAR](300)     NOT NULL,
	[Password]          [NVARCHAR](1000)    NULL,

    CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Users (Id, [Password]) VALUES ('myUser', 'myPass')

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Organizations]') AND type in (N'U'))
DROP TABLE [dbo].[Organizations]
GO

CREATE TABLE Organizations (
	[Id]                [NVARCHAR](300)     NOT NULL,
	[UserId]            [NVARCHAR](300)     NOT NULL,

    CONSTRAINT [PK_ORGANIZATIONS] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Organizations (Id, [UserId]) VALUES ('myOrganization', 'myUser')

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
DROP TABLE [dbo].[Employees]
GO

CREATE TABLE Employees (
	[Id]                [NVARCHAR](300)     NOT NULL,
	[Title]             [NVARCHAR](150)     NULL,
	[Image]             [VARBINARY](MAX)    NULL,
    [OrganizationId]    [NVARCHAR](300)     NOT NULL,

    CONSTRAINT [PK_EMPLOYEES] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Employees (Id, [Title], [Image], [OrganizationId])
VALUES (
    'Ricardo Nunes',
    'Engineer',
    null,
    'myOrganization'
)
