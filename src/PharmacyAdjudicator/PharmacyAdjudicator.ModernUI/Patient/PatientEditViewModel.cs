using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel;
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

        //Supplies values for gender ComboBoxes 
        public IEnumerable<Library.Core.Enums.Gender> GenderValues
        {
            get
            {
                return Enum.GetValues(typeof(Library.Core.Enums.Gender)).Cast<Library.Core.Enums.Gender>();
            }
        }
    }
}    
