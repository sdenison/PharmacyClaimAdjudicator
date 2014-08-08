-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/08/2014 15:17:42
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
    ALTER TABLE [dbo].[GroupFact] DROP CONSTRAINT [FK_FKGroupFacts397517];
GO
IF OBJECT_ID(N'[dbo].[FK_Group_PatientGroup_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroup] DROP CONSTRAINT [FK_Group_PatientGroup_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PateintFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroup] DROP CONSTRAINT [FK_Patient_PateintFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PatientFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientFact] DROP CONSTRAINT [FK_Patient_PatientFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanPlanFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanFact] DROP CONSTRAINT [FK_PlanPlanFact];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanGroupFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupFact] DROP CONSTRAINT [FK_PlanGroupFact];
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
IF OBJECT_ID(N'[dbo].[FK_PlanFactPlanRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanRule] DROP CONSTRAINT [FK_PlanFactPlanRules];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomAtomFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomFact] DROP CONSTRAINT [FK_AtomAtomFact];
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

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group];
GO
IF OBJECT_ID(N'[dbo].[GroupFact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupFact];
GO
IF OBJECT_ID(N'[dbo].[Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient];
GO
IF OBJECT_ID(N'[dbo].[PatientFact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientFact];
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
IF OBJECT_ID(N'[dbo].[PlanFact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanFact];
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
IF OBJECT_ID(N'[dbo].[AtomFact]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomFact];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Group'
CREATE TABLE [dbo].[Group] (
    [GroupId] nvarchar(20)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'GroupFact'
CREATE TABLE [dbo].[GroupFact] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] bigint  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [GroupId] nvarchar(20)  NOT NULL,
    [PlanId] nvarchar(20)  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Plan_PlanId] nvarchar(20)  NOT NULL
);
GO

-- Creating table 'Patient'
CREATE TABLE [dbo].[Patient] (
    [PatientId] bigint IDENTITY(1,1) NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'PatientFact'
CREATE TABLE [dbo].[PatientFact] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [CardholderId] nvarchar(20)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [PersonCode] nvarchar(3)  NOT NULL,
    [PatientRelationshipCode] nvarchar(2)  NOT NULL,
    [Gender] char(1)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] bigint  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] bigint  NOT NULL
);
GO

-- Creating table 'PatientGroup'
CREATE TABLE [dbo].[PatientGroup] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] bigint  NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL,
    [PatientId] bigint  NOT NULL,
    [GroupId] nvarchar(20)  NOT NULL
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
    [PlanId] nvarchar(20)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanFact'
CREATE TABLE [dbo].[PlanFact] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [PlanId] nvarchar(20)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] bigint  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Atom'
CREATE TABLE [dbo].[Atom] (
    [AtomId] bigint IDENTITY(1,1) NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'AtomGroup'
CREATE TABLE [dbo].[AtomGroup] (
    [AtomGroupId] bigint IDENTITY(1,1) NOT NULL,
    [LogicalOperator] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'AtomGroupItem'
CREATE TABLE [dbo].[AtomGroupItem] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [AtomGroupId] bigint  NOT NULL,
    [AtomId] bigint  NULL,
    [ContainedAtomGroupId] bigint  NULL,
    [Priority] int  NOT NULL
);
GO

-- Creating table 'Rule'
CREATE TABLE [dbo].[Rule] (
    [RuleId] bigint IDENTITY(1,1) NOT NULL,
    [RuleType] nvarchar(max)  NOT NULL,
    [DefaultValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanRule'
CREATE TABLE [dbo].[PlanRule] (
    [PlanRecordId] bigint  NOT NULL,
    [RuleId] bigint  NOT NULL,
    [Rule_RuleId] bigint  NOT NULL,
    [PlanFact_RecordId] bigint  NOT NULL
);
GO

-- Creating table 'Implication'
CREATE TABLE [dbo].[Implication] (
    [ImplicationId] bigint IDENTITY(1,1) NOT NULL,
    [AtomGroupId] bigint  NOT NULL,
    [DeductionAtomId] bigint  NULL,
    [Label] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RuleImplication'
CREATE TABLE [dbo].[RuleImplication] (
    [RecordId] bigint IDENTITY(1,1) NOT NULL,
    [RuleId] bigint  NOT NULL,
    [ImplicationId] bigint  NOT NULL,
    [Priority] nvarchar(max)  NOT NULL,
    [Rule_RuleId] bigint  NOT NULL,
    [Implication_ImplicationId] bigint  NOT NULL
);
GO

-- Creating table 'AtomFact'
CREATE TABLE [dbo].[AtomFact] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [AtomId] bigint  NOT NULL,
    [Class] nvarchar(50)  NOT NULL,
    [Property] nvarchar(50)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL,
    [Operation] nvarchar(max)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] int  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GroupId] in table 'Group'
ALTER TABLE [dbo].[Group]
ADD CONSTRAINT [PK_Group]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [RecordId] in table 'GroupFact'
ALTER TABLE [dbo].[GroupFact]
ADD CONSTRAINT [PK_GroupFact]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [PatientId] in table 'Patient'
ALTER TABLE [dbo].[Patient]
ADD CONSTRAINT [PK_Patient]
    PRIMARY KEY CLUSTERED ([PatientId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PatientFact'
ALTER TABLE [dbo].[PatientFact]
ADD CONSTRAINT [PK_PatientFact]
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

-- Creating primary key on [PlanId] in table 'Plan'
ALTER TABLE [dbo].[Plan]
ADD CONSTRAINT [PK_Plan]
    PRIMARY KEY CLUSTERED ([PlanId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PlanFact'
ALTER TABLE [dbo].[PlanFact]
ADD CONSTRAINT [PK_PlanFact]
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

-- Creating primary key on [PlanRecordId] in table 'PlanRule'
ALTER TABLE [dbo].[PlanRule]
ADD CONSTRAINT [PK_PlanRule]
    PRIMARY KEY CLUSTERED ([PlanRecordId] ASC);
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

-- Creating primary key on [RecordId] in table 'AtomFact'
ALTER TABLE [dbo].[AtomFact]
ADD CONSTRAINT [PK_AtomFact]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [GroupId] in table 'GroupFact'
ALTER TABLE [dbo].[GroupFact]
ADD CONSTRAINT [FK_FKGroupFacts397517]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Group]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FKGroupFacts397517'
CREATE INDEX [IX_FK_FKGroupFacts397517]
ON [dbo].[GroupFact]
    ([GroupId]);
GO

-- Creating foreign key on [GroupId] in table 'PatientGroup'
ALTER TABLE [dbo].[PatientGroup]
ADD CONSTRAINT [FK_Group_PatientGroup_Rel]
    FOREIGN KEY ([GroupId])
    REFERENCES [dbo].[Group]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Group_PatientGroup_Rel'
CREATE INDEX [IX_FK_Group_PatientGroup_Rel]
ON [dbo].[PatientGroup]
    ([GroupId]);
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

-- Creating foreign key on [PatientId] in table 'PatientFact'
ALTER TABLE [dbo].[PatientFact]
ADD CONSTRAINT [FK_Patient_PatientFacts_Rel]
    FOREIGN KEY ([PatientId])
    REFERENCES [dbo].[Patient]
        ([PatientId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Patient_PatientFacts_Rel'
CREATE INDEX [IX_FK_Patient_PatientFacts_Rel]
ON [dbo].[PatientFact]
    ([PatientId]);
GO

-- Creating foreign key on [PlanId] in table 'PlanFact'
ALTER TABLE [dbo].[PlanFact]
ADD CONSTRAINT [FK_PlanPlanFact]
    FOREIGN KEY ([PlanId])
    REFERENCES [dbo].[Plan]
        ([PlanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanPlanFact'
CREATE INDEX [IX_FK_PlanPlanFact]
ON [dbo].[PlanFact]
    ([PlanId]);
GO

-- Creating foreign key on [Plan_PlanId] in table 'GroupFact'
ALTER TABLE [dbo].[GroupFact]
ADD CONSTRAINT [FK_PlanGroupFact]
    FOREIGN KEY ([Plan_PlanId])
    REFERENCES [dbo].[Plan]
        ([PlanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanGroupFact'
CREATE INDEX [IX_FK_PlanGroupFact]
ON [dbo].[GroupFact]
    ([Plan_PlanId]);
GO

-- Creating foreign key on [Rule_RuleId] in table 'RuleImplication'
ALTER TABLE [dbo].[RuleImplication]
ADD CONSTRAINT [FK_RuleRuleImplication]
    FOREIGN KEY ([Rule_RuleId])
    REFERENCES [dbo].[Rule]
        ([RuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleRuleImplication'
CREATE INDEX [IX_FK_RuleRuleImplication]
ON [dbo].[RuleImplication]
    ([Rule_RuleId]);
GO

-- Creating foreign key on [Implication_ImplicationId] in table 'RuleImplication'
ALTER TABLE [dbo].[RuleImplication]
ADD CONSTRAINT [FK_ImplicationRuleImplication]
    FOREIGN KEY ([Implication_ImplicationId])
    REFERENCES [dbo].[Implication]
        ([ImplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ImplicationRuleImplication'
CREATE INDEX [IX_FK_ImplicationRuleImplication]
ON [dbo].[RuleImplication]
    ([Implication_ImplicationId]);
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

-- Creating foreign key on [PlanFact_RecordId] in table 'PlanRule'
ALTER TABLE [dbo].[PlanRule]
ADD CONSTRAINT [FK_PlanFactPlanRules]
    FOREIGN KEY ([PlanFact_RecordId])
    REFERENCES [dbo].[PlanFact]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanFactPlanRules'
CREATE INDEX [IX_FK_PlanFactPlanRules]
ON [dbo].[PlanRule]
    ([PlanFact_RecordId]);
GO

-- Creating foreign key on [AtomId] in table 'AtomFact'
ALTER TABLE [dbo].[AtomFact]
ADD CONSTRAINT [FK_AtomAtomFact]
    FOREIGN KEY ([AtomId])
    REFERENCES [dbo].[Atom]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomAtomFact'
CREATE INDEX [IX_FK_AtomAtomFact]
ON [dbo].[AtomFact]
    ([AtomId]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------