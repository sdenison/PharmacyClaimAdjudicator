
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/06/2014 15:09:03
-- Generated from EDMX file: C:\Users\sdenison\work\PharmacyClaimAdjudicator\src\PharmacyAdjudicator\PharmacyAdjudicator.DataAccess\PharmacyAdjFromDatabase.edmx
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
    ALTER TABLE [dbo].[GroupDetail] DROP CONSTRAINT [FK_FKGroupFacts397517];
GO
IF OBJECT_ID(N'[dbo].[FK_Group_PatientGroup_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroup] DROP CONSTRAINT [FK_Group_PatientGroup_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PateintFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroup] DROP CONSTRAINT [FK_Patient_PateintFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PatientFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientDetail] DROP CONSTRAINT [FK_Patient_PatientFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanPlanFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanDetail] DROP CONSTRAINT [FK_PlanPlanFact];
GO
IF OBJECT_ID(N'[dbo].[FK_RuleRuleImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleImplication] DROP CONSTRAINT [FK_RuleRuleImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_ImplicationRuleImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleImplication] DROP CONSTRAINT [FK_ImplicationRuleImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_RulePlanRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanRule] DROP CONSTRAINT [FK_RulePlanRules];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomGroupAtomGroupItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroupItem] DROP CONSTRAINT [FK_AtomGroupAtomGroupItems];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomAtomGroupItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroupItem] DROP CONSTRAINT [FK_AtomAtomGroupItems];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomGroupAtomGroupItems1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroupItem] DROP CONSTRAINT [FK_AtomGroupAtomGroupItems1];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomGroupImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Implication] DROP CONSTRAINT [FK_AtomGroupImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Implication] DROP CONSTRAINT [FK_AtomImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_AddressTypePatientAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientAddress] DROP CONSTRAINT [FK_AddressTypePatientAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_AddressPatientAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientAddress] DROP CONSTRAINT [FK_AddressPatientAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientPatientAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientAddress] DROP CONSTRAINT [FK_PatientPatientAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientAddressPatientAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientAddress] DROP CONSTRAINT [FK_PatientAddressPatientAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientClientDetails]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientDetail] DROP CONSTRAINT [FK_ClientClientDetails];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientClientGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientGroup] DROP CONSTRAINT [FK_ClientClientGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupClientGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientGroup] DROP CONSTRAINT [FK_GroupClientGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupGroupPlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupPlan] DROP CONSTRAINT [FK_GroupGroupPlan];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanGroupPlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupPlan] DROP CONSTRAINT [FK_PlanGroupPlan];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupDetailGroupDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupDetail] DROP CONSTRAINT [FK_GroupDetailGroupDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupPlanGroupPlan]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupPlan] DROP CONSTRAINT [FK_GroupPlanGroupPlan];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientGroupClientGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientGroup] DROP CONSTRAINT [FK_ClientGroupClientGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ClientDetailClientDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ClientDetail] DROP CONSTRAINT [FK_ClientDetailClientDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientDetailPatientDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientDetail] DROP CONSTRAINT [FK_PatientDetailPatientDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientGroupPatientGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroup] DROP CONSTRAINT [FK_PatientGroupPatientGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanPlanRule]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanRule] DROP CONSTRAINT [FK_PlanPlanRule];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group];
GO
IF OBJECT_ID(N'[dbo].[GroupDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupDetail];
GO
IF OBJECT_ID(N'[dbo].[Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient];
GO
IF OBJECT_ID(N'[dbo].[PatientDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientDetail];
GO
IF OBJECT_ID(N'[dbo].[PatientGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientGroup];
GO
--IF OBJECT_ID(N'[dbo].[VaDrug]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[VaDrug];
--GO
IF OBJECT_ID(N'[dbo].[Plan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plan];
GO
IF OBJECT_ID(N'[dbo].[PlanDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanDetail];
GO
IF OBJECT_ID(N'[dbo].[Atom]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Atom];
GO
IF OBJECT_ID(N'[dbo].[AtomGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomGroup];
GO
IF OBJECT_ID(N'[dbo].[AtomGroupItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomGroupItem];
GO
IF OBJECT_ID(N'[dbo].[Rule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rule];
GO
IF OBJECT_ID(N'[dbo].[PlanRule]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanRule];
GO
IF OBJECT_ID(N'[dbo].[Implication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Implication];
GO
IF OBJECT_ID(N'[dbo].[RuleImplication]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RuleImplication];
GO
IF OBJECT_ID(N'[dbo].[Address]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Address];
GO
IF OBJECT_ID(N'[dbo].[PatientAddress]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientAddress];
GO
IF OBJECT_ID(N'[dbo].[AddressType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AddressType];
GO
IF OBJECT_ID(N'[dbo].[Client]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Client];
GO
IF OBJECT_ID(N'[dbo].[ClientDetail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientDetail];
GO
IF OBJECT_ID(N'[dbo].[ClientGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ClientGroup];
GO
IF OBJECT_ID(N'[dbo].[GroupPlan]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupPlan];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Group'
CREATE TABLE [dbo].[Group] (
    [GroupInternalId] uniqueidentifier  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'GroupDetail'
CREATE TABLE [dbo].[GroupDetail] (
    [RecordId] uniqueidentifier  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [GroupId] nvarchar(max)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [GroupInternalId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Patient'
CREATE TABLE [dbo].[Patient] (
    [PatientId] bigint IDENTITY(1,1) NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'PatientDetail'
CREATE TABLE [dbo].[PatientDetail] (
    [RecordId] uniqueidentifier  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [CardholderId] nvarchar(20)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [PersonCode] nvarchar(3)  NOT NULL,
    [PatientRelationshipCode] nvarchar(2)  NOT NULL,
    [Gender] char(1)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] bigint  NOT NULL
);
GO

-- Creating table 'PatientGroup'
CREATE TABLE [dbo].[PatientGroup] (
    [RecordId] uniqueidentifier  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] bigint  NOT NULL,
    [GroupInternalId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'VaDrug'
--CREATE TABLE [dbo].[VaDrug] (
--    [Ndc_1] nvarchar(5)  NOT NULL,
--    [Ndc_2] nvarchar(4)  NOT NULL,
--    [Ndc_3] nvarchar(2)  NOT NULL,
--    [NdfNdc] nvarchar(11)  NOT NULL,
--    [Upn] nvarchar(max)  NULL,
--    [IDateNdc] datetime  NULL,
--    [Trade] nvarchar(max)  NOT NULL,
--    [VaProduct] nvarchar(max)  NOT NULL,
--    [IDateVap] datetime  NULL,
--    [ProductNu] nvarchar(max)  NULL,
--    [FeeDer] nvarchar(max)  NOT NULL,
--    [Generic] nvarchar(max)  NOT NULL,
--    [PkgSz] decimal(18,0)  NOT NULL,
--    [PkgType] nvarchar(max)  NOT NULL,
--    [VaClass] nvarchar(max)  NOT NULL,
--    [Manufac] nvarchar(max)  NOT NULL,
--    [StandardMedRoute] nvarchar(max)  NULL,
--    [Strength] nvarchar(max)  NULL,
--    [Units] nvarchar(max)  NULL,
--    [DoseForm] nvarchar(max)  NOT NULL,
--    [NfName] nvarchar(max)  NOT NULL,
--    [Csfs] nvarchar(max)  NOT NULL,
--    [RxOtc] nvarchar(max)  NOT NULL,
--    [NfIndicat] nvarchar(max)  NOT NULL,
--    [VaPrn] nvarchar(max)  NULL,
--    [DispUnt] nvarchar(max)  NULL,
--    [Id] nvarchar(max)  NULL,
--    [Mark] nvarchar(max)  NOT NULL
--);
--GO

-- Creating table 'Plan'
CREATE TABLE [dbo].[Plan] (
    [PlanInternalId] uniqueidentifier  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanDetail'
CREATE TABLE [dbo].[PlanDetail] (
    [RecordId] uniqueidentifier  NOT NULL,
    [PlanId] nvarchar(30)  NOT NULL,
    [Name] nvarchar(255)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] bigint  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PlanInternalId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Atom'
CREATE TABLE [dbo].[Atom] (
    [AtomId] uniqueidentifier  NOT NULL,
    [Class] nvarchar(50)  NOT NULL,
    [Property] nvarchar(50)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Operation] nvarchar(max)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(50)  NOT NULL,
    [RecordDeleteDate] datetime  NULL,
    [RecordDeleteUser] nvarchar(50)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'AtomGroup'
CREATE TABLE [dbo].[AtomGroup] (
    [AtomGroupId] uniqueidentifier  NOT NULL,
    [LogicalOperator] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AtomGroupItem'
CREATE TABLE [dbo].[AtomGroupItem] (
    [RecordId] uniqueidentifier  NOT NULL,
    [AtomGroupId] uniqueidentifier  NOT NULL,
    [AtomId] uniqueidentifier  NULL,
    [ContainedAtomGroupId] uniqueidentifier  NULL,
    [Priority] int  NOT NULL
);
GO

-- Creating table 'Rule'
CREATE TABLE [dbo].[Rule] (
    [RuleId] uniqueidentifier  NOT NULL,
    [RuleType] nvarchar(max)  NOT NULL,
    [DefaultValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanRule'
CREATE TABLE [dbo].[PlanRule] (
    [RecordId] uniqueidentifier  NOT NULL,
    [RuleId] uniqueidentifier  NOT NULL,
    [PlanInternalId] uniqueidentifier  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] nvarchar(max)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [Rule_RuleId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Implication'
CREATE TABLE [dbo].[Implication] (
    [ImplicationId] uniqueidentifier  NOT NULL,
    [AtomGroupId] uniqueidentifier  NOT NULL,
    [DeductionAtomId] uniqueidentifier  NOT NULL,
    [Label] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RuleImplication'
CREATE TABLE [dbo].[RuleImplication] (
    [RecordId] uniqueidentifier  NOT NULL,
    [RuleId] uniqueidentifier  NOT NULL,
    [ImplicationId] uniqueidentifier  NOT NULL,
    [Priority] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Address'
CREATE TABLE [dbo].[Address] (
    [AddressId] uniqueidentifier  NOT NULL,
    [Address1] nvarchar(max)  NOT NULL,
    [Address2] nvarchar(max)  NOT NULL,
    [Address3] nvarchar(max)  NOT NULL,
    [City] nvarchar(max)  NOT NULL,
    [State] nvarchar(max)  NOT NULL,
    [Zip] nvarchar(max)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL,
    [Latitude] decimal(18,0)  NULL,
    [Longitude] decimal(18,0)  NULL
);
GO

-- Creating table 'PatientAddress'
CREATE TABLE [dbo].[PatientAddress] (
    [RecordId] uniqueidentifier  NOT NULL,
    [PatientId] bigint  NOT NULL,
    [AddressTypeCode] nvarchar(20)  NOT NULL,
    [AddressId] uniqueidentifier  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL,
    [Slot] int  NOT NULL
);
GO

-- Creating table 'AddressType'
CREATE TABLE [dbo].[AddressType] (
    [AddressTypeCode] nvarchar(20)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Client'
CREATE TABLE [dbo].[Client] (
    [ClientInternalId] uniqueidentifier  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'ClientDetail'
CREATE TABLE [dbo].[ClientDetail] (
    [RecordId] uniqueidentifier  NOT NULL,
    [ClientId] nvarchar(20)  NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL,
    [ClientInternalId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'ClientGroup'
CREATE TABLE [dbo].[ClientGroup] (
    [RecordId] uniqueidentifier  NOT NULL,
    [ClientInternalId] uniqueidentifier  NOT NULL,
    [GroupInternalId] uniqueidentifier  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(29)  NOT NULL
);
GO

-- Creating table 'GroupPlan'
CREATE TABLE [dbo].[GroupPlan] (
    [RecordId] uniqueidentifier  NOT NULL,
    [GroupInternalId] uniqueidentifier  NOT NULL,
    [PlanInternalId] uniqueidentifier  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] uniqueidentifier  NULL,
    [RecordCreateDateTime] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GroupInternalId] in table 'Group'
ALTER TABLE [dbo].[Group]
ADD CONSTRAINT [PK_Group]
    PRIMARY KEY CLUSTERED ([GroupInternalId] ASC);
GO

-- Creating primary key on [RecordId] in table 'GroupDetail'
ALTER TABLE [dbo].[GroupDetail]
ADD CONSTRAINT [PK_GroupDetail]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [PatientId] in table 'Patient'
ALTER TABLE [dbo].[Patient]
ADD CONSTRAINT [PK_Patient]
    PRIMARY KEY CLUSTERED ([PatientId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientDetail'
ALTER TABLE [dbo].[PatientDetail]
ADD CONSTRAINT [PK_PatientDetail]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientGroup'
ALTER TABLE [dbo].[PatientGroup]
ADD CONSTRAINT [PK_PatientGroup]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [NdfNdc] in table 'VaDrug'
--ALTER TABLE [dbo].[VaDrug]
--ADD CONSTRAINT [PK_VaDrug]
--    PRIMARY KEY CLUSTERED ([NdfNdc] ASC);
--GO

-- Creating primary key on [PlanInternalId] in table 'Plan'
ALTER TABLE [dbo].[Plan]
ADD CONSTRAINT [PK_Plan]
    PRIMARY KEY CLUSTERED ([PlanInternalId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PlanDetail'
ALTER TABLE [dbo].[PlanDetail]
ADD CONSTRAINT [PK_PlanDetail]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [AtomId] in table 'Atom'
ALTER TABLE [dbo].[Atom]
ADD CONSTRAINT [PK_Atom]
    PRIMARY KEY CLUSTERED ([AtomId] ASC);
GO

-- Creating primary key on [AtomGroupId] in table 'AtomGroup'
ALTER TABLE [dbo].[AtomGroup]
ADD CONSTRAINT [PK_AtomGroup]
    PRIMARY KEY CLUSTERED ([AtomGroupId] ASC);
GO

-- Creating primary key on [RecordId] in table 'AtomGroupItem'
ALTER TABLE [dbo].[AtomGroupItem]
ADD CONSTRAINT [PK_AtomGroupItem]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RuleId] in table 'Rule'
ALTER TABLE [dbo].[Rule]
ADD CONSTRAINT [PK_Rule]
    PRIMARY KEY CLUSTERED ([RuleId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PlanRule'
ALTER TABLE [dbo].[PlanRule]
ADD CONSTRAINT [PK_PlanRule]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [ImplicationId] in table 'Implication'
ALTER TABLE [dbo].[Implication]
ADD CONSTRAINT [PK_Implication]
    PRIMARY KEY CLUSTERED ([ImplicationId] ASC);
GO

-- Creating primary key on [RecordId] in table 'RuleImplication'
ALTER TABLE [dbo].[RuleImplication]
ADD CONSTRAINT [PK_RuleImplication]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [AddressId] in table 'Address'
ALTER TABLE [dbo].[Address]
ADD CONSTRAINT [PK_Address]
    PRIMARY KEY CLUSTERED ([AddressId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientAddress'
ALTER TABLE [dbo].[PatientAddress]
ADD CONSTRAINT [PK_PatientAddress]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [AddressTypeCode] in table 'AddressType'
ALTER TABLE [dbo].[AddressType]
ADD CONSTRAINT [PK_AddressType]
    PRIMARY KEY CLUSTERED ([AddressTypeCode] ASC);
GO

-- Creating primary key on [ClientInternalId] in table 'Client'
ALTER TABLE [dbo].[Client]
ADD CONSTRAINT [PK_Client]
    PRIMARY KEY CLUSTERED ([ClientInternalId] ASC);
GO

-- Creating primary key on [RecordId] in table 'ClientDetail'
ALTER TABLE [dbo].[ClientDetail]
ADD CONSTRAINT [PK_ClientDetail]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RecordId] in table 'ClientGroup'
ALTER TABLE [dbo].[ClientGroup]
ADD CONSTRAINT [PK_ClientGroup]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RecordId] in table 'GroupPlan'
ALTER TABLE [dbo].[GroupPlan]
ADD CONSTRAINT [PK_GroupPlan]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GroupInternalId] in table 'GroupDetail'
ALTER TABLE [dbo].[GroupDetail]
ADD CONSTRAINT [FK_FKGroupFacts397517]
    FOREIGN KEY ([GroupInternalId])
    REFERENCES [dbo].[Group]
        ([GroupInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FKGroupFacts397517'
CREATE INDEX [IX_FK_FKGroupFacts397517]
ON [dbo].[GroupDetail]
    ([GroupInternalId]);
GO

-- Creating foreign key on [GroupInternalId] in table 'PatientGroup'
ALTER TABLE [dbo].[PatientGroup]
ADD CONSTRAINT [FK_Group_PatientGroup_Rel]
    FOREIGN KEY ([GroupInternalId])
    REFERENCES [dbo].[Group]
        ([GroupInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Group_PatientGroup_Rel'
CREATE INDEX [IX_FK_Group_PatientGroup_Rel]
ON [dbo].[PatientGroup]
    ([GroupInternalId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientGroup'
ALTER TABLE [dbo].[PatientGroup]
ADD CONSTRAINT [FK_Patient_PateintFacts_Rel]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_PateintFacts_Rel'
CREATE INDEX [IX_FK_Patient_PateintFacts_Rel]
ON [dbo].[PatientGroup]
    ([PatientId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientDetail'
ALTER TABLE [dbo].[PatientDetail]
ADD CONSTRAINT [FK_Patient_PatientFacts_Rel]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_PatientFacts_Rel'
CREATE INDEX [IX_FK_Patient_PatientFacts_Rel]
ON [dbo].[PatientDetail]
    ([PatientId]);
GO

-- Creating foreign key on [PlanInternalId] in table 'PlanDetail'
ALTER TABLE [dbo].[PlanDetail]
ADD CONSTRAINT [FK_PlanPlanFact]
    FOREIGN KEY ([PlanInternalId])
    REFERENCES [dbo].[Plan]
        ([PlanInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanPlanFact'
CREATE INDEX [IX_FK_PlanPlanFact]
ON [dbo].[PlanDetail]
    ([PlanInternalId]);
GO

-- Creating foreign key on [RuleId] in table 'RuleImplication'
ALTER TABLE [dbo].[RuleImplication]
ADD CONSTRAINT [FK_RuleRuleImplication]
    FOREIGN KEY ([RuleId])
    REFERENCES [dbo].[Rule]
        ([RuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleRuleImplication'
CREATE INDEX [IX_FK_RuleRuleImplication]
ON [dbo].[RuleImplication]
    ([RuleId]);
GO

-- Creating foreign key on [ImplicationId] in table 'RuleImplication'
ALTER TABLE [dbo].[RuleImplication]
ADD CONSTRAINT [FK_ImplicationRuleImplication]
    FOREIGN KEY ([ImplicationId])
    REFERENCES [dbo].[Implication]
        ([ImplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImplicationRuleImplication'
CREATE INDEX [IX_FK_ImplicationRuleImplication]
ON [dbo].[RuleImplication]
    ([ImplicationId]);
GO

-- Creating foreign key on [Rule_RuleId] in table 'PlanRule'
ALTER TABLE [dbo].[PlanRule]
ADD CONSTRAINT [FK_RulePlanRules]
    FOREIGN KEY ([Rule_RuleId])
    REFERENCES [dbo].[Rule]
        ([RuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RulePlanRules'
CREATE INDEX [IX_FK_RulePlanRules]
ON [dbo].[PlanRule]
    ([Rule_RuleId]);
GO

-- Creating foreign key on [AtomGroupId] in table 'AtomGroupItem'
ALTER TABLE [dbo].[AtomGroupItem]
ADD CONSTRAINT [FK_AtomGroupAtomGroupItems]
    FOREIGN KEY ([AtomGroupId])
    REFERENCES [dbo].[AtomGroup]
        ([AtomGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomGroupAtomGroupItems'
CREATE INDEX [IX_FK_AtomGroupAtomGroupItems]
ON [dbo].[AtomGroupItem]
    ([AtomGroupId]);
GO

-- Creating foreign key on [AtomId] in table 'AtomGroupItem'
ALTER TABLE [dbo].[AtomGroupItem]
ADD CONSTRAINT [FK_AtomAtomGroupItems]
    FOREIGN KEY ([AtomId])
    REFERENCES [dbo].[Atom]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomAtomGroupItems'
CREATE INDEX [IX_FK_AtomAtomGroupItems]
ON [dbo].[AtomGroupItem]
    ([AtomId]);
GO

-- Creating foreign key on [ContainedAtomGroupId] in table 'AtomGroupItem'
ALTER TABLE [dbo].[AtomGroupItem]
ADD CONSTRAINT [FK_AtomGroupAtomGroupItems1]
    FOREIGN KEY ([ContainedAtomGroupId])
    REFERENCES [dbo].[AtomGroup]
        ([AtomGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomGroupAtomGroupItems1'
CREATE INDEX [IX_FK_AtomGroupAtomGroupItems1]
ON [dbo].[AtomGroupItem]
    ([ContainedAtomGroupId]);
GO

-- Creating foreign key on [AtomGroupId] in table 'Implication'
ALTER TABLE [dbo].[Implication]
ADD CONSTRAINT [FK_AtomGroupImplication]
    FOREIGN KEY ([AtomGroupId])
    REFERENCES [dbo].[AtomGroup]
        ([AtomGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomGroupImplication'
CREATE INDEX [IX_FK_AtomGroupImplication]
ON [dbo].[Implication]
    ([AtomGroupId]);
GO

-- Creating foreign key on [DeductionAtomId] in table 'Implication'
ALTER TABLE [dbo].[Implication]
ADD CONSTRAINT [FK_AtomImplication]
    FOREIGN KEY ([DeductionAtomId])
    REFERENCES [dbo].[Atom]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomImplication'
CREATE INDEX [IX_FK_AtomImplication]
ON [dbo].[Implication]
    ([DeductionAtomId]);
GO

-- Creating foreign key on [AddressTypeCode] in table 'PatientAddress'
ALTER TABLE [dbo].[PatientAddress]
ADD CONSTRAINT [FK_AddressTypePatientAddress]
    FOREIGN KEY ([AddressTypeCode])
    REFERENCES [dbo].[AddressType]
        ([AddressTypeCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressTypePatientAddress'
CREATE INDEX [IX_FK_AddressTypePatientAddress]
ON [dbo].[PatientAddress]
    ([AddressTypeCode]);
GO

-- Creating foreign key on [AddressId] in table 'PatientAddress'
ALTER TABLE [dbo].[PatientAddress]
ADD CONSTRAINT [FK_AddressPatientAddress]
    FOREIGN KEY ([AddressId])
    REFERENCES [dbo].[Address]
        ([AddressId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AddressPatientAddress'
CREATE INDEX [IX_FK_AddressPatientAddress]
ON [dbo].[PatientAddress]
    ([AddressId]);
GO

-- Creating foreign key on [PatientId] in table 'PatientAddress'
ALTER TABLE [dbo].[PatientAddress]
ADD CONSTRAINT [FK_PatientPatientAddress]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientPatientAddress'
CREATE INDEX [IX_FK_PatientPatientAddress]
ON [dbo].[PatientAddress]
    ([PatientId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'PatientAddress'
ALTER TABLE [dbo].[PatientAddress]
ADD CONSTRAINT [FK_PatientAddressPatientAddress]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[PatientAddress]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientAddressPatientAddress'
CREATE INDEX [IX_FK_PatientAddressPatientAddress]
ON [dbo].[PatientAddress]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [ClientInternalId] in table 'ClientDetail'
ALTER TABLE [dbo].[ClientDetail]
ADD CONSTRAINT [FK_ClientClientDetails]
    FOREIGN KEY ([ClientInternalId])
    REFERENCES [dbo].[Client]
        ([ClientInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientClientDetails'
CREATE INDEX [IX_FK_ClientClientDetails]
ON [dbo].[ClientDetail]
    ([ClientInternalId]);
GO

-- Creating foreign key on [ClientInternalId] in table 'ClientGroup'
ALTER TABLE [dbo].[ClientGroup]
ADD CONSTRAINT [FK_ClientClientGroup]
    FOREIGN KEY ([ClientInternalId])
    REFERENCES [dbo].[Client]
        ([ClientInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientClientGroup'
CREATE INDEX [IX_FK_ClientClientGroup]
ON [dbo].[ClientGroup]
    ([ClientInternalId]);
GO

-- Creating foreign key on [GroupInternalId] in table 'ClientGroup'
ALTER TABLE [dbo].[ClientGroup]
ADD CONSTRAINT [FK_GroupClientGroup]
    FOREIGN KEY ([GroupInternalId])
    REFERENCES [dbo].[Group]
        ([GroupInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupClientGroup'
CREATE INDEX [IX_FK_GroupClientGroup]
ON [dbo].[ClientGroup]
    ([GroupInternalId]);
GO

-- Creating foreign key on [GroupInternalId] in table 'GroupPlan'
ALTER TABLE [dbo].[GroupPlan]
ADD CONSTRAINT [FK_GroupGroupPlan]
    FOREIGN KEY ([GroupInternalId])
    REFERENCES [dbo].[Group]
        ([GroupInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupGroupPlan'
CREATE INDEX [IX_FK_GroupGroupPlan]
ON [dbo].[GroupPlan]
    ([GroupInternalId]);
GO

-- Creating foreign key on [PlanInternalId] in table 'GroupPlan'
ALTER TABLE [dbo].[GroupPlan]
ADD CONSTRAINT [FK_PlanGroupPlan]
    FOREIGN KEY ([PlanInternalId])
    REFERENCES [dbo].[Plan]
        ([PlanInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanGroupPlan'
CREATE INDEX [IX_FK_PlanGroupPlan]
ON [dbo].[GroupPlan]
    ([PlanInternalId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'GroupDetail'
ALTER TABLE [dbo].[GroupDetail]
ADD CONSTRAINT [FK_GroupDetailGroupDetail]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[GroupDetail]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupDetailGroupDetail'
CREATE INDEX [IX_FK_GroupDetailGroupDetail]
ON [dbo].[GroupDetail]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'GroupPlan'
ALTER TABLE [dbo].[GroupPlan]
ADD CONSTRAINT [FK_GroupPlanGroupPlan]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[GroupPlan]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupPlanGroupPlan'
CREATE INDEX [IX_FK_GroupPlanGroupPlan]
ON [dbo].[GroupPlan]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'ClientGroup'
ALTER TABLE [dbo].[ClientGroup]
ADD CONSTRAINT [FK_ClientGroupClientGroup]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[ClientGroup]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientGroupClientGroup'
CREATE INDEX [IX_FK_ClientGroupClientGroup]
ON [dbo].[ClientGroup]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'ClientDetail'
ALTER TABLE [dbo].[ClientDetail]
ADD CONSTRAINT [FK_ClientDetailClientDetail]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[ClientDetail]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ClientDetailClientDetail'
CREATE INDEX [IX_FK_ClientDetailClientDetail]
ON [dbo].[ClientDetail]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'PatientDetail'
ALTER TABLE [dbo].[PatientDetail]
ADD CONSTRAINT [FK_PatientDetailPatientDetail]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[PatientDetail]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientDetailPatientDetail'
CREATE INDEX [IX_FK_PatientDetailPatientDetail]
ON [dbo].[PatientDetail]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [OriginalFactRecordId] in table 'PatientGroup'
ALTER TABLE [dbo].[PatientGroup]
ADD CONSTRAINT [FK_PatientGroupPatientGroup]
    FOREIGN KEY ([OriginalFactRecordId])
    REFERENCES [dbo].[PatientGroup]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientGroupPatientGroup'
CREATE INDEX [IX_FK_PatientGroupPatientGroup]
ON [dbo].[PatientGroup]
    ([OriginalFactRecordId]);
GO

-- Creating foreign key on [PlanInternalId] in table 'PlanRule'
ALTER TABLE [dbo].[PlanRule]
ADD CONSTRAINT [FK_PlanPlanRule]
    FOREIGN KEY ([PlanInternalId])
    REFERENCES [dbo].[Plan]
        ([PlanInternalId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanPlanRule'
CREATE INDEX [IX_FK_PlanPlanRule]
ON [dbo].[PlanRule]
    ([PlanInternalId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------