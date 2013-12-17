using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using CslaContrib.Caliburn.Micro;

namespace PharmacyAdjudicator.ModernUI.Patient
{
    [Export]
    public class PatientEditViewModel : ScreenWithModel<Library.Core.Patient>
    {
      
        [ImportingConstructor]
        public PatientEditViewModel()
        {
            //For development
            this.Model = Library.Core.Patient.GetByPatientId(22);
        }

        public PatientEditViewModel(Library.Core.Patient existingPatient)
        {
            this.Model = existingPatient;
        }
    }
}
