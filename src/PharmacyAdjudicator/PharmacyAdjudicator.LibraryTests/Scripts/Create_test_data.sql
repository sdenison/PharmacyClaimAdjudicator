SET QUOTED_IDENTIFIER OFF;
GO
USE [PharmacyClaimAdjudicator];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
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

--set identity_insert dbo.PatientDetail on;
DECLARE @TRUE bit;
set @TRUE=1;
DECLARE @FALSE bit;
set @FALSE=0;
--User 100
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('9D8E05B5-3E34-4512-9DAB-057EF627A404', 'GARY', 'M', 'COOPER', '123456789', '1999-05-04', '01', '1', '1', @FALSE, '2013-04-01 12:03:39', 'SDENISON', null, 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('D0D8C406-3D93-4964-9579-63F74B35534E', 'GARY', 'M', 'COOPER', '987654321', '1999-05-04', '01', '1', '1', @TRUE, '2013-05-05 17:05:25', 'SDENISON', '9D8E05B5-3E34-4512-9DAB-057EF627A404', 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('8B90061C-A11E-4114-863D-4C57991E30BF', 'GARY', 'M', 'COOPER', '987654321', '1999-05-04', '01', '1', '1', @FALSE, '2013-05-05 17:05:25', 'SDENISON', null, 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('8A40E6E0-CFB6-478B-A698-21A9A3E8E169', 'GARY', 'M', 'COOPER', '987654321', '1999-05-04', '01', '1', '1', @TRUE, '2013-05-05 17:05:25', 'SDENISON', '8B90061C-A11E-4114-863D-4C57991E30BF', 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('9BC9DC04-A279-4A50-BEC1-2E64446202D5', 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @FALSE, '2013-05-10 00:00:00', 'TEST', null, 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('8580F9B8-3F05-410E-B025-FBE8A9E1B072', 'GARY', 'M', 'COOPER', '987654321', '1999-05-05', '01', '1', '1', @TRUE, '2013-06-01 08:59:00', 'SDENISON', '9BC9DC04-A279-4A50-BEC1-2E64446202D5', 100);
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('916BF69E-6B5A-40BB-BBAE-54E64332669A', 'GARY', 'M', 'COOPER', '987654321', '1999-07-05', '01', '1', '1', @FALSE, '2013-06-05 09:15:30', 'SDENISON', null, 100);
--User 20 
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('14239EAF-364A-42F6-A7D6-E92EAAFBCEFC', 'Eric', '', 'Bloom', '111222333', '1999-12-01', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 20);
--User 21
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('6D056E08-B334-4AAD-99FF-75F9D77E6CA0', 'Richard', '', 'Hell', '222333444555', '1949-10-02', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 21);
--User 22
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('1A456D6A-61CD-467C-B2AF-7086A3AFD926', 'Joe', '', 'Strummer', '222333444', '1952-08-21', '01', '1', '', @FALSE, sysdatetime(), 'SDENISON', null, 22);
--User55 
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('91CCA16B-587F-4B28-8BED-93AB4698E514', 'John', '', 'Doe', '333444555b', '1954-02-25', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 55);
--User 1 
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('2C1A176D-D735-49D4-99A1-D78DCCD18EDF', 'Bobby', '', 'Bob', '123456789', '1975-02-19', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 1);

--Family
--User 61 (Dad)
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('578957C0-0E28-4FC3-B184-7F10EDAB575C', 'Joseph', '', 'Smith', '987654321', '1932-09-15', '01', '1', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 60  (Son)
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('95ECC071-F983-469A-B2D5-F8B63A7F599F', 'Joseph', '', 'Smith', '987654321', '1962-06-15', '03', '3', '1', @FALSE, sysdatetime(), 'SDENISON', null, 61);
--User 62 (twin sister)
insert into dbo.patientdetail (recordid, firstname, middlename, lastname, cardholderid, birthdate, personcode, patientrelationshipcode, gender, retraction, recordcreateddatetime, recordcreateduser, originalfactrecordid, PatientId)
	values ('366B47B2-A58C-42CA-863F-BDD677762D57', 'Josephina', '', 'Smith', '987654321', '1962-06-15', '04', '3', '2', @FALSE, sysdatetime(), 'SDENISON', null, 62);

insert into dbo.[group] (groupinternalid, recordcreateddatetime, recordcreateduser)
	values('E24B42FA-4A53-4949-B695-957E690451FD', sysdatetime(), 'SDENISON');

insert into dbo.patientgroup (recordid, effectivedate, expirationdate, retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, patientid, groupinternalid)
	values ('4ACBFE41-CC8E-4F15-B027-98516EFAC728', '2003-01-31', '9999-12-31', @FALSE, null, sysdatetime(), 'SDENISON', 61, 'E24B42FA-4A53-4949-B695-957E690451FD');

insert into dbo.groupdetail (recordid, Name, GroupId, Retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, groupinternalid)
	values ('D02F06FE-30FA-4062-ADB4-CF6BE5633784', 'Very excellent group', 'GROUP1', @FALSE, null, sysdatetime(), 'SDENISON', 'E24B42FA-4A53-4949-B695-957E690451FD');

--Add client ACME with multiple versions of detail record
insert into client (clientinternalid, recordcreateddatetime, recordcreateduser)
	values ('EE60CD12-8063-4FBC-9165-0C6F7D81B6EB', '2014-09-24 14:59:04.090', 'Test');

insert into clientdetail (recordid, clientid, name, retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, clientinternalid)
	values ('EE91E12B-F74D-439B-B85C-3E98DFC4B69E', 'ACME',	'ACME Corporation',	0, NULL, '2014-09-24 14:59:06.033', 'Test', 'EE60CD12-8063-4FBC-9165-0C6F7D81B6EB');
insert into clientdetail (recordid, clientid, name, retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, clientinternalid)
	values ('36439C62-8EAD-4F0B-BA21-9429371392DC', 'ACME', 'New ACME Corporation Name', 1, 'EE91E12B-F74D-439B-B85C-3E98DFC4B69E', '2014-09-24 14:59:09.470', 'Test', 'EE60CD12-8063-4FBC-9165-0C6F7D81B6EB');
insert into clientdetail (recordid, clientid, name, retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, clientinternalid)
	values ('FA73CA67-1BA8-4277-9B81-28D8C0467641', 'ACME', 'New ACME Corporation Name', 0, NULL, '2014-09-24 14:59:09.470', 'Test', 'EE60CD12-8063-4FBC-9165-0C6F7D81B6EB');

--Add client OB with just one detail record
insert into client (clientinternalid, recordcreateddatetime, recordcreateduser)
	values ('874199AF-8839-4984-B4A0-C4DB53D5442A', '2014-09-24 16:59:04.090', 'Test');
insert into clientdetail (recordid, clientid, name, retraction, originalfactrecordid, recordcreateddatetime, recordcreateduser, clientinternalid)
	values ('0F0F9FB0-067D-4B5C-B807-1173C1DBA959', 'OB', 'Oyster Bar', 0, NULL, '2014-09-24 16:59:09.470', 'Test', '874199AF-8839-4984-B4A0-C4DB53D5442A'); 

--Add address types
insert into AddressType values ("Physical", "Physical Location");
insert into AddressType values ("Mailing", "Mailing Address");
insert into AddressType values ("Billing", "Billing Address");