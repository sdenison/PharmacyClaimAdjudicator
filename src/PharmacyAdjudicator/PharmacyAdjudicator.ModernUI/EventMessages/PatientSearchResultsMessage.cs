using PharmacyAdjudicator.Library.Core.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAdjudicator.ModernUI.EventMessages
{
    public class PatientSearchResultsMessage
    {
        public object Requestor { get; set; }
        public PatientList PatientSearchResults { get; set; }
    }
}
