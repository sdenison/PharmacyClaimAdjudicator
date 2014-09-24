using System;
using Csla;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PharmacyAdjudicator.Library.Core.Patient
{
    [Serializable]
    public class PatientEdit : BusinessBase<PatientEdit>
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

        [Display(Name= "Full Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
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

        public static readonly PropertyInfo<long> PatientIdProperty = RegisterProperty<long>(c => c.PatientId);
        /// <summary>
        /// Unique Patient Id
        /// </summary>
        /// <value>
        /// Internal to adjudication system and not part of NCPDP standard
        /// </value>
        public long PatientId
        {
            get { return GetProperty(PatientIdProperty); }
            private set { LoadProperty(PatientIdProperty, value); }
        }

        public static readonly PropertyInfo<PatientAddressList> PatientAddressesProperty = RegisterProperty<PatientAddressList>(c => c.PatientAddresses, RelationshipTypes.Child);
        public PatientAddressList PatientAddresses
        {
            get
            {
                if (!(FieldManager.FieldExists(PatientAddressesProperty)))
                    LoadProperty(PatientAddressesProperty, DataPortal.FetchChild<PatientAddressList>(this.PatientId));
                return GetProperty(PatientAddressesProperty);
            }
            private set { SetProperty(PatientAddressesProperty, value); }
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

        public static readonly PropertyInfo<string> LastChangedUserNameProperty = RegisterProperty<string>(c => c.LastChangedUserName);
        public string LastChangedUserName
        {
            get {  return GetProperty(LastChangedUserNameProperty); }
            private set { LoadProperty(LastChangedUserNameProperty, value);  }
        }

        private Guid _RecordId;
        private Guid RecordId
        {
            get { return _RecordId; }
            set { _RecordId = value; }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(FirstNameProperty));
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.Required(LastNameProperty));


            BusinessRules.AddRule(new OnlyUniqueAddressTypesAllowedInAddressList(PatientAddressesProperty));
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, FirstNameProperty, "RuleManager"));
            //BusinessRules.AddRule(new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.WriteProperty, LastNameProperty, "RuleManager"));
        }

        protected override void OnChildChanged(Csla.Core.ChildChangedEventArgs e)
        {
            base.OnChildChanged(e);
            BusinessRules.CheckRules(PatientEdit.PatientAddressesProperty);
        }

        public LinqObservableCollection<Csla.Rules.BrokenRule> BrokenAddressRules
        {

            get
            {
                var source = this.BrokenRulesCollection;
                var query = from b in source
                            where b.Property.Equals("PatientAddresses")
                            select b;
                return new LinqObservableCollection<Csla.Rules.BrokenRule>(source, query);
                //var synced = (from b in this.BrokenRulesCollection where b.Property == "PatientAddresses" select b).ToSyncList(this.BrokenRulesCollection);
                //return synced;
            }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            //Requires that the user be in the RuleManager role to create, edit or delete an Patient object
            Csla.Rules.BusinessRules.AddRule(typeof(PatientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.GetObject, "RuleManager", "Manager", "Admin", "User"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.CreateObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.EditObject, "RuleManager", "Manager", "Admin"));
            Csla.Rules.BusinessRules.AddRule(typeof(PatientEdit), new Csla.Rules.CommonRules.IsInRole(Csla.Rules.AuthorizationActions.DeleteObject, "RuleManager", "Manager", "Admin"));
        }

        #endregion

        #region Factory Methods

#if !WINDOWS_PHONE
        public async static System.Threading.Tasks.Task<PatientEdit> GetByPatientIdAsync(long patientId)
        {
            return await DataPortal.FetchAsync<PatientEdit>(new CriteriaByPatientIdEF { PatientId = patientId });
        }
#endif

#if !SILVERLIGHT && !NETFX_CORE
        public static PatientEdit NewPatient()
        {
            return DataPortal.Create<PatientEdit>();
        }

        public static PatientEdit GetPatient(long patientId)
        {
            return DataPortal.Fetch<PatientEdit>(new CriteriaByPatientIdEF { PatientId = patientId });
        }

        public static PatientEdit GetByRecordId(Guid recordId)
        {
            return DataPortal.Fetch<PatientEdit>(new CriteriaByRecordIdEF { RecordId = recordId });
        }

        public static PatientEdit GetByPatientId(long patientId)
        {
            return DataPortal.Fetch<PatientEdit>(new CriteriaByPatientIdEF { PatientId = patientId });
        }

        public static PatientEdit GetByPatientIdCompareDate(long patientId, DateTime compareDateTime)
        {
            return DataPortal.Fetch<PatientEdit>(new CriteriaByPatientIdCompareDatetime { PatientId = patientId, RecordCompareDatetime = compareDateTime });
        }

        public static PatientEdit GetByTransmissionCriteria(string processorControlNumber, string cardholderId, string groupId, DateTime dateOfBirth, string patientLastName,
                string patientGenderCode, string personCode, string relationshipCode)
        {
            return DataPortal.Fetch<PatientEdit>(new CriteriaByTransmission 
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

        private PatientEdit()
        { /* Require use of factory methods */ }

        internal PatientEdit(DataAccess.PatientDetail patientData)
        {
            using (BypassPropertyChecks)
                PopulateByRow(patientData);
        }

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
            public static readonly PropertyInfo<long> PatientIdProperty = RegisterProperty<long>(r => r.PatientId);
            public long PatientId
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
            public static readonly PropertyInfo<Guid> RecordIdProperty = RegisterProperty<Guid>(r => r.RecordId);
            public Guid RecordId
            {
                get { return ReadProperty(RecordIdProperty); }
                set { LoadProperty(RecordIdProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByPatientIdEF: CriteriaBase<CriteriaByPatientIdEF>
        {
            public static readonly PropertyInfo<long> PatientIdProperty = RegisterProperty<long>(p => p.PatientId);
            public long PatientId
            {
                get { return ReadProperty(PatientIdProperty); }
                set { LoadProperty(PatientIdProperty, value); }
            }
        }

        [Serializable]
        private class CriteriaByRecordIdDS : CriteriaBase<CriteriaByRecordIdDS>
        {
            public static readonly PropertyInfo<long> RecordIdProperty = RegisterProperty<long>(r => r.RecordId);
            public long RecordId
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
                DataAccess.PatientDetail patientData;
                //Gets the patient record active on the compare date/time passed in
                patientData = (from p in ctx.DbContext.PatientDetail
                               where
                               p.PatientId == criteria.PatientId
                               && p.Retraction == false
                               && p.RecordCreatedDateTime < criteria.RecordCompareDatetime
                               && !ctx.DbContext.PatientDetail.Any(p2 => p2.PatientId == criteria.PatientId && p2.Retraction == true && p2.OriginalFactRecordId == p.RecordId && p2.RecordCreatedDateTime < criteria.RecordCompareDatetime)
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
                DataAccess.PatientDetail patientData = (from p in ctx.DbContext.PatientDetail
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
                DataAccess.PatientDetail patientData;
                //Pulls the most recent patient record
                patientData = (from p in ctx.DbContext.PatientDetail
                               where 
                               p.PatientId == criteria.PatientId
                               && p.Retraction == false
                               && !ctx.DbContext.PatientDetail.Any(p2 => p2.PatientId == criteria.PatientId && p2.Retraction == true && p2.OriginalFactRecordId == p.RecordId)
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
                var patientIds = ((from p in ctx.DbContext.PatientDetail
                          where p.CardholderId == criteria.CardholderId
                          && p.BirthDate == criteria.DateOfBirth
                          && p.Gender == criteria.PatientGenderCode
                          select p.PatientId).Distinct()).ToList();

                //Throw and error if the cardholder is not found 
                if (patientIds.Count == 0)
                    throw new DataNotFoundException("CardholderId = " + criteria.CardholderId);
                if (patientIds.Count > 1)
                    //throw new Exception("Could not uniquely identify patient");
                    throw new DataNotUniqueException("CardHolderId = " + criteria.CardholderId + "; DateOfBirth = " +
                                criteria.DateOfBirth.ToString("MM/dd/yyyy") + "; PatientGenderCode " + criteria.PatientGenderCode);

                //If we only have one patientId is the list then we can load that patient.
                //Otherwise we have to keep adding more data to the criteria
                DataPortal_Fetch(new CriteriaByPatientIdEF { PatientId = patientIds[0] });
                return;

                //if (patientIds.Count == 1)
                //{
                //    DataPortal_Fetch(new CriteriaByPatientIdEF { PatientId = patientIds[0] });
                //    return;
                //}


            }
        }

        private void PopulateByRow(DataAccess.PatientDetail patientData)
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
            this.LastChangedDateTime = patientData.RecordCreatedDateTime;
            this.LastChangedUserName = patientData.RecordCreatedUser;
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                var patientData = CreateNewEntity();
                ctx.DbContext.PatientDetail.Add(patientData);
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
                PopulateByRow(patientData);
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                //Adds an entry in the database retracting the current data.
                RetractFact();
                //Adds an entry in the database asserting the current data.
                //Need to return this because rowid will be changed when savechanges is called
                var newPatientData = AssertNewFact();
                FieldManager.UpdateChildren(this);
                ctx.DbContext.SaveChanges();
                PopulateByRow(newPatientData);
            }
        }

        private void RetractFact()
        {
            using(BypassPropertyChecks)
            {
                //Add a record with Retraction = true and OriginalFactRecordId = currentRecordId
                var originalRecordId = this.RecordId;
                var patientData = CreateNewEntity();
                patientData.Retraction = true;
                patientData.OriginalFactRecordId = originalRecordId;
                using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                {
                    ctx.DbContext.PatientDetail.Add(patientData);
                }
            }
        }

        private DataAccess.PatientDetail AssertNewFact()
        {
            var patientData = CreateNewEntity();
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                ctx.DbContext.PatientDetail.Add(patientData);
            }
            return patientData;
        }

        /// <summary>
        /// Deleting items is really just retracting previously asserted facts which 
        /// result in a retraction record being added.
        /// </summary>
        protected override void DataPortal_DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                RetractFact();
                //var patientData = CreateNewEntity();
                //using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
                //{
                //    patientData.Retraction = true;
                //    patientData.OriginalFactRecordId = this._RecordId;
                //    ctx.DbContext.PatientDetail.Add(patientData);
                //    ctx.DbContext.SaveChanges();
                //}
            }
        }

        private DataAccess.PatientDetail CreateNewEntity()
        {
            var patientData = new DataAccess.PatientDetail();
            patientData.RecordId = Guid.NewGuid();
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

        private void Fetch(DataAccess.PatientDetail patientData)
        {
            using (BypassPropertyChecks)
                PopulateByRow(patientData);
        }

        #endregion

        #region Child Data Access

        protected override void Child_Create()
        {
            base.Child_Create();
        }

        private void Child_Fetch(DataAccess.PatientDetail patientData)
        {
            using (BypassPropertyChecks)
                PopulateByRow(patientData);
        }

        private void Child_Insert(object parent)
        {
            DataPortal_Insert();
        }

        private void Child_Update(object parent)
        {
            DataPortal_Update();
        }

        private void Child_DeleteSelf(object parent)
        {
            DataPortal_DeleteSelf();
        }

        #endregion
    }
}
