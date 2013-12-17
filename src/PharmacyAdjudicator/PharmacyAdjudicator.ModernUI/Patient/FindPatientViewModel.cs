using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    public class FindPatientViewModel : ScreenWithModel<Library.Core.PatientSearchCriteria>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardholderId { get; set; }
        public string Group { get; set; }
    }
}
