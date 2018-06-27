
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/22/2018 11:16:12
-- Generated from EDMX file: E:\MyProject\MVC\SpaceHome\SpaseHome.Model\SpaceHomeBase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SpaseHome];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Account]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Account];
GO
IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Account'
CREATE TABLE [dbo].[Account] (
    [AccountId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(50)  NULL,
    [PassWord] nvarchar(50)  NULL,
    [PhoneNum] nvarchar(11)  NULL,
    [Gender] tinyint  NULL,
    [RegTime] datetime  NOT NULL,
    [DelFlag] tinyint  NOT NULL,
    [LastLoginTime] datetime  NULL,
    [LoginErrorTimes] tinyint  NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [UserInfo_UserId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [UserId] uniqueidentifier  NOT NULL,
    [Nick] nvarchar(20)  NULL,
    [Gender] tinyint  NULL,
    [BrithDay] datetime  NULL,
    [Sign] nvarchar(5)  NULL,
    [BloodType] nvarchar(1)  NULL,
    [MaritalStatus] nvarchar(5)  NULL,
    [Address] nvarchar(50)  NULL,
    [BirthPlace] nvarchar(50)  NULL,
    [HeadImg] nvarchar(100)  NULL,
    [AccountId] int  NULL,
    [Age] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountId] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [UserId] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfo_UserId] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [FK_AccountUserInfo]
    FOREIGN KEY ([UserInfo_UserId])
    REFERENCES [dbo].[UserInfo]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountUserInfo'
CREATE INDEX [IX_FK_AccountUserInfo]
ON [dbo].[Account]
    ([UserInfo_UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------