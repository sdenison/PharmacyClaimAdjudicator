﻿USE [PharmacyClaimAdjudicator]
GO
INSERT [dbo].[Group] ([GroupInternalId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'e24b42fa-4a53-4949-b695-957e690451fd', CAST(0x0000A3E200C06D51 AS DateTime), N'SDENISON')
INSERT [dbo].[Plan] ([PlanInternalId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'22ec2f61-8c34-4501-867d-ede13cfb63ae', CAST(0x0000A3E30111C039 AS DateTime), N'Test')
INSERT [dbo].[GroupDetail] ([RecordId], [Name], [GroupId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [GroupInternalId]) VALUES (N'd02f06fe-30fa-4062-adb4-cf6be5633784', N'Very excellent group', N'GROUP1', 0, NULL, CAST(0x0000A3E200C06D52 AS DateTime), N'SDENISON', N'e24b42fa-4a53-4949-b695-957e690451fd')
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (1, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (20, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (21, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (22, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (55, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (60, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (61, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (62, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
INSERT [dbo].[Patient] ([PatientId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (100, CAST(0x00009C950000012C AS DateTime), N'SDENISON')
SET IDENTITY_INSERT [dbo].[Patient] OFF
INSERT [dbo].[PatientGroup] ([RecordId], [EffectiveDate], [ExpirationDate], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId], [GroupInternalId]) VALUES (N'4acbfe41-cc8e-4f15-b027-98516efac728', CAST(0x0000931200000000 AS DateTime), CAST(0x002D247F00000000 AS DateTime), 0, NULL, CAST(0x0000A3E200C06D51 AS DateTime), N'SDENISON', 61, N'e24b42fa-4a53-4949-b695-957e690451fd')
INSERT [dbo].[Client] ([ClientInternalId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'ee60cd12-8063-4fbc-9165-0c6f7d81b6eb', CAST(0x0000A3B000F6EFBB AS DateTime), N'Test')
INSERT [dbo].[Client] ([ClientInternalId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'874199af-8839-4984-b4a0-c4db53d5442a', CAST(0x0000A3B00117E53B AS DateTime), N'Test')
INSERT [dbo].[ClientGroup] ([RecordId], [ClientInternalId], [GroupInternalId], [EffectiveDate], [ExpirationDate], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'32a9594c-74a1-463a-a995-4913c31551e6', N'ee60cd12-8063-4fbc-9165-0c6f7d81b6eb', N'e24b42fa-4a53-4949-b695-957e690451fd', CAST(0x00008EAC00000000 AS DateTime), CAST(0x002D247F00000000 AS DateTime), 0, NULL, CAST(0x0000A3B000F6F609 AS DateTime), N'Test')
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'9d8e05b5-3e34-4512-9dab-057ef627a404', N'GARY', N'M', N'COOPER', N'123456789', CAST(0x00008DBA00000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A19300C6C1A4 AS DateTime), N'SDENISON', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'9bc9dc04-a279-4a50-bec1-2e64446202d5', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DBB00000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A1BA00000000 AS DateTime), N'TEST', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'8b90061c-a11e-4114-863d-4c57991e30bf', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DBA00000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A1B50119A39C AS DateTime), N'SDENISON', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'916bf69e-6b5a-40bb-bbae-54e64332669a', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DF800000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A1D400989298 AS DateTime), N'SDENISON', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'1a456d6a-61cd-467c-b2af-7086a3afd926', N'Joe', N'', N'Strummer', N'222333444', CAST(0x00004B1900000000 AS DateTime), N'01', N'1', N' ', 0, NULL, CAST(0x0000A3E200C06D50 AS DateTime), N'SDENISON', 22)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'6d056e08-b334-4aad-99ff-75f9d77e6ca0', N'Richard', N'', N'Hell', N'222333444555', CAST(0x000046FB00000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A3E200C06D4F AS DateTime), N'SDENISON', 21)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'578957c0-0e28-4fc3-b184-7f10edab575c', N'Joseph', N'', N'Smith', N'987654321', CAST(0x00002EA900000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A3E200C06D50 AS DateTime), N'SDENISON', 61)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'91cca16b-587f-4b28-8bed-93ab4698e514', N'John', N'', N'Doe', N'333444555b', CAST(0x00004D4200000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A3E200C06D50 AS DateTime), N'SDENISON', 55)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'366b47b2-a58c-42ca-863f-bdd677762d57', N'Josephina', N'', N'Smith', N'987654321', CAST(0x0000591A00000000 AS DateTime), N'04', N'3', N'2', 0, NULL, CAST(0x0000A3E200C06D51 AS DateTime), N'SDENISON', 62)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'2c1a176d-d735-49d4-99a1-d78dccd18edf', N'Bobby', N'', N'Bob', N'123456789', CAST(0x00006B3200000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A3E200C06D50 AS DateTime), N'SDENISON', 1)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'14239eaf-364a-42f6-a7d6-e92eaafbcefc', N'Eric', N'', N'Bloom', N'111222333', CAST(0x00008E8D00000000 AS DateTime), N'01', N'1', N'1', 0, NULL, CAST(0x0000A3E200C06D4F AS DateTime), N'SDENISON', 20)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'95ecc071-f983-469a-b2d5-f8b63a7f599f', N'Joseph', N'', N'Smith', N'987654321', CAST(0x0000591A00000000 AS DateTime), N'03', N'3', N'1', 0, NULL, CAST(0x0000A3E200C06D51 AS DateTime), N'SDENISON', 60)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'8a40e6e0-cfb6-478b-a698-21a9a3e8e169', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DBA00000000 AS DateTime), N'01', N'1', N'1', 1, N'8b90061c-a11e-4114-863d-4c57991e30bf', CAST(0x0000A1B50119A39C AS DateTime), N'SDENISON', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'8580f9b8-3f05-410e-b025-fbe8a9e1b072', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DBB00000000 AS DateTime), N'01', N'1', N'1', 1, N'9bc9dc04-a279-4a50-bec1-2e64446202d5', CAST(0x0000A1D000940A70 AS DateTime), N'SDENISON', 100)
INSERT [dbo].[PatientDetail] ([RecordId], [FirstName], [MiddleName], [LastName], [CardholderId], [BirthDate], [PersonCode], [PatientRelationshipCode], [Gender], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PatientId]) VALUES (N'd0d8c406-3d93-4964-9579-63f74b35534e', N'GARY', N'M', N'COOPER', N'987654321', CAST(0x00008DBA00000000 AS DateTime), N'01', N'1', N'1', 1, N'9d8e05b5-3e34-4512-9dab-057ef627a404', CAST(0x0000A1B50119A39C AS DateTime), N'SDENISON', 100)
INSERT [dbo].[AddressType] ([AddressTypeCode], [Description]) VALUES (N'Billing', N'Billing Address')
INSERT [dbo].[AddressType] ([AddressTypeCode], [Description]) VALUES (N'Mailing', N'Mailing Address')
INSERT [dbo].[AddressType] ([AddressTypeCode], [Description]) VALUES (N'Physical', N'Physical Location')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'8914a0b7-93d1-4997-b8f5-0db48527b2c0', N'ResponseStatus', N'Captured')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'adbf66ce-3fb3-478f-952d-4c64d2f1ffbc', N'DispensingFeePaid', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'7e36ae68-efae-4e6c-8f25-554d955af752', N'TotalAmountPaid', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'89cbdece-e4c8-4805-9ba1-5dfdf4867706', N'FlatSalesTaxAmountPaid', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'1e56cf49-fbb4-4a91-98f8-65054f9800df', N'TaxExemptIndicator', N'NotSpecified')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'0bdf9a6c-6103-4083-bcdc-68d91d6a5837', N'IngredientCostPaid', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'9e443e4f-65ce-4973-8232-68f651ed08ac', N'AmountOfCopay', N'100')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'b6d05998-6c2a-4d23-9ba2-8fc6008a2b64', N'PercentageSalesTaxAmountPaid', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'1c6ae596-ab9e-4a49-a18d-a852bd6aad8b', N'PatientPaySalesTaxAmount', N'0')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'f987c063-fadd-4aff-b1b4-b8780994489b', N'Formulary', N'false')
INSERT [dbo].[Rule] ([RuleId], [RuleType], [DefaultValue]) VALUES (N'6889cfbe-bd18-4a1f-9e11-f91d6ca8c5cc', N'BasisOfReimbursement', N'NotSpecified')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'88af0e09-ee2c-42a2-877a-0321f269a56c', N'1c6ae596-ab9e-4a49-a18d-a852bd6aad8b', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'779307b9-34ed-4a3f-ab95-0cf639e12a0f', N'6889cfbe-bd18-4a1f-9e11-f91d6ca8c5cc', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0E AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'242f0293-c159-481f-8ea5-2efae1e5c88b', N'9e443e4f-65ce-4973-8232-68f651ed08ac', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0D AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'868940db-d119-4131-96b0-467239475c3a', N'f987c063-fadd-4aff-b1b4-b8780994489b', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'6bb2585e-0666-489a-9336-676c89c5fb57', N'89cbdece-e4c8-4805-9ba1-5dfdf4867706', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0E AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'7578706a-5d64-44eb-a0ea-6c0c261dd232', N'7e36ae68-efae-4e6c-8f25-554d955af752', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'5b4587b3-94ff-458a-b4d0-7325b16292ee', N'8914a0b7-93d1-4997-b8f5-0db48527b2c0', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'bfba449a-e8aa-4a97-8758-78e1f1b8fb3b', N'0bdf9a6c-6103-4083-bcdc-68d91d6a5837', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'096b9c25-7dd3-4eef-ae80-b52da3bf47bf', N'adbf66ce-3fb3-478f-952d-4c64d2f1ffbc', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0E AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'd30a14b6-8bec-4a44-a3ae-c0e5433e000d', N'1e56cf49-fbb4-4a91-98f8-65054f9800df', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanRule] ([RecordId], [RuleId], [PlanInternalId], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'09ebbee1-f6cf-4c06-923b-d896d3f9dae9', N'b6d05998-6c2a-4d23-9ba2-8fc6008a2b64', N'22ec2f61-8c34-4501-867d-ede13cfb63ae', 0, NULL, CAST(0x0000A3E30111CA0F AS DateTime), N'Test')
INSERT [dbo].[PlanDetail] ([RecordId], [PlanId], [Name], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [PlanInternalId]) VALUES (N'3ef51464-faae-4160-9744-a35042d5c4e5', N'NEW-PLAN-ID-4', N'This is the plan for the front end', 0, NULL, CAST(0x0000A3E30111CA0B AS DateTime), N'Test', N'22ec2f61-8c34-4501-867d-ede13cfb63ae')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'c26d7ce5-011c-4d47-90a1-16705e2a821a', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'F4486754-60ED-44E6-ADEB-245334EBB050', N'c26d7ce5-011c-4d47-90a1-16705e2a821a', N'Drug', N'Ndc', N'9999*', N'Matches', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'61565c88-7bcc-44d4-a3ac-1b1f09ff84f3', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'6F4AB74B-EB44-49F7-A78D-D470F8489122', N'61565c88-7bcc-44d4-a3ac-1b1f09ff84f3', N'Transaction', N'Formulary', N'true', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'b3908b8d-e70d-4474-9101-4fa167c1d116', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'D6584B0E-CE6D-4007-8C9B-B0CA74BE5E5A', N'b3908b8d-e70d-4474-9101-4fa167c1d116', N'Drug', N'VaClass', N'PENICILLINS,AMINO DERIVATIVES', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'6baaeaa0-e6f5-49d1-bce5-9cf774afe9fd', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'B6D4E526-5603-4426-9C7B-7D3696D40F68', N'6baaeaa0-e6f5-49d1-bce5-9cf774afe9fd', N'Drug', N'DosageForm', N'PWDR,RENST-ORAL', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'37d9cb74-146e-44b3-b080-c86fbf7e86f7', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'122AE846-E3AC-48FF-9D24-36D8F3D8019E', N'37d9cb74-146e-44b3-b080-c86fbf7e86f7', N'Drug', N'DosageForm', N'TAB', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'a2731be1-b6b0-49eb-93cb-d64225c38f56', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'A435CD4E-109F-47C3-BD66-7509BCA95D9D', N'a2731be1-b6b0-49eb-93cb-d64225c38f56', N'Transaction', N'Formulary', N'true', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[Atom] ([AtomId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'e9baec93-0ce6-436e-8b60-f8f364874cc2', CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomDetail] ([RecordId], [AtomId], [Class], [Property], [Value], [Operation], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'24E8D676-8F05-4022-ADDB-B80DB7839740', N'e9baec93-0ce6-436e-8b60-f8f364874cc2', N'Transaction', N'AmountOfCopay', N'5', N'', 0, null, CAST(0x0000A3E30111C9D7 AS DateTime), N'Test')
INSERT [dbo].[AtomGroup] ([AtomGroupId], [LogicalOperator], [Name]) VALUES (N'af708a83-428f-4e51-b84e-52555c06189d', N'And', N'')
INSERT [dbo].[AtomGroup] ([AtomGroupId], [LogicalOperator], [Name]) VALUES (N'd6b2d192-8f11-4d91-831f-b9aeef5e5b61', N'Or', N'')
INSERT [dbo].[AtomGroup] ([AtomGroupId], [LogicalOperator], [Name]) VALUES (N'de4bb4e6-2c7b-44c5-8bc5-c5a906c3c590', N'Or', N'DosageForm is TAB or PWDR,RENST-ORAL')
INSERT [dbo].[AtomGroup] ([AtomGroupId], [LogicalOperator], [Name]) VALUES (N'3e0d1617-617a-4e19-8546-f48a9b2bd844', N'And', N'')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'6b9ee60d-e82d-4b96-8ab5-2be7cfc3a99e', N'd6b2d192-8f11-4d91-831f-b9aeef5e5b61', N'b3908b8d-e70d-4474-9101-4fa167c1d116', NULL, 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'51ade836-fab9-4cec-92e9-542615941806', N'af708a83-428f-4e51-b84e-52555c06189d', NULL, N'd6b2d192-8f11-4d91-831f-b9aeef5e5b61', 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'88eee53d-1ac1-48ff-b747-78dc815b0ae7', N'de4bb4e6-2c7b-44c5-8bc5-c5a906c3c590', N'6baaeaa0-e6f5-49d1-bce5-9cf774afe9fd', NULL, 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'c925e642-c8b1-4f9e-8e9c-7f9c9fb0225d', N'af708a83-428f-4e51-b84e-52555c06189d', NULL, N'de4bb4e6-2c7b-44c5-8bc5-c5a906c3c590', 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'11c951dc-a8e3-4126-aeb3-cde99ebae64b', N'de4bb4e6-2c7b-44c5-8bc5-c5a906c3c590', N'37d9cb74-146e-44b3-b080-c86fbf7e86f7', NULL, 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'f2f10923-3837-49b2-8cb9-d87499f0b7b7', N'3e0d1617-617a-4e19-8546-f48a9b2bd844', N'a2731be1-b6b0-49eb-93cb-d64225c38f56', NULL, 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[AtomGroupItem] ([RecordId], [AtomGroupId], [AtomId], [ContainedAtomGroupId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'f890972e-175d-4e2a-9795-f15ff10526bd', N'd6b2d192-8f11-4d91-831f-b9aeef5e5b61', N'c26d7ce5-011c-4d47-90a1-16705e2a821a', NULL, 0, 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[Implication] ([ImplicationId], [AtomGroupId], [DeductionAtomId], [Label]) VALUES (N'55e9d221-f35c-495d-9ae1-36a57d2cca59', N'af708a83-428f-4e51-b84e-52555c06189d', N'61565c88-7bcc-44d4-a3ac-1b1f09ff84f3', N'PENICILLINS,AMINO DERIVATIVES with PWDR,RENST-ORAL OR TAB are formulary')
INSERT [dbo].[Implication] ([ImplicationId], [AtomGroupId], [DeductionAtomId], [Label]) VALUES (N'ba351ab6-6425-478b-a6ab-761b66870995', N'3e0d1617-617a-4e19-8546-f48a9b2bd844', N'e9baec93-0ce6-436e-8b60-f8f364874cc2', N'Formulary drugs have 5 dollar copay')
INSERT [dbo].[RuleImplication] ([RecordId], [RuleId], [ImplicationId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'33b797cb-eeeb-4df7-a54d-590402da3574', N'f987c063-fadd-4aff-b1b4-b8780994489b', N'55e9d221-f35c-495d-9ae1-36a57d2cca59', N'', 0, null, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[RuleImplication] ([RecordId], [RuleId], [ImplicationId], [Priority], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser]) VALUES (N'1f19d2fe-09d4-4d8b-9a61-dae6fcd9a393', N'9e443e4f-65ce-4973-8232-68f651ed08ac', N'ba351ab6-6425-478b-a6ab-761b66870995', N'', 0, null, CAST(0x0000A3B00117EB89 AS DateTime), N'Test')
INSERT [dbo].[ClientDetail] ([RecordId], [ClientId], [Name], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [ClientInternalId]) VALUES (N'0f0f9fb0-067d-4b5c-b807-1173c1dba959', N'OB', N'Oyster Bar', 0, NULL, CAST(0x0000A3B00117EB89 AS DateTime), N'Test', N'874199af-8839-4984-b4a0-c4db53d5442a')
INSERT [dbo].[ClientDetail] ([RecordId], [ClientId], [Name], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [ClientInternalId]) VALUES (N'fa73ca67-1ba8-4277-9b81-28d8c0467641', N'ACME', N'New ACME Corporation Name', 0, NULL, CAST(0x0000A3B000F6F609 AS DateTime), N'Test', N'ee60cd12-8063-4fbc-9165-0c6f7d81b6eb')
INSERT [dbo].[ClientDetail] ([RecordId], [ClientId], [Name], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [ClientInternalId]) VALUES (N'ee91e12b-f74d-439b-b85c-3e98dfc4b69e', N'ACME', N'ACME Corporation', 0, NULL, CAST(0x0000A3B000F6F202 AS DateTime), N'Test', N'ee60cd12-8063-4fbc-9165-0c6f7d81b6eb')
INSERT [dbo].[ClientDetail] ([RecordId], [ClientId], [Name], [Retraction], [OriginalFactRecordId], [RecordCreatedDateTime], [RecordCreatedUser], [ClientInternalId]) VALUES (N'36439c62-8ead-4f0b-ba21-9429371392dc', N'ACME', N'New ACME Corporation Name', 1, N'ee91e12b-f74d-439b-b85c-3e98dfc4b69e', CAST(0x0000A3B000F6F609 AS DateTime), N'Test', N'ee60cd12-8063-4fbc-9165-0c6f7d81b6eb')