
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/13/2013 19:45:36
-- Generated from EDMX file: C:\Users\sdenison\work\Projects\PharmacyClaimAdjudicator\src\PharmacyAdjudicator\PharmacyAdjudicator.DataAccess\PharmacyAdjFromDatabase.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PharmacyClaimAdjudicator];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FKGroupFacts397517]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupFacts] DROP CONSTRAINT [FK_FKGroupFacts397517];
GO
IF OBJECT_ID(N'[dbo].[FK_Group_PatientGroup_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroups] DROP CONSTRAINT [FK_Group_PatientGroup_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupFacts_GroupFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupFacts] DROP CONSTRAINT [FK_GroupFacts_GroupFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PateintFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroups] DROP CONSTRAINT [FK_Patient_PateintFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PatientFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientFacts] DROP CONSTRAINT [FK_Patient_PatientFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientFacts_PatientFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientFacts] DROP CONSTRAINT [FK_PatientFacts_PatientFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientGroup_PateintGroup_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroups] DROP CONSTRAINT [FK_PatientGroup_PateintGroup_Rel];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Groups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Groups];
GO
IF OBJECT_ID(N'[dbo].[GroupFacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupFacts];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO
IF OBJECT_ID(N'[dbo].[PatientFacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientFacts];
GO
IF OBJECT_ID(N'[dbo].[PatientGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientGroups];
GO
IF OBJECT_ID(N'[dbo].[VaDrugs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VaDrugs];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
    [GroupGroupId] int  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'GroupFacts'
CREATE TABLE [dbo].[GroupFacts] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [ShortName] nvarchar(100)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] int  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'Patient'
CREATE TABLE [dbo].[Patient] (
    [PatientId] int IDENTITY(1,1) NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'PatientFacts'
CREATE TABLE [dbo].[PatientFacts] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [CardholderId] nvarchar(20)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [PersonCode] nvarchar(3)  NOT NULL,
    [PatientRelationshipCode] nvarchar(2)  NOT NULL,
    [Gender] char(1)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] int  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] int  NOT NULL
);
GO

-- Creating table 'PatientGroups'
CREATE TABLE [dbo].[PatientGroups] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] int  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] int  NOT NULL,
    [GroupId] int  NOT NULL
);
GO

-- Creating table 'VaDrugs'
CREATE TABLE [dbo].[VaDrugs] (
    [Ndc_1] nvarchar(5)  NOT NULL,
    [Ndc_2] nvarchar(4)  NOT NULL,
    [Ndc_3] nvarchar(2)  NOT NULL,
    [NdfNdc] nvarchar(11)  NOT NULL,
    [Upn] nvarchar(max)  NULL,
    [IDateNdc] datetime  NULL,
    [Trade] nvarchar(max)  NOT NULL,
    [VaProduct] nvarchar(max)  NOT NULL,
    [IDateVap] datetime  NULL,
    [ProductNu] nvarchar(max)  NOT NULL,
    [FeeDer] nvarchar(max)  NOT NULL,
    [Generic] nvarchar(max)  NOT NULL,
    [PkgSz] decimal(18,0)  NOT NULL,
    [PkgType] nvarchar(max)  NOT NULL,
    [VaClass] nvarchar(max)  NOT NULL,
    [Manufac] nvarchar(max)  NOT NULL,
    [StandardMedRoute] nvarchar(max)  NULL,
    [Strength] nvarchar(max)  NULL,
    [Units] nvarchar(max)  NULL,
    [DoseForm] nvarchar(max)  NOT NULL,
    [NfName] nvarchar(max)  NOT NULL,
    [Csfs] nvarchar(max)  NOT NULL,
    [RxOtc] nvarchar(max)  NOT NULL,
    [NfIndicat] nvarchar(max)  NOT NULL,
    [VaPrn] nvarchar(max)  NULL,
    [DispUnt] nvarchar(max)  NULL,
    [Id] nvarchar(max)  NULL,
    [Mark] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GroupId] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [RecordId] in table 'GroupFacts'
ALTER TABLE [dbo].[GroupFacts]
ADD CONSTRAINT [PK_GroupFacts]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [PatientId] in table 'Patient'
ALTER TABLE [dbo].[Patient]
ADD CONSTRAINT [PK_Patient]
    PRIMARY KEY CLUSTERED ([PatientId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientFacts'
ALTER TABLE [dbo].[PatientFacts]
ADD CONSTRAINT [PK_PatientFacts]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientGroups'
ALTER TABLE [dbo].[PatientGroups]
ADD CONSTRAINT [PK_PatientGroups]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [NdfNdc] in table 'VaDrugs'
ALTER TABLE [dbo].[VaDrugs]
ADD CONSTRAINT [PK_VaDrugs]
    PRIMARY KEY CLUSTERED ([NdfNdc] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GroupId] in table 'GroupFacts'
ALTER TABLE [dbo].[GroupFacts]
ADD CONSTRAINT [FK_FKGroupFacts397517]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FKGroupFacts397517'
CREATE INDEX [IX_FK_FKGroupFacts397517]
ON [dbo].[GroupFacts]
    ([GroupId]);
GO

-- Creating foreign key on [GroupId] in table 'PatientGroups'
ALTER TABLE [dbo].[PatientGroups]
ADD CONSTRAINT [FK_Group_PatientGroup_Rel]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Groups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Group_PatientGroup_Rel'
CREATE INDEX [IX_FK_Group_PatientGroup_Rel]
ON [dbo].[PatientGroups]
    ([GroupId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'GroupFacts'
ALTER TABLE [dbo].[GroupFacts]
ADD CONSTRAINT [FK_GroupFacts_GroupFacts_Rel]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[GroupFacts]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupFacts_GroupFacts_Rel'
CREATE INDEX [IX_FK_GroupFacts_GroupFacts_Rel]
ON [dbo].[GroupFacts]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientGroups'
ALTER TABLE [dbo].[PatientGroups]
ADD CONSTRAINT [FK_Patient_PateintFacts_Rel]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_PateintFacts_Rel'
CREATE INDEX [IX_FK_Patient_PateintFacts_Rel]
ON [dbo].[PatientGroups]
    ([PatientId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientFacts'
ALTER TABLE [dbo].[PatientFacts]
ADD CONSTRAINT [FK_Patient_PatientFacts_Rel]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_PatientFacts_Rel'
CREATE INDEX [IX_FK_Patient_PatientFacts_Rel]
ON [dbo].[PatientFacts]
    ([PatientId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'PatientFacts'
ALTER TABLE [dbo].[PatientFacts]
ADD CONSTRAINT [FK_PatientFacts_PatientFacts_Rel]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[PatientFacts]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientFacts_PatientFacts_Rel'
CREATE INDEX [IX_FK_PatientFacts_PatientFacts_Rel]
ON [dbo].[PatientFacts]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'PatientGroups'
ALTER TABLE [dbo].[PatientGroups]
ADD CONSTRAINT [FK_PatientGroup_PateintGroup_Rel]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[PatientGroups]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientGroup_PateintGroup_Rel'
CREATE INDEX [IX_FK_PatientGroup_PateintGroup_Rel]
ON [dbo].[PatientGroups]
    ([OriginalFactRecordId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------