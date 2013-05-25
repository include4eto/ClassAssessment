
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 05/24/2013 17:53:47
-- Generated from EDMX file: D:\Projects\ClassAssessment\ClassAssessment\ClassAssessment\ClassAssessment\Models\MainDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Answer_Assessment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_Answer_Assessment];
GO
IF OBJECT_ID(N'[dbo].[FK_Answer_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_Answer_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_Assessment_UserFrom]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Assessments] DROP CONSTRAINT [FK_Assessment_UserFrom];
GO
IF OBJECT_ID(N'[dbo].[FK_Assessment_UserTo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Assessments] DROP CONSTRAINT [FK_Assessment_UserTo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[Assessments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Assessments];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Question] int  NULL,
    [Value] int  NULL,
    [Assessment] int  NULL
);
GO

-- Creating table 'Assessments'
CREATE TABLE [dbo].[Assessments] (
    [id] int IDENTITY(1,1) NOT NULL,
    [From] int  NULL,
    [To] int  NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [id] int  NOT NULL,
    [Text] nvarchar(max)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [Password] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Assessments'
ALTER TABLE [dbo].[Assessments]
ADD CONSTRAINT [PK_Assessments]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Assessment] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answer_Assessment]
    FOREIGN KEY ([Assessment])
    REFERENCES [dbo].[Assessments]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Assessment'
CREATE INDEX [IX_FK_Answer_Assessment]
ON [dbo].[Answers]
    ([Assessment]);
GO

-- Creating foreign key on [Question] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_Answer_Question]
    FOREIGN KEY ([Question])
    REFERENCES [dbo].[Questions]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Answer_Question'
CREATE INDEX [IX_FK_Answer_Question]
ON [dbo].[Answers]
    ([Question]);
GO

-- Creating foreign key on [From] in table 'Assessments'
ALTER TABLE [dbo].[Assessments]
ADD CONSTRAINT [FK_Assessment_UserFrom]
    FOREIGN KEY ([From])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Assessment_UserFrom'
CREATE INDEX [IX_FK_Assessment_UserFrom]
ON [dbo].[Assessments]
    ([From]);
GO

-- Creating foreign key on [To] in table 'Assessments'
ALTER TABLE [dbo].[Assessments]
ADD CONSTRAINT [FK_Assessment_UserTo]
    FOREIGN KEY ([To])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Assessment_UserTo'
CREATE INDEX [IX_FK_Assessment_UserTo]
ON [dbo].[Assessments]
    ([To]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------