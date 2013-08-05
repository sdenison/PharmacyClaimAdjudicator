if not exists(select * from sys.databases where name = 'PharmacyClaimAdjudicator')
	create database PharmacyClaimAdjudicator
GO

use PharmacyClaimAdjudicator
GO



ALTER TABLE PatientFacts DROP CONSTRAINT Patient_PatientFacts_Rel;
ALTER TABLE PatientFacts DROP CONSTRAINT PatientFacts_PatientFacts_Rel;
ALTER TABLE PatientGroup DROP CONSTRAINT Patient_PateintFacts_Rel;
ALTER TABLE PatientGroup DROP CONSTRAINT PatientGroup_PateintGroup_Rel;
ALTER TABLE PatientGroup DROP CONSTRAINT Group_PatientGroup_Rel;
ALTER TABLE GroupFacts DROP CONSTRAINT FKGroupFacts397517;
ALTER TABLE GroupFacts DROP CONSTRAINT GroupFacts_GroupFacts_Rel;
ALTER TABLE DrugFacts DROP CONSTRAINT DrugFacts_DrugFacts_Rel;
ALTER TABLE PharmacyFacts DROP CONSTRAINT FKPharmacyFa330200;
ALTER TABLE ChainPharmacy DROP CONSTRAINT FKChainPharm455074;
ALTER TABLE ChainPharmacy DROP CONSTRAINT FKChainPharm960365;
ALTER TABLE ChainFacts DROP CONSTRAINT FKChainFacts100830;
ALTER TABLE Claim DROP CONSTRAINT Claim_Pharmacy_Rel;
ALTER TABLE Claim DROP CONSTRAINT Patient_Claim_Rel;
ALTER TABLE NcpdpCommunicationLog DROP CONSTRAINT FKNcpdpCommu356639;
ALTER TABLE DoctorFacts DROP CONSTRAINT FKDoctorFact693793;
ALTER TABLE DoctorFacts DROP CONSTRAINT Doctor_DoctorFacts_Rel;
ALTER TABLE Claim DROP CONSTRAINT Claim_Prescriber_Rel;
ALTER TABLE Claim DROP CONSTRAINT Claim_PrimaryCare_Rel;
ALTER TABLE CompoundIngredients DROP CONSTRAINT FKCompoundIn343611;
ALTER TABLE CompoundIngredients DROP CONSTRAINT FKCompoundIn797620;
ALTER TABLE Claim DROP CONSTRAINT FKClaim485369;
ALTER TABLE ClaimCommunicationLog DROP CONSTRAINT FKClaimCommu570973;
ALTER TABLE ClaimCommunicationLog DROP CONSTRAINT FKClaimCommu616237;
ALTER TABLE PriorAuthorization DROP CONSTRAINT FKPriorAutho228362;
ALTER TABLE PriorAuthorization DROP CONSTRAINT FKPriorAutho211304;
ALTER TABLE DrugFacts DROP CONSTRAINT Drug_DrugFacts_Rel;
ALTER TABLE ExternalDollarAmounts DROP CONSTRAINT ExternalDollarAmount_Self_Rel;
ALTER TABLE ExternalDollarAmounts DROP CONSTRAINT Patient_ExternalDollar_Rel;
DROP TABLE Patient;
DROP TABLE PatientFacts;
DROP TABLE PatientGroup;
DROP TABLE [Group];
DROP TABLE GroupFacts;
DROP TABLE Claim;
DROP TABLE Drug;
DROP TABLE DrugFacts;
DROP TABLE Pharmacy;
DROP TABLE PharmacyFacts;
DROP TABLE Chain;
DROP TABLE ChainFacts;
DROP TABLE ChainPharmacy;
DROP TABLE NcpdpCommunicationLog;
DROP TABLE Doctor;
DROP TABLE DoctorFacts;
DROP TABLE CompoundIngredients;
DROP TABLE ClaimCommunicationLog;
DROP TABLE PriorAuthorization;
DROP TABLE ExternalDollarAmounts;
CREATE TABLE Patient (
  PatientId             int IDENTITY NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  CONSTRAINT Id 
    PRIMARY KEY (PatientId));
CREATE TABLE PatientFacts (
  RecordId                int IDENTITY NOT NULL, 
  FirstName               nvarchar(50) NOT NULL, 
  MiddleName              nvarchar(50) NOT NULL, 
  LastName                nvarchar(50) NOT NULL, 
  CardholderId            nvarchar(20) NOT NULL, 
  BirthDate               datetime NOT NULL, 
  PersonCode              nvarchar(3) NOT NULL, 
  PatientRelationshipCode nvarchar(2) NOT NULL, 
  Gender                  char(1) NOT NULL, 
  Retraction              bit NOT NULL, 
  OriginalFactRecordId    int NULL, 
  RecordCreatedDateTime   datetime NOT NULL, 
  RecordCreatedUser       nvarchar(30) NOT NULL, 
  PatientId               int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE PatientGroup (
  RecordId              int IDENTITY NOT NULL, 
  EffectiveDate         datetime NOT NULL, 
  ExpirationDate        datetime NOT NULL, 
  Retraction            bit NOT NULL, 
  OriginalFactRecordId  int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  PatientId             int NOT NULL, 
  GroupId               int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE [Group] (
  GroupId               int IDENTITY NOT NULL, 
  GroupGroupId          int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  PRIMARY KEY (GroupId));
CREATE TABLE GroupFacts (
  RecordId              int IDENTITY NOT NULL, 
  Name                  nvarchar(100) NOT NULL, 
  ShortName             nvarchar(100) NOT NULL, 
  Retraction            bit NOT NULL, 
  OriginalFactRecordId  int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  GroupId               int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE Claim (
  AuthorizationNumber               nvarchar(20) NOT NULL, 
  PrescriptionNumberQualifier       char(1) NOT NULL, 
  PrescriptionNumber                nvarchar(12) NOT NULL, 
  Ndc                               char(11) NOT NULL, 
  PharmacyId                        int NOT NULL, 
  PatientId                         int NOT NULL, 
  Reversal                          bit NOT NULL, 
  OriginalAuthorizationNumber       nvarchar(20) NULL, 
  RecordCreatedDateTime             datetime NOT NULL, 
  RecordCreatedUser                 nvarchar(30) NOT NULL, 
  PrescriberDoctorId                int NOT NULL, 
  PrimaryCareDoctorId               int NOT NULL, 
  BasisOfCostDeterminationSubmitted char(1) NULL, 
  DispensingFeeSubmitted            decimal(19, 2) NULL, 
  FlatSalesTaxAmountSubmitted       decimal(19, 2) NULL, 
  GrossAmountDue                    int NULL, 
  Compound                          char(1) NOT NULL, 
  PRIMARY KEY (AuthorizationNumber));
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'Indicates the type of billing submitted.
1 - Rx Billing
2 - Service Billing', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'PrescriptionNumberQualifier';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'The Claim table holds Claims and also Reversals.  Reversals must point to an original claim with the Authorization Number.', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'PrescriptionNumber';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'NCPDP 423-DN
Code indicating the method by which ''Ingredient Cost Submitted'' (Field 409-D9) was calculated.', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'BasisOfCostDeterminationSubmitted';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'NCPDP 412-DC
Dispensing fee submitted by the pharmacy. This amount is included in the ''Gross Amount Due'' (430-DU).', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'DispensingFeeSubmitted';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'NCPDP 481-HA
Flat sales tax submitted for prescription. This amount is included in the ''Gross Amount Due'' (430-DU).', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'FlatSalesTaxAmountSubmitted';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'NCPDP 430-DU
Total price claimed from all sources. For prescription claim request, field represents a sum of ‘Ingredient Cost Submitted’ (409-D9), ‘Dispensing Fee Submitted’ (412-DC), ‘Flat Sales Tax Amount Submitted’ (481-HA), ‘Percentage Sales Tax Amount Submitted’ (482-GE), ‘Incentive Amount Submitted’ (438-E3), ‘Other Amount Claimed’ (48Ø-H9). For service claim request, field represents a sum of ‘Professional Services Fee Submitted’ (477-BE), ‘Flat Sales Tax Amount Submitted’ (481-HA), ‘Percentage Sales Tax Amount Submitted’ (482-GE), ‘Other Amount Claimed’ (480-H9).', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'Claim', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'GrossAmountDue';
CREATE TABLE Drug (
  Ndc                  char(11) NOT NULL, 
  RecordCreateDateTime datetime NOT NULL, 
  RecordCreatedUser    nvarchar(30) NOT NULL, 
  PRIMARY KEY (Ndc));
CREATE TABLE DrugFacts (
  RecordId                                 int IDENTITY NOT NULL, 
  AverageAcquisitionCost                   decimal(19, 2) NULL, 
  WholesaleAcquisitionCost                 decimal(19, 2) NULL, 
  DirectPrice                              decimal(19, 2) NULL, 
  SuggestedWholesalePrice                  decimal(19, 2) NULL, 
  FederalFinancingParticipationUpperLimits decimal(19, 2) NULL, 
  MedicarePartB                            decimal(19, 2) NULL, 
  MedicaidStateMaximumAllowableCost        decimal(19, 2) NULL, 
  AhfsCode                                 varchar(8) NULL, 
  Retraction                               bit NOT NULL, 
  OriginalFactRecordId                     int NULL, 
  RecordCreatedDateTime                    datetime NOT NULL, 
  RecordCreatedUser                        nvarchar(30) NOT NULL, 
  Ndc                                      char(11) NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE Pharmacy (
  PharmacyId           int IDENTITY NOT NULL, 
  RecordCreateDateTime datetime NOT NULL, 
  RecordCreatedUser    nvarchar(30) NOT NULL, 
  PRIMARY KEY (PharmacyId));
CREATE TABLE PharmacyFacts (
  RecordId              int IDENTITY NOT NULL, 
  PharmacyName          nvarchar(100) NOT NULL, 
  Nabp                  nvarchar(11) NULL, 
  Npi                   nvarchar(11) NULL, 
  PharmacyId            int NOT NULL, 
  Retraction            bit NOT NULL, 
  OriginalFactRecordId  int NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE Chain (
  ChainId              int IDENTITY NOT NULL, 
  RecordCreateDateTime datetime NOT NULL, 
  RecordCreatedUser    nvarchar(30) NOT NULL, 
  PRIMARY KEY (ChainId));
CREATE TABLE ChainFacts (
  RecordId              int IDENTITY NOT NULL, 
  Name                  nvarchar(100) NOT NULL, 
  ChainChainId          int NOT NULL, 
  Retraction            bit NOT NULL, 
  OriginalFactRecordId  int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     datetime NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE ChainPharmacy (
  RecordId              int IDENTITY NOT NULL, 
  EffectiveDate         datetime NOT NULL, 
  ExpirationDate        datetime NOT NULL, 
  Retraction            bit NOT NULL, 
  OriginalFactRecordId  int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  PharmacyId            int NOT NULL, 
  ChainId               int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE NcpdpCommunicationLog (
  RecordId              int IDENTITY NOT NULL, 
  Direction             char(1) NOT NULL, 
  NcpdpVersion          char(2) NOT NULL, 
  Transmission          nvarchar(max) NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  ResponseRecordId      int NOT NULL, 
  PRIMARY KEY (RecordId));
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'I - Incoming
O - Outgoing', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'NcpdpCommunicationLog', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'Direction';
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'For a record with a Direction = ''O'' the ResponseRecordId should be the RecordId where Direction = ''I''.', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'NcpdpCommunicationLog', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'ResponseRecordId';
CREATE TABLE Doctor (
  DoctorId              int IDENTITY NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  PRIMARY KEY (DoctorId));
CREATE TABLE DoctorFacts (
  RecordId              int IDENTITY NOT NULL, 
  FirstName             nvarchar(100) NULL, 
  LastName              nvarchar(50) NULL, 
  Upin                  nvarchar(20) NULL, 
  Retraction            bit NOT NULL, 
  OriginalRecordId      int NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  DoctorId              int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE CompoundIngredients (
  RecordId                                   int IDENTITY NOT NULL, 
  CompoundIngredientBasisOfCostDetermination int NOT NULL, 
  Ndc                                        char(11) NOT NULL, 
  AuthorizationNumber                        nvarchar(20) NOT NULL, 
  PRIMARY KEY (RecordId));
EXEC sp_addextendedproperty 
  @NAME = N'MS_Description', @VALUE = 'NCPDP 490-UE
Code indicating the method by which the drug cost of an ingredient used in a compound was calculated.', 
  @LEVEL0TYPE = N'Schema', @LEVEL0NAME = 'dbo', 
  @LEVEL1TYPE = N'Table', @LEVEL1NAME = 'CompoundIngredients', 
  @LEVEL2TYPE = N'Column', @LEVEL2NAME = 'CompoundIngredientBasisOfCostDetermination';
CREATE TABLE ClaimCommunicationLog (
  RecordId                      int IDENTITY NOT NULL, 
  ClaimAuthorizationNumber      nvarchar(20) NOT NULL, 
  NcpdpCommunicationLogRecordId int NOT NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE PriorAuthorization (
  RecordId              int IDENTITY NOT NULL, 
  PatientPatientId      int NOT NULL, 
  PriorAuthNumber       varchar(20) NULL, 
  Used                  bit NULL, 
  AuthorizationNumber   nvarchar(20) NOT NULL, 
  Retraction            bit NOT NULL, 
  RecordCreatedDateTime datetime NULL, 
  RecordCreatedUser     nvarchar(30) NULL, 
  OriginalFactRecordId  int NULL, 
  PRIMARY KEY (RecordId));
CREATE TABLE ExternalDollarAmounts (
  RecordId              int IDENTITY NOT NULL, 
  Amount                decimal(19, 2) NOT NULL, 
  Description           nvarchar(100) NOT NULL, 
  EffectiveDate         datetime NOT NULL, 
  ExpirationDate        datetime NOT NULL, 
  Retraction            bit NOT NULL, 
  RecordCreatedDateTime datetime NOT NULL, 
  RecordCreatedUser     nvarchar(30) NOT NULL, 
  OriginalFactRecordId  int NOT NULL, 
  PatientId             int NOT NULL, 
  PRIMARY KEY (RecordId));
ALTER TABLE PatientFacts ADD CONSTRAINT Patient_PatientFacts_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);
ALTER TABLE PatientFacts ADD CONSTRAINT PatientFacts_PatientFacts_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES PatientFacts (RecordId);
ALTER TABLE PatientGroup ADD CONSTRAINT Patient_PateintFacts_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);
ALTER TABLE PatientGroup ADD CONSTRAINT PatientGroup_PateintGroup_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES PatientGroup (RecordId);
ALTER TABLE PatientGroup ADD CONSTRAINT Group_PatientGroup_Rel FOREIGN KEY (GroupId) REFERENCES [Group] (GroupId);
ALTER TABLE GroupFacts ADD CONSTRAINT FKGroupFacts397517 FOREIGN KEY (GroupId) REFERENCES [Group] (GroupId);
ALTER TABLE GroupFacts ADD CONSTRAINT GroupFacts_GroupFacts_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES GroupFacts (RecordId);
ALTER TABLE DrugFacts ADD CONSTRAINT DrugFacts_DrugFacts_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES DrugFacts (RecordId);
ALTER TABLE PharmacyFacts ADD CONSTRAINT FKPharmacyFa330200 FOREIGN KEY (PharmacyId) REFERENCES Pharmacy (PharmacyId);
ALTER TABLE ChainPharmacy ADD CONSTRAINT FKChainPharm455074 FOREIGN KEY (PharmacyId) REFERENCES Pharmacy (PharmacyId);
ALTER TABLE ChainPharmacy ADD CONSTRAINT FKChainPharm960365 FOREIGN KEY (ChainId) REFERENCES Chain (ChainId);
ALTER TABLE ChainFacts ADD CONSTRAINT FKChainFacts100830 FOREIGN KEY (ChainChainId) REFERENCES Chain (ChainId);
ALTER TABLE Claim ADD CONSTRAINT Claim_Pharmacy_Rel FOREIGN KEY (PharmacyId) REFERENCES Pharmacy (PharmacyId);
ALTER TABLE Claim ADD CONSTRAINT Patient_Claim_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);
ALTER TABLE NcpdpCommunicationLog ADD CONSTRAINT FKNcpdpCommu356639 FOREIGN KEY (ResponseRecordId) REFERENCES NcpdpCommunicationLog (RecordId);
ALTER TABLE DoctorFacts ADD CONSTRAINT FKDoctorFact693793 FOREIGN KEY (OriginalRecordId) REFERENCES DoctorFacts (RecordId);
ALTER TABLE DoctorFacts ADD CONSTRAINT Doctor_DoctorFacts_Rel FOREIGN KEY (DoctorId) REFERENCES Doctor (DoctorId);
ALTER TABLE Claim ADD CONSTRAINT Claim_Prescriber_Rel FOREIGN KEY (PrescriberDoctorId) REFERENCES Doctor (DoctorId);
ALTER TABLE Claim ADD CONSTRAINT Claim_PrimaryCare_Rel FOREIGN KEY (PrimaryCareDoctorId) REFERENCES Doctor (DoctorId);
ALTER TABLE CompoundIngredients ADD CONSTRAINT FKCompoundIn343611 FOREIGN KEY (Ndc) REFERENCES Drug (Ndc);
ALTER TABLE CompoundIngredients ADD CONSTRAINT FKCompoundIn797620 FOREIGN KEY (AuthorizationNumber) REFERENCES Claim (AuthorizationNumber);
ALTER TABLE Claim ADD CONSTRAINT FKClaim485369 FOREIGN KEY (Ndc) REFERENCES Drug (Ndc);
ALTER TABLE ClaimCommunicationLog ADD CONSTRAINT FKClaimCommu570973 FOREIGN KEY (ClaimAuthorizationNumber) REFERENCES Claim (AuthorizationNumber);
ALTER TABLE ClaimCommunicationLog ADD CONSTRAINT FKClaimCommu616237 FOREIGN KEY (NcpdpCommunicationLogRecordId) REFERENCES NcpdpCommunicationLog (RecordId);
ALTER TABLE PriorAuthorization ADD CONSTRAINT FKPriorAutho228362 FOREIGN KEY (PatientPatientId) REFERENCES Patient (PatientId);
ALTER TABLE PriorAuthorization ADD CONSTRAINT FKPriorAutho211304 FOREIGN KEY (AuthorizationNumber) REFERENCES Claim (AuthorizationNumber);
ALTER TABLE DrugFacts ADD CONSTRAINT Drug_DrugFacts_Rel FOREIGN KEY (Ndc) REFERENCES Drug (Ndc);
ALTER TABLE ExternalDollarAmounts ADD CONSTRAINT ExternalDollarAmount_Self_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES ExternalDollarAmounts (RecordId);
ALTER TABLE ExternalDollarAmounts ADD CONSTRAINT Patient_ExternalDollar_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);

GO


set identity_insert dbo.patient on;
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (20, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (21, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (22, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (55, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (1, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (60, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (61, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (62, '2009-10-01 00:00:01', 'SDENISON');
insert into dbo.Patient(PatientId, RecordCreatedDateTime, RecordCreatedUser) values (100, '2009-10-01 00:00:01', 'SDENISON');
set identity_insert dbo.patient off;

set identity_insert dbo.PatientFacts on;
DECLARE @TRUE bit;
set @TRUE=1;
DECLARE @FALSE bit;
set @FALSE=0;
--User 100
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (999, 'GARY', 'M', 'COOPER', '123456789', '1999-05-04', '01', '1', '1', @FALSE, '2013-04-01 12:03:39', 'SDENISON', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1000, 'GARY', 'M', 'COOPER', '987654321', '1999-05-04', '01', '1', '1', @FALSE, '2013-05-05 17:05:25', 'SDENISON', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1001, 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @FALSE, '2013-05-10 00:00:00', 'TEST', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1002, 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @TRUE, '2013-06-01 08:59:00', 'SDENISON', 1001, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1003, 'GARY', 'M', 'COOPER', '987654321', '1999-07-05', '01', '1', '1', @FALSE, '2013-06-05 09:15:30', 'SDENISON', null, 100);
--User 20 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1020, 'Eric', '', 'Bloom', '111222333', '1999-12-01', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 20);
--User 21
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1021, 'Richard', '', 'Hell', '222333444555', '1949-10-02', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 21);
--User 22
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1022, 'Joe', '', 'Strummer', '222333444', '1952-08-21', '01', '1', '', @FALSE, sysdatetime(), 'SDENISON', null, 22);
--User55 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1023, 'John', '', 'Doe', '333444555b', '1954-02-25', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 55);
--User 1 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1024, 'Bobby', '', 'Bob', '123456789', '1975-02-19', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 1);

--Family
--User 61 (Dad)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1026, 'Joseph', '', 'Smith', '987654321', '1932-09-15', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 60  (Son)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1027, 'Joseph', '', 'Smith', '987654321', '1962-06-15', '03', '3', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 62 (twin sister)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values (1028, 'Josephina', '', 'Smith', '987654321', '1962-06-15', '04', '3', '2', @FALSE, sysdatetime(), 'SDENISON', null, 62);

set identity_insert dbo.patientfacts off;