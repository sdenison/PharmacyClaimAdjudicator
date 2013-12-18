using System;
using Csla;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class Patient : BusinessBase<Patient>
    {
        #region Business Methods

        public static readonly PropertyInfo<string> PatientRelationshipCodeProperty = RegisterProperty<string>(c => c.PatientRelationshipCode );
        public string PatientRelationshipCode 
        {
            get { return GetProperty(PatientRelationshipCodeProperty); }
            set { SetProperty(PatientRelationshipCodeProperty, value); }
        }

        public static readonly PropertyInfo<string> PersonCodeProperty = RegisterProperty<string>(c => c.PersonCode);
        public string PersonCode
        {
            get { return GetProperty(PersonCodeProperty);  }
            set { SetProperty(PersonCodeProperty, value);  }
        }

        public static readonly PropertyInfo<DateTime?> DateOfBirthProperty = RegisterProperty<DateTime?>(c => c.DateOfBirth);
        /// <summary>
        /// Date of birth
        /// </summary>
        /// <value>
        /// NCPDP 304-C4
        /// </value>
        [Display(Name = "Date Of Birth")]
        [Required]
        public DateTime? DateOfBirth
        {
            get { return GetProperty(DateOfBirthProperty); }
            set { SetProperty(DateOfBirthProperty, value); }
        }

        public static readonly PropertyInfo<String> CardholderIdProperty = RegisterProperty<String>(c => c.CardholderId);
        [Display(Name = "Cardholder ID")]
        [Required]
        public String CardholderId
        {
            get { return GetProperty(CardholderIdProperty); }
            set { SetProperty(CardholderIdProperty, value); }
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        /// <summary>
        /// Patient last name
        /// </summary>
        /// <value>
        /// NCPDP 311-CB
        /// </value>
        [Display(Name = "Last Name")] 
        [Required]
        public string LastName
        {
            get { return GetProperty(LastNameProperty); }
            set { SetProperty(LastNameProperty, value); }
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(p => p.MiddleName);
        [Display(Name = "Middle Name")]
        public string MiddleName
        {
            get { return GetProperty(MiddleNameProperty); }
            set { SetProperty(MiddleNameProperty, value); }
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(p => p.FirstName);
        /// <summary>
        /// Patient first Name
        /// </summary>
        /// <value>
        /// NCPDP 310-CA
        /// </value>
        [Display(Name = "First Name")]
        [Required]
        public string FirstName
        {
            get { return GetProperty(FirstNameProperty); }
            set { SetProperty(FirstNameProperty, value); }
        }


        public static readonly PropertyInfo<Enums.Gender> GenderProperty = RegisterProperty<Enums.Gender>(p => p.Gender);
        /// <summary>
        /// Patient Gender
        /// </summary>
        /// <value>
        /// NCPDP 310-CA
        /// </value>
        [Display(Name = "Gender")]
        public Enums.Gender Gender
        {
            get { return GetProperty(GenderProperty); }
            set { SetProperty(GenderProperty, value); }
        }

        public static readonly PropertyInfo<int> PatientIdProperty = RegisterProperty<int>(c => c.PatientId);
        /// <summary>
        /// Unique Patient Id
        /// </summary>
        /// <value>
        /// Internal to adjudication system and not part of NCPDP standard
        /// </value>
        public int PatientId
        {
            get { return GetProperty(PatientIdProperty); }
            private set { LoadProperty(PatientIdProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> LastChangedDateTimeProperty = RegisterProperty<DateTime>(c => c.LastChangedDateTime);
        /// <summary>
        /// Last Changed DatetTime holds the last modified timestamp
        /// </summary>
        public DateTime LastChangedDateTime
        {
            get { return GetProperty(LastChangedDateTimeProperty); }
            private set { LoadProperty(LastChangedDateTimeProperty, value); }
        }

        private int _RecordId;
        private int RecordId
        {
            get { return _RecordId; }
            set { _RecordId = value; }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FirstNameProperty));
            BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(LastNameProperty));
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, FirstNameProperty, "RuleManager"));
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, LastNameProperty, "RuleManager"));
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            //Requires that the user be in the RuleManager role to create, edit or delete an Patient object
            Csla.Rules.BusinessRules.AddRule(typeof(Patient), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(Patient), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(Patient), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(Patient), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        #endregion

        #region Factory Methods


        //public static void GetProject(int id, EventHandler<DataPortalResult<ProjectEdit>> callback)
        //{
        //    ProjectGetter.GetExistingProject(id, (o, e) =>
        //    {
        //        callback(o, new DataPortalResult<ProjectEdit>(e.Object.Project, e.Error, null));
        //    });
        //}

        //public static void GetPatient(int patientId, EventHandler<DataPortalResult<Patient>> callback)
        //{

        //}

#if !WINDOWS_PHONE
        public async static System.Threading.Tasks.Task<Patient> GetByPatientIdAsync(int patientId)
        {
            await System.Threading.Tasks.Task.Delay(10000);
            return await DataPortal.FetchAsync<Patient>(new CriteriaByPatientIdEF { PatientId = patientId });
        }
#endif

#if !SILVERLIGHT && !NETFX_CORE
        public static Patient NewPatient()
        {
            return DataPortal.Create<Patient>();
        }

        public static Patient GetPatient(int patientId)
        {
            return DataPortal.Fetch<Patient>(new CriteriaByPatientIdEF { PatientId = patientId });
        }

        public static Patient GetByRecordId(int recordId)
        {
            return DataPortal.Fetch<Patient>(new CriteriaByRecordIdEF { RecordId = recordId });
        }

        public static Patient GetByPatientId(int patientId)
        {
            return DataPortal.Fetch<Patient>(new CriteriaByPatientIdEF { PatientId = patientId });
        }

        public static Patient GetByPatientIdCompareDate(int patientId, DateTime compareDateTime)
        {
            return DataPortal.Fetch<Patient>(new CriteriaByPatientIdCompareDatetime { PatientId = patientId, RecordCompareDatetime = compareDateTime });
        }

        public static Patient GetByTransmissionCriteria(string processorControlNumber, string cardholderId, string groupId, DateTime dateOfBirth, string patientLastName,
                string patientGenderCode, string personCode, string relationshipCode)
        {
            return DataPortal.Fetch<Patient>(new CriteriaByTransmission 
            {
                ProcessorControlNumber = processorControlNumber,
                CardholderId = cardholderId,
                GroupId = groupId, 
                DateOfBirth = dateOfBirth,
                PatientLastName = patientLastName,
                PatientGenderCode = patientGenderCode,
                PersonCode = personCode,
                PatientRelationshipCode = relationshipCode
            });
        }

        public static bool Exists(long patientId)
        {
            var cmd = new PatientExistsCommand(patientId);
            cmd = DataPortal.Execute<PatientExistsCommand>(cmd);
            return cmd.PatientExists;
        }

        private Patient()
        { /* Require use of factory methods */ }

#endif

        #endregion

#if !SILVERLIGHT
#endif

        #region Data Access

        [Serializable]
        private class CriteriaByTransmission : CriteriaBase<CriteriaByTransmission>
        {
            public static readonly PropertyInfo<string> PatientRelationshipCodeProperty = RegisterProperty<string>(c => c.PatientRelationshipCode);
            public string PatientRelationshipCode
            {
                get { return ReadProperty(PatientRelationshipCodeProperty); }
                set { LoadProperty(PatientRelationshipCodeProperty, value); }
            }
            public static readonly PropertyInfo<string> PersonCodeProperty = RegisterProperty<string>(c => c.PersonCode);
            public string PersonCode
            {
                get { return ReadProperty(PersonCodeProperty); }
                set { LoadProperty(PersonCodeProperty, value); }
            }
            public static readonly PropertyInfo<string> PatientGenderCodeProperty = RegisterProperty<string>(c => c.PatientGenderCode);
            public string PatientGenderCode
            {
                get { return ReadProperty(PatientGenderCodeProperty); }
                set { LoadProperty(PatientGenderCodeProperty, value); }
            }
            public static readonly PropertyInfo<string> PatientLastNameProperty = RegisterProperty<string>(c => c.PatientLastName);
            public string PatientLastName
            {
                get { return ReadProperty(PatientLastNameProperty); }
                set { LoadProperty(PatientLastNameProperty, value); }
            }
            public static readonly PropertyInfo<DateTime> DateOfBirthProperty = RegisterProperty<DateTime>(c => c.DateOfBirth);
            public DateTime DateOfBirth
            {
                get { return ReadProperty(DateOfBirthProperty); }
                set { LoadProperty(DateOfBirthProperty, value); }
            }

            public static readonly PropertyInfo<string> GroupIdProperty = RegisterProperty<string>(c => c.GroupId);
            public string GroupId
            {
                get { return ReadProperty(GroupIdProperty); }
                set { LoadProperty(GroupIdProperty, value); }
            }
            public static readonly PropertyInfo<string> CardholderIdProperty = RegisterProperty<string>(c => c.CardholderId);
            public string CardholderId
            {
                get { return ReadProperty(CardholderIdProperty); }
                set { LoadProperty(CardholderIdProperty, value); }
            }
            public static readonly PropertyInfo<string> ProcessorControlNumberProperty = RegisterProperty<string>(c => c.ProcessorControlNumber);
            public string ProcessorControlNumber
            {
                get { return ReadProperty(ProcessorControlNumberProperty); }
                set { LoadProperty(ProcessorControlNumberProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByPatientIdCompareDatetime: CriteriaBase<CriteriaByPatientIdCompareDatetime>
        {
            public static readonly PropertyInfo<int> PatientIdProperty = RegisterProperty<int>(r => r.PatientId);
            public int PatientId
            {
                get { return ReadProperty(PatientIdProperty); }
                set { LoadProperty(PatientIdProperty, value);  }
            }

            public static readonly PropertyInfo<DateTime> RecordCompareDatetimeProperty = RegisterProperty<DateTime>(r => r.RecordCompareDatetime);
            public DateTime RecordCompareDatetime
            {
                get { return ReadProperty(RecordCompareDatetimeProperty); }
                set { LoadProperty(RecordCompareDatetimeProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByRecordIdEF : CriteriaBase<CriteriaByRecordIdEF>
        {
            public static readonly PropertyInfo<int> RecordIdProperty = RegisterProperty<int>(r => r.RecordId);
            public int RecordId
            {
                get { return ReadProperty(RecordIdProperty); }
                set { LoadProperty(RecordIdProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByPatientIdEF: CriteriaBase<CriteriaByPatientIdEF>
        {
            public static readonly PropertyInfo<int> PatientIdProperty = RegisterProperty<int>(p => p.PatientId);
            public int PatientId
            {
                get { return ReadProperty(PatientIdProperty); }
                set { LoadProperty(PatientIdProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByRecordIdDS : CriteriaBase<CriteriaByRecordIdDS>
        {
            public static readonly PropertyInfo<int> RecordIdProperty = RegisterProperty<int>(r => r.RecordId);
            public int RecordId
            {
                get { return ReadProperty(RecordIdProperty); }
                set { LoadProperty(RecordIdProperty, value); }
            }
        }

        protected override void DataPortal_Create()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.Patient newPatient = new DataAccess.Patient();
                newPatient.RecordCreatedDateTime = DateTime.Now;
                newPatient.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
                ctx.DbContext.Patient.Add(newPatient);
                ctx.DbContext.SaveChanges();
                this.PatientId = newPatient.PatientId;
            }
            base.DataPortal_Create();
        }

        /// <summary>
        /// Uses CompareDateTime to pull the patient facts as of a certain date/time
        /// </summary>
        private void DataPortal_Fetch(CriteriaByPatientIdCompareDatetime criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.PatientFact patientData;
                //Gets the patient record active on the compare date/time passed in
                patientData = (from p in ctx.DbContext.PatientFacts
                               where
                               p.PatientId == criteria.PatientId
                               && p.RecordId == (from p2 in ctx.DbContext.PatientFacts
                                                 where p2.PatientId == criteria.PatientId
                                                 && p2.Retraction == false
                                                 && p2.RecordCreatedDateTime < criteria.RecordCompareDatetime 
                                                 && !ctx.DbContext.PatientFacts.Any(p3 => p3.PatientId == criteria.PatientId
                                                     && p3.Retraction == true
                                                     && p3.OriginalFactRecordId == p2.RecordId
                                                     && p3.RecordCreatedDateTime < criteria.RecordCompareDatetime)
                                                 select p2.RecordId).Max()
                               select p).FirstOrDefault();
                if (patientData != null)
                    using (BypassPropertyChecks)
                        PopulateByRow(patientData);
                else
                    throw new DataNotFoundException("PatientId = " + criteria.PatientId);
            }
        }

        private void DataPortal_Fetch(CriteriaByRecordIdEF criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //No sub-selects necessary since we are getting the record by the unique identifier
                DataAccess.PatientFact patientData = (from p in ctx.DbContext.PatientFacts
                                  where p.RecordId == criteria.RecordId
                                  select p).FirstOrDefault();
                if (patientData != null)
                    using (BypassPropertyChecks)
                        PopulateByRow(patientData);
                else
                    throw new DataNotFoundException("RecordId = " + criteria.RecordId);
            }
        }

        private void DataPortal_Fetch(CriteriaByPatientIdEF criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                DataAccess.PatientFact patientData;
                //Pulls the most recent patient record
                patientData = (from p in ctx.DbContext.PatientFacts
                               where 
                               p.PatientId == criteria.PatientId
                               && p.RecordId == (from p2 in ctx.DbContext.PatientFacts
                                                    where p2.PatientId == criteria.PatientId
                                                    && p2.Retraction == false
                                                    && !ctx.DbContext.PatientFacts.Any(p3 => p3.PatientId == criteria.PatientId
                                                        && p3.Retraction == true
                                                        && p3.OriginalFactRecordId == p2.RecordId)
                                                    select p2.RecordId).Max()
                               select p).FirstOrDefault();
                if (patientData != null)
                    using (BypassPropertyChecks)
                        PopulateByRow(patientData);
                else
                    throw new DataNotFoundException("PatientId = " + criteria.PatientId);
            }
        }

        //Searching for unique patient given the criteria available from the transmission
        private void DataPortal_Fetch(CriteriaByTransmission criteria)
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Cardholder ID, Birth Date and Gender are all reqired in the transmission.
                //We'll keep this simple for now.  When real patients are added something
                //more sophisticated will need to be put in place.
                var patientIds = ((from p in ctx.DbContext.PatientFacts 
                          where p.CardholderId == criteria.CardholderId
                          && p.BirthDate == criteria.DateOfBirth
                          && p.Gender == criteria.PatientGenderCode
                          select p.PatientId).Distinct()).ToList();

                //Throw and error if the cardholder is not found 
                if (patientIds.Count == 0)
                    throw new DataNotFoundException("CardholderId = " + criteria.CardholderId);

                //If we only have one patientId is the list then we can load that patient.
                //Otherwise we have to keep adding more data to the criteria

                if (patientIds.Count == 1)
                {
                    DataPortal_Fetch(new CriteriaByPatientIdEF { PatientId = patientIds[0] });
                    return;
                }

                if (patientIds.Count > 1)
                {
                    throw new Exception("Could not uniquely identify patient");
                }
            }
        }

        private void PopulateByRow(DataAccess.PatientFact patientData)
        {
            this.PatientId = patientData.PatientId;
            this.FirstName = patientData.FirstName;
            this.MiddleName = patientData.MiddleName;
            this.LastName = patientData.LastName;
            this.CardholderId = patientData.CardholderId;
            this.DateOfBirth = patientData.BirthDate;
            this.PersonCode = patientData.PersonCode;
            this.PatientRelationshipCode = patientData.PatientRelationshipCode;
            this.Gender = string.IsNullOrWhiteSpace(patientData.Gender) ? Enums.Gender.NotSet : (Enums.Gender)int.Parse(patientData.Gender);
            this._RecordId = patientData.RecordId;
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var patientData = CreateNewEntity();
                ctx.DbContext.PatientFacts.Add(patientData);
                ctx.DbContext.SaveChanges();
                var recordId = patientData.RecordId;
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var patientData = CreateNewEntity();
                ctx.DbContext.PatientFacts.Add(patientData);
                ctx.DbContext.SaveChanges();
                var recordId = patientData.RecordId;
            }
        }

        /// <summary>
        /// Deleting items is really just retracting previously asserted facts which 
        /// result in a retraction record being added.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                var patientData = CreateNewEntity();
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    patientData.Retraction = true;
                    patientData.OriginalFactRecordId = this._RecordId;
                    ctx.DbContext.PatientFacts.Add(patientData);
                    ctx.DbContext.SaveChanges();
                }
            }
        }

        private DataAccess.PatientFact CreateNewEntity()
        {
            var patientData = new DataAccess.PatientFact();
            patientData.PatientId = this.PatientId;
            patientData.FirstName = this.FirstName;
            patientData.MiddleName = this.MiddleName;
            patientData.LastName = this.LastName;
            patientData.CardholderId = this.CardholderId;
            patientData.BirthDate = this.DateOfBirth.Value;
            patientData.PersonCode = this.PersonCode;
            patientData.PatientRelationshipCode = this.PatientRelationshipCode;
            patientData.Gender = ((int)this.Gender).ToString();
            patientData.Retraction = false;
            patientData.RecordCreatedDateTime = DateTime.Now;
            patientData.RecordCreatedUser = Csla.ApplicationContext.User.Identity.Name;
            return patientData;
        }

        private void Child_Fetch(DataAccess.PatientFact patientData)
        {
            using (BypassPropertyChecks)
                PopulateByRow(patientData);
        }

        #endregion
    }
}
