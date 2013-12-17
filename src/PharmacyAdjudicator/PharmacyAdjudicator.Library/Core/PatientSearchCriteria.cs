using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Csla;

namespace PharmacyAdjudicator.Library.Core
{
    [Serializable]
    public class PatientSearchCriteria : CriteriaBase<PatientSearchCriteria>
    {
        public static readonly PropertyInfo<string> PatientLastNameProperty = RegisterProperty<string>(c => c.PatientLastName);
        public string PatientLastName
        {
            get { return ReadProperty(PatientLastNameProperty); }
            set { LoadProperty(PatientLastNameProperty, value); }
        }

        public static readonly PropertyInfo<string> PatientFirstNameProperty = RegisterProperty<string>(c => c.PatientFirstName);
        public string PatientFirstName
        {
            get { return ReadProperty(PatientFirstNameProperty); }
            set { LoadProperty(PatientFirstNameProperty, value); }
        }
    }
}
