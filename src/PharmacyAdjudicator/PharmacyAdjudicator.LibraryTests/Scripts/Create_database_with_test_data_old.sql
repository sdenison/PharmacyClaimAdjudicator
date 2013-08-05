Use Master
if not Exists(select * from sys.databases where name = 'PharmacyClaimAdjudicator')
create database PharmacyClaimAdjudicator;

Use PharmacyClaimAdjudicator;
DROP TABLE PatientFacts;
DROP TABLE GroupFacts;
DROP TABLE PatientGroup;
DROP TABLE Patient;
DROP TABLE [Group];

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
ALTER TABLE PatientFacts ADD CONSTRAINT Patient_PatientFacts_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);
ALTER TABLE PatientFacts ADD CONSTRAINT PatientFacts_PatientFacts_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES PatientFacts (RecordId);
ALTER TABLE PatientGroup ADD CONSTRAINT Patient_PateintFacts_Rel FOREIGN KEY (PatientId) REFERENCES Patient (PatientId);
ALTER TABLE PatientGroup ADD CONSTRAINT PatientGroup_PateintGroup_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES PatientGroup (RecordId);
ALTER TABLE PatientGroup ADD CONSTRAINT Group_PatientGroup_Rel FOREIGN KEY (GroupId) REFERENCES [Group] (GroupId);
ALTER TABLE GroupFacts ADD CONSTRAINT FKGroupFacts397517 FOREIGN KEY (GroupId) REFERENCES [Group] (GroupId);
ALTER TABLE GroupFacts ADD CONSTRAINT GroupFacts_GroupFacts_Rel FOREIGN KEY (OriginalFactRecordId) REFERENCES GroupFacts (RecordId);

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
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (999, 'GARY', 'M', 'COOPER', '123456789', '1999-05-04', '01', '1', '1', @FALSE, '2013-04-01 12:03:39', 'SDENISON', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1000, 'GARY', 'M', 'COOPER', '987654321', '1999-05-04', '01', '1', '1', @FALSE, '2013-05-05 17:05:25', 'SDENISON', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1001, 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @FALSE, '2013-05-10 00:00:00', 'TEST', null, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1002, 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @TRUE, '2013-06-01 08:59:00', 'SDENISON', 1001, 100);
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1003, 'GARY', 'M', 'COOPER', '987654321', '1999-07-05', '01', '1', '1', @FALSE, '2013-06-05 09:15:30', 'SDENISON', null, 100);
--User 20 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1020, 'Eric', '', 'Bloom', '111222333', '1999-12-01', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 20);
--User 21
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1021, 'Richard', '', 'Hell', '222333444555', '1949-10-02', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 21);
--User 22
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1022, 'Joe', '', 'Strummer', '222333444', '1952-08-21', '01', '1', '', @FALSE, sysdatetime(), 'SDENISON', null, 22);
--User55 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1023, 'John', '', 'Doe', '333444555b', '1954-02-25', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 55);
--User 1 
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1024, 'Bobby', '', 'Bob', '123456789', '1975-02-19', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 1);

--Family
--User 61 (Dad)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1026, 'Joseph', '', 'Smith', '987654321', '1932-09-15', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 60  (Son)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1027, 'Joseph', '', 'Smith', '987654321', '1962-06-15', '03', '3', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 62 (twin sister)
insert into dbo.patientfacts (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, patientId)
	values (1028, 'Josephina', '', 'Smith', '987654321', '1962-06-15', '04', '3', '2', @FALSE, sysdatetime(), 'SDENISON', null, 62);

set identity_insert dbo.patientfacts off;





