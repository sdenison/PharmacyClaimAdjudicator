using System;
using Csla;

namespace PharmacyAdjudicator.Library.Core.Patient
{
    [Serializable]
    public class PatientExistsCommand : CommandBase<PatientExistsCommand>
    {
        public PatientExistsCommand(long patientId)
        {
            this.PatientId = patientId;
        }

        #region Client-side Code

        public static PropertyInfo<long> PatientIdProperty = RegisterProperty<long>(c => c.PatientId);
        private long PatientId
        {
            get { return ReadProperty(PatientIdProperty); }
            set { LoadProperty(PatientIdProperty, value); }
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
            //using (var ctx = PharmacyAdjudicator.Dal.DalFactory.GetManager())
            //{
            //    var dal = ctx.GetProvider<PharmacyAdjudicator.Dal.IPatientDal>();
            //    PatientExists = dal.Exists(PatientId);
            //}
        }
#endif

        #endregion
    }
}
