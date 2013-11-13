
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/13/2013 00:01:46
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
IF OBJECT_ID(N'[dbo].[FK_Patient_PateintFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientGroups] DROP CONSTRAINT [FK_Patient_PateintFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_Patient_PatientFacts_Rel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientFacts] DROP CONSTRAINT [FK_Patient_PatientFacts_Rel];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanPlanFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanFacts] DROP CONSTRAINT [FK_PlanPlanFact];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanGroupFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GroupFacts] DROP CONSTRAINT [FK_PlanGroupFact];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomGroupAtomGroupItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroupItems] DROP CONSTRAINT [FK_AtomGroupAtomGroupItems];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomAtomGroupItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroupItems] DROP CONSTRAINT [FK_AtomAtomGroupItems];
GO
IF OBJECT_ID(N'[dbo].[FK_ImplicationAtomGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomGroups] DROP CONSTRAINT [FK_ImplicationAtomGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_RuleRuleImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleImplications] DROP CONSTRAINT [FK_RuleRuleImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_ImplicationRuleImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RuleImplications] DROP CONSTRAINT [FK_ImplicationRuleImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_RulePlanRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanRules] DROP CONSTRAINT [FK_RulePlanRules];
GO
IF OBJECT_ID(N'[dbo].[FK_PlanFactPlanRules]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PlanRules] DROP CONSTRAINT [FK_PlanFactPlanRules];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomImplication]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Implications] DROP CONSTRAINT [FK_AtomImplication];
GO
IF OBJECT_ID(N'[dbo].[FK_AtomAtomFact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AtomFacts] DROP CONSTRAINT [FK_AtomAtomFact];
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
IF OBJECT_ID(N'[dbo].[Patient]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patient];
GO
IF OBJECT_ID(N'[dbo].[PatientFacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientFacts];
GO
IF OBJECT_ID(N'[dbo].[PatientGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientGroups];
GO
--IF OBJECT_ID(N'[dbo].[VaDrugs]', 'U') IS NOT NULL
--    DROP TABLE [dbo].[VaDrugs];
--GO
IF OBJECT_ID(N'[dbo].[Plans]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Plans];
GO
IF OBJECT_ID(N'[dbo].[PlanFacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanFacts];
GO
IF OBJECT_ID(N'[dbo].[Atoms]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Atoms];
GO
IF OBJECT_ID(N'[dbo].[AtomGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomGroups];
GO
IF OBJECT_ID(N'[dbo].[AtomGroupItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomGroupItems];
GO
IF OBJECT_ID(N'[dbo].[Rules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rules];
GO
IF OBJECT_ID(N'[dbo].[PlanRules]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PlanRules];
GO
IF OBJECT_ID(N'[dbo].[Implications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Implications];
GO
IF OBJECT_ID(N'[dbo].[RuleImplications]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RuleImplications];
GO
IF OBJECT_ID(N'[dbo].[AtomFacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AtomFacts];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
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
    [GroupId] int  NOT NULL,
    [PlanId] nvarchar(20)  NOT NULL,
    [EffectiveDate] datetime  NOT NULL,
    [ExpirationDate] datetime  NOT NULL,
    [Plan_PlanId] nvarchar(20)  NOT NULL
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

-- Creating table 'Plans'
CREATE TABLE [dbo].[Plans] (
    [PlanId] nvarchar(20)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanFacts'
CREATE TABLE [dbo].[PlanFacts] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [PlanId] nvarchar(20)  NOT NULL,
    [Retraction] bit  NOT NULL,
    [OriginalFactRecordId] int  NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Atoms'
CREATE TABLE [dbo].[Atoms] (
    [AtomId] int IDENTITY(1,1) NOT NULL,
    [RecordCreatedDateTime] datetime  NOT NULL,
    [RecordCreatedUser] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'AtomGroups'
CREATE TABLE [dbo].[AtomGroups] (
    [AtomGroupId] int IDENTITY(1,1) NOT NULL,
    [LogicalOperator] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Implication_ImplicationId] int  NOT NULL
);
GO

-- Creating table 'AtomGroupItems'
CREATE TABLE [dbo].[AtomGroupItems] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [AtomGroupId] nvarchar(max)  NOT NULL,
    [AtomId] nvarchar(max)  NOT NULL,
    [ContainedAtomGroupId] int  NOT NULL,
    [AtomGroup_AtomGroupId] int  NULL,
    [Atom_AtomId] int  NULL
);
GO

-- Creating table 'Rules'
CREATE TABLE [dbo].[Rules] (
    [RuleId] int IDENTITY(1,1) NOT NULL,
    [RuleType] nvarchar(max)  NOT NULL,
    [DefaultValue] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PlanRules'
CREATE TABLE [dbo].[PlanRules] (
    [PlanRecordId] int  NOT NULL,
    [RuleId] nvarchar(max)  NOT NULL,
    [Rule_RuleId] int  NOT NULL,
    [PlanFact_RecordId] int  NOT NULL
);
GO

-- Creating table 'Implications'
CREATE TABLE [dbo].[Implications] (
    [ImplicationId] int IDENTITY(1,1) NOT NULL,
    [AtomGroupId] nvarchar(max)  NOT NULL,
    [DeductionAtomId] int  NOT NULL,
    [AtomImplication_Implication_AtomId] int  NULL
);
GO

-- Creating table 'RuleImplications'
CREATE TABLE [dbo].[RuleImplications] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [RuleId] nvarchar(max)  NOT NULL,
    [ImplicationId] nvarchar(max)  NOT NULL,
    [Priority] nvarchar(max)  NOT NULL,
    [Rule_RuleId] int  NOT NULL,
    [Implication_ImplicationId] int  NOT NULL
);
GO

-- Creating table 'AtomFacts'
CREATE TABLE [dbo].[AtomFacts] (
    [RecordId] int IDENTITY(1,1) NOT NULL,
    [AtomId] int  NOT NULL,
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

-- Creating primary key on [PlanId] in table 'Plans'
ALTER TABLE [dbo].[Plans]
ADD CONSTRAINT [PK_Plans]
    PRIMARY KEY CLUSTERED ([PlanId] ASC);
GO

-- Creating primary key on [RecordId] in table 'PlanFacts'
ALTER TABLE [dbo].[PlanFacts]
ADD CONSTRAINT [PK_PlanFacts]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [AtomId] in table 'Atoms'
ALTER TABLE [dbo].[Atoms]
ADD CONSTRAINT [PK_Atoms]
    PRIMARY KEY CLUSTERED ([AtomId] ASC);
GO

-- Creating primary key on [AtomGroupId] in table 'AtomGroups'
ALTER TABLE [dbo].[AtomGroups]
ADD CONSTRAINT [PK_AtomGroups]
    PRIMARY KEY CLUSTERED ([AtomGroupId] ASC);
GO

-- Creating primary key on [RecordId] in table 'AtomGroupItems'
ALTER TABLE [dbo].[AtomGroupItems]
ADD CONSTRAINT [PK_AtomGroupItems]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RuleId] in table 'Rules'
ALTER TABLE [dbo].[Rules]
ADD CONSTRAINT [PK_Rules]
    PRIMARY KEY CLUSTERED ([RuleId] ASC);
GO

-- Creating primary key on [PlanRecordId] in table 'PlanRules'
ALTER TABLE [dbo].[PlanRules]
ADD CONSTRAINT [PK_PlanRules]
    PRIMARY KEY CLUSTERED ([PlanRecordId] ASC);
GO

-- Creating primary key on [ImplicationId] in table 'Implications'
ALTER TABLE [dbo].[Implications]
ADD CONSTRAINT [PK_Implications]
    PRIMARY KEY CLUSTERED ([ImplicationId] ASC);
GO

-- Creating primary key on [RecordId] in table 'RuleImplications'
ALTER TABLE [dbo].[RuleImplications]
ADD CONSTRAINT [PK_RuleImplications]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
GO

-- Creating primary key on [RecordId] in table 'AtomFacts'
ALTER TABLE [dbo].[AtomFacts]
ADD CONSTRAINT [PK_AtomFacts]
    PRIMARY KEY CLUSTERED ([RecordId] ASC);
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

-- Creating foreign key on [PlanId] in table 'PlanFacts'
ALTER TABLE [dbo].[PlanFacts]
ADD CONSTRAINT [FK_PlanPlanFact]
    FOREIGN KEY ([PlanId])
    REFERENCES [dbo].[Plans]
        ([PlanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanPlanFact'
CREATE INDEX [IX_FK_PlanPlanFact]
ON [dbo].[PlanFacts]
    ([PlanId]);
GO

-- Creating foreign key on [Plan_PlanId] in table 'GroupFacts'
ALTER TABLE [dbo].[GroupFacts]
ADD CONSTRAINT [FK_PlanGroupFact]
    FOREIGN KEY ([Plan_PlanId])
    REFERENCES [dbo].[Plans]
        ([PlanId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanGroupFact'
CREATE INDEX [IX_FK_PlanGroupFact]
ON [dbo].[GroupFacts]
    ([Plan_PlanId]);
GO

-- Creating foreign key on [AtomGroup_AtomGroupId] in table 'AtomGroupItems'
ALTER TABLE [dbo].[AtomGroupItems]
ADD CONSTRAINT [FK_AtomGroupAtomGroupItems]
    FOREIGN KEY ([AtomGroup_AtomGroupId])
    REFERENCES [dbo].[AtomGroups]
        ([AtomGroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomGroupAtomGroupItems'
CREATE INDEX [IX_FK_AtomGroupAtomGroupItems]
ON [dbo].[AtomGroupItems]
    ([AtomGroup_AtomGroupId]);
GO

-- Creating foreign key on [Atom_AtomId] in table 'AtomGroupItems'
ALTER TABLE [dbo].[AtomGroupItems]
ADD CONSTRAINT [FK_AtomAtomGroupItems]
    FOREIGN KEY ([Atom_AtomId])
    REFERENCES [dbo].[Atoms]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomAtomGroupItems'
CREATE INDEX [IX_FK_AtomAtomGroupItems]
ON [dbo].[AtomGroupItems]
    ([Atom_AtomId]);
GO

-- Creating foreign key on [Implication_ImplicationId] in table 'AtomGroups'
ALTER TABLE [dbo].[AtomGroups]
ADD CONSTRAINT [FK_ImplicationAtomGroup]
    FOREIGN KEY ([Implication_ImplicationId])
    REFERENCES [dbo].[Implications]
        ([ImplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImplicationAtomGroup'
CREATE INDEX [IX_FK_ImplicationAtomGroup]
ON [dbo].[AtomGroups]
    ([Implication_ImplicationId]);
GO

-- Creating foreign key on [Rule_RuleId] in table 'RuleImplications'
ALTER TABLE [dbo].[RuleImplications]
ADD CONSTRAINT [FK_RuleRuleImplication]
    FOREIGN KEY ([Rule_RuleId])
    REFERENCES [dbo].[Rules]
        ([RuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RuleRuleImplication'
CREATE INDEX [IX_FK_RuleRuleImplication]
ON [dbo].[RuleImplications]
    ([Rule_RuleId]);
GO

-- Creating foreign key on [Implication_ImplicationId] in table 'RuleImplications'
ALTER TABLE [dbo].[RuleImplications]
ADD CONSTRAINT [FK_ImplicationRuleImplication]
    FOREIGN KEY ([Implication_ImplicationId])
    REFERENCES [dbo].[Implications]
        ([ImplicationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ImplicationRuleImplication'
CREATE INDEX [IX_FK_ImplicationRuleImplication]
ON [dbo].[RuleImplications]
    ([Implication_ImplicationId]);
GO

-- Creating foreign key on [Rule_RuleId] in table 'PlanRules'
ALTER TABLE [dbo].[PlanRules]
ADD CONSTRAINT [FK_RulePlanRules]
    FOREIGN KEY ([Rule_RuleId])
    REFERENCES [dbo].[Rules]
        ([RuleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RulePlanRules'
CREATE INDEX [IX_FK_RulePlanRules]
ON [dbo].[PlanRules]
    ([Rule_RuleId]);
GO

-- Creating foreign key on [PlanFact_RecordId] in table 'PlanRules'
ALTER TABLE [dbo].[PlanRules]
ADD CONSTRAINT [FK_PlanFactPlanRules]
    FOREIGN KEY ([PlanFact_RecordId])
    REFERENCES [dbo].[PlanFacts]
        ([RecordId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PlanFactPlanRules'
CREATE INDEX [IX_FK_PlanFactPlanRules]
ON [dbo].[PlanRules]
    ([PlanFact_RecordId]);
GO

-- Creating foreign key on [AtomImplication_Implication_AtomId] in table 'Implications'
ALTER TABLE [dbo].[Implications]
ADD CONSTRAINT [FK_AtomImplication]
    FOREIGN KEY ([AtomImplication_Implication_AtomId])
    REFERENCES [dbo].[Atoms]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomImplication'
CREATE INDEX [IX_FK_AtomImplication]
ON [dbo].[Implications]
    ([AtomImplication_Implication_AtomId]);
GO

-- Creating foreign key on [AtomId] in table 'AtomFacts'
ALTER TABLE [dbo].[AtomFacts]
ADD CONSTRAINT [FK_AtomAtomFact]
    FOREIGN KEY ([AtomId])
    REFERENCES [dbo].[Atoms]
        ([AtomId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AtomAtomFact'
CREATE INDEX [IX_FK_AtomAtomFact]
ON [dbo].[AtomFacts]
    ([AtomId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------