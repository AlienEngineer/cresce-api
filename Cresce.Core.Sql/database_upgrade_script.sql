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
	[Id]                [INT]               NOT NULL        IDENTITY (1, 1),
	[Name]              [NVARCHAR](150)     NULL,
	[Title]             [NVARCHAR](150)     NULL,
	[Image]             [VARBINARY](MAX)    NULL,
    [OrganizationId]    [NVARCHAR](150)     NOT NULL,
    [Pin]               [NVARCHAR](150)     NOT NULL,

    CONSTRAINT [PK_EMPLOYEES] PRIMARY KEY CLUSTERED ([Id] ASC)
)

INSERT INTO Employees (Name, [Title], [Image], [OrganizationId], [Pin])
VALUES (
    'Ricardo Nunes',
    'Engineer',
    NULL,
    'myOrganization',
    '1234'
)

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Services]') AND type in (N'U'))
    DROP TABLE [dbo].[Services]
GO

create table Services
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [Name]              [NVARCHAR](150)     NOT NULL,
    [Value]             [FLOAT]             NOT NULL,
    [IsCoTherapy]       [BIT]               NULL,
    [Inactive]          [BIT]               NULL,
    [Image]             [VARBINARY](MAX)    NULL,
    [Intensive]         [BIT]               NULL,

    constraint PK_SERVICES                  PRIMARY KEY (Id),
    unique (Name)
)

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND type in (N'U'))
    DROP TABLE [dbo].[Customers]
GO

create table Customers
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [Name]              [NVARCHAR](150)     NOT NULL,
    [Inactive]          [BIT]               NULL,
    [Image]             [VARBINARY](MAX)    NULL,

    constraint PK_CUSTOMERS                 PRIMARY KEY (Id),
    unique (Name)
)


IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Appointments]') AND type in (N'U'))
    DROP TABLE [dbo].[Appointments]
GO

create table Appointments
(
    [Id]                [INT]               NOT NULL        IDENTITY (1, 1),
    [StartedAt]         [DATE]              NOT NULL,
    [CustomerId]        [INT]               NOT NULL,
    [ServiceId]         [INT]               NOT NULL,
    [EmployeeId]        [INT]               NOT NULL,
    [Hours]             [FLOAT]             NOT NULL,
    [Discount]          [FLOAT]             NULL,
    [Value]             [FLOAT]             NULL,
    [InvoiceId]         [INT]               NULL,

    constraint PK_APPOINTMENTS              PRIMARY KEY (Id),
    constraint Appointments_fk0             FOREIGN KEY (CustomerId) references Customers,
    constraint Appointments_fk1             FOREIGN KEY (ServiceId) references Service,
    constraint Appointments_fk2             FOREIGN KEY (EmployeeId) references [Employees]
)
