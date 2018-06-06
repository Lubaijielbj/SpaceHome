
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/06/2018 11:33:34
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

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Account'
CREATE TABLE [dbo].[Account] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(20)  NULL,
    [PassWord] nvarchar(20)  NULL,
    [UserPhone] nvarchar(11)  NULL,
    [NickName] nvarchar(20)  NULL,
    [Head] nvarchar(50)  NULL,
    [Gender] tinyint  NULL,
    [RegTime] datetime  NOT NULL,
    [DelFlag] tinyint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Account'
ALTER TABLE [dbo].[Account]
ADD CONSTRAINT [PK_Account]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------