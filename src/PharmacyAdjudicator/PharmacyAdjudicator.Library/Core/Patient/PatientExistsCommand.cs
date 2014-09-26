using System;
using System.Linq;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Patient
{
    [Serializable]
    public class PatientExistsCommand : CommandBase<PatientExistsCommand>
    {

        public static bool Execute(string firstName, string lastName, DateTime birthDate, string cardholderId)
        {
            PatientExistsCommand cmd = new PatientExistsCommand() { Firstname = firstName, LastName = lastName, BirthDate = birthDate, CardholderId = cardholderId };
            cmd = DataPortal.Execute<PatientExistsCommand>(cmd);
            return cmd.PatientExists;
        }

        private PatientExistsCommand()
        { /* require use of factor methods */ }

        #region Client-side Code

        public static readonly PropertyInfo<string> FirstnameProperty = RegisterProperty<string>(c => c.Firstname);
        public string Firstname
        {
            get { return ReadProperty(FirstnameProperty); }
            set { LoadProperty(FirstnameProperty, value); }
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(c => c.LastName);
        public string LastName
        {
            get { return ReadProperty(LastNameProperty); }
            set { LoadProperty(LastNameProperty, value); }
        }

        public static readonly PropertyInfo<DateTime> BirthDateProperty = RegisterProperty<DateTime>(c => c.BirthDate);
        public DateTime BirthDate 
        {
            get { return ReadProperty(BirthDateProperty); }
            set { LoadProperty(BirthDateProperty, value); }
        }

        public static readonly PropertyInfo<string> CardholderIdProperty = RegisterProperty<string>(c => c.CardholderId);
        public string CardholderId
        {
            get { return ReadProperty(CardholderIdProperty); }
            set { LoadProperty(CardholderIdProperty, value); }
        }

        public static readonly PropertyInfo<bool> PatientExistsProperty = RegisterProperty<bool>(p => p.PatientExists);
        public bool PatientExists
        {
            get { return ReadProperty(PatientExistsProperty); }
            set { LoadProperty(PatientExistsProperty, value); }
        }

        #endregion

        #region Server-side Code
#if !SILVERLIGHT
        protected override void DataPortal_Execute()
        {
            using (var ctx = DbContextManager<DataAccess.PharmacyClaimAdjudicatorEntities>.GetManager())
            {
                this.PatientExists = (from p in ctx.DbContext.PatientDetail
                                      where p.FirstName == this.Firstname
                                      && p.LastName == this.LastName
                                      && p.BirthDate == this.BirthDate
                                      && p.CardholderId == this.CardholderId
                                      && p.Retraction == false
                                      && !ctx.DbContext.PatientDetail.Any(p2 => p2.Retraction == true && p2.OriginalFactRecordId == p.RecordId)
                                      select p).Any();
            }
        }
#endif
        #endregion
    }
}
