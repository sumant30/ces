USE Master
GO
CREATE DATABASE CES
Go

CREATE TABLE [dbo].[Users] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Username]     VARCHAR (100)    NOT NULL,
    [Password]     VARCHAR (500)    NOT NULL,
    [RefreshToken] VARCHAR (200)    NULL,
    [Role]         VARCHAR (20)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
Go

CREATE TABLE [dbo].[Applications] (
    [Id]   UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR (100)    NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
Go

CREATE TABLE [dbo].[UserApplicationAccess] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [UserId]        UNIQUEIDENTIFIER NOT NULL,
    [ApplicationId] UNIQUEIDENTIFIER NOT NULL,
    [AccessType]    VARCHAR (10)     NOT NULL,
    [Granted]       BIT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_UserApplicationAccess_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_UserApplicationAccess_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[Applications] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);
Go
